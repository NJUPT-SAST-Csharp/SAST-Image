using System.Text.RegularExpressions;

namespace App.SourceGen;

public static class CssBundler
{
    private static readonly Regex s_classRe = new(@"\.([a-zA-Z_][\w-]*)", RegexOptions.Compiled);

    public static string Bundle(string[] paths, string[] contents)
    {
        var indexed = new (string Path, string Content)[paths.Length];
        for (var i = 0; i < paths.Length; i++)
            indexed[i] = (paths[i], contents[i]);
        var sorted = indexed.OrderBy(f => f.Path, StringComparer.Ordinal).ToArray();

        // Pass 1: collect local class names
        var localClasses = new Dictionary<string, HashSet<string>>();
        foreach (var (path, content) in sorted)
        {
            var scope = GetScope(path);
            if (scope is null) continue;
            var tokens = CssTokenizer.Tokenize(content);
            var rules = CssParser.Parse(tokens);
            localClasses[scope] = CssParser.ExtractLocalClasses(rules);
        }

        // Pass 2: scope and bundle
        var sb = new System.Text.StringBuilder();
        foreach (var (path, content) in sorted)
        {
            var name = System.IO.Path.GetFileName(path);
            var scope = GetScope(path);

            sb.Append("/* ").Append(name).Append(" */\n");

            if (scope is not null && localClasses.TryGetValue(scope, out var local) && local.Count > 0)
                sb.Append(ScopeCss(content, scope, local));
            else
                sb.Append(content);

            if (!content.EndsWith("\n")) sb.Append('\n');
        }

        return sb.ToString();
    }

    private static string? GetScope(string path)
    {
        var name = System.IO.Path.GetFileName(path);
        if (!name.EndsWith(".module.css", StringComparison.Ordinal)) return null;
        var s = name.Substring(0, name.Length - ".module.css".Length);
        return s is "global" or "" ? null : s;
    }

    internal static string ScopeCss(string css, string scope, HashSet<string> local)
    {
        var sb = new System.Text.StringBuilder();
        var pos = 0;
        var lastOut = 0;

        while (pos < css.Length)
        {
            if (char.IsWhiteSpace(css[pos])) { pos++; continue; }
            if (Match(css, pos, "/*"))
            { pos = css.IndexOf("*/", pos + 2, StringComparison.Ordinal); if (pos < 0) break; pos += 2; continue; }
            if (css[pos] == '@')
            { pos = SkipAtRule(css, pos); continue; }
            if (css[pos] == '}') { pos++; continue; }

            // Peek ahead: is this a declaration (; before {) or a rule ({ before ;)?
            var peek = pos;
            var hasBrace = false;
            while (peek < css.Length)
            {
                var c = css[peek];
                if (c == '{') { hasBrace = true; break; }
                if (c == ';' || c == '}') break;
                if (c == '\'' || c == '"') { peek = SkipString(css, peek); continue; }
                if (Match(css, peek, "/*")) { peek = css.IndexOf("*/", peek + 2, StringComparison.Ordinal) + 1; continue; }
                if (c == '(') { peek = SkipParens(css, peek); continue; }
                peek++;
            }

            if (!hasBrace)
            {
                // Declaration: copy verbatim to next ; or }
                var end = pos;
                while (end < css.Length && css[end] != ';' && css[end] != '}')
                {
                    if (css[end] == '\'' || css[end] == '"') end = SkipString(css, end);
                    else end++;
                }
                if (end < css.Length && css[end] == ';') end++;
                sb.Append(css, lastOut, end - lastOut);
                pos = end;
                lastOut = pos;
                continue;
            }

            // Nested rule: scope the selector
            var selStart = pos;
            pos = SkipSelector(css, pos);
            if (pos >= css.Length || css[pos] != '{') break;

            var selector = css.Substring(selStart, pos - selStart).Trim();
            sb.Append(css, lastOut, selStart - lastOut);
            sb.Append(ScopeSelectorText(selector, scope, local));
            sb.Append('{');
            pos++;

            // Extract and recurse into body
            var bodyStart = pos;
            var depth = 1;
            while (pos < css.Length && depth > 0)
            {
                if (css[pos] == '{') depth++;
                else if (css[pos] == '}') { depth--; if (depth == 0) break; }
                pos++;
            }
            sb.Append(ScopeCss(css.Substring(bodyStart, pos - bodyStart), scope, local));
            sb.Append('}');
            pos++;
            lastOut = pos;
        }

        sb.Append(css, lastOut, css.Length - lastOut);
        return sb.ToString();
    }

    private static string ScopeSelectorText(string selector, string scope, HashSet<string> local)
    {
        var parts = selector.Split(',');
        for (var i = 0; i < parts.Length; i++)
        {
            var seg = parts[i].Trim();
            if (seg.Length == 0) continue;
            var localPart = TakeBeforeCombinator(seg);
            var after = seg.Length > localPart.Length ? seg.Substring(localPart.Length) : "";

            localPart = s_classRe.Replace(localPart, m =>
                local.Contains(m.Groups[1].Value)
                    ? "." + scope + "-" + m.Groups[1].Value
                    : m.Value);

            parts[i] = localPart + after;
        }
        return string.Join(",", parts);
    }

    private static string TakeBeforeCombinator(string segment)
    {
        var s = segment.Trim();
        var depth = 0;
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == '(') { depth++; continue; }
            if (s[i] == ')') { depth--; continue; }
            if (depth > 0) continue;
            if (s[i] is ' ' or '>' or '+' or '~') return s.Substring(0, i);
        }
        return s;
    }

    private static bool Match(string s, int pos, string p) =>
        pos + p.Length <= s.Length && s.IndexOf(p, pos, p.Length, StringComparison.Ordinal) == pos;

    private static int SkipSelector(string css, int pos)
    {
        while (pos < css.Length)
        {
            var ch = css[pos];
            if (ch == '{') break;
            if (ch == '\'' || ch == '"') { pos = SkipString(css, pos); continue; }
            if (Match(css, pos, "/*")) { pos = css.IndexOf("*/", pos + 2, StringComparison.Ordinal) + 2; continue; }
            if (ch == '(') { pos = SkipParens(css, pos); continue; }
            if (ch == '[') { pos = SkipBrackets(css, pos); continue; }
            pos++;
        }
        return pos;
    }

    private static int SkipAtRule(string css, int pos)
    {
        pos++; while (pos < css.Length && (char.IsLetterOrDigit(css[pos]) || css[pos] == '-')) pos++;
        while (pos < css.Length)
        {
            var ch = css[pos];
            if (ch == '{') { pos = SkipBlock(css, pos); break; }
            if (ch == ';') { pos++; break; }
            if (ch == '\'' || ch == '"') { pos = SkipString(css, pos); continue; }
            if (Match(css, pos, "/*")) { pos = css.IndexOf("*/", pos + 2, StringComparison.Ordinal) + 2; continue; }
            if (ch == '(') { pos = SkipParens(css, pos); continue; }
            pos++;
        }
        return pos;
    }

    private static int SkipBlock(string css, int pos)
    {
        var depth = 1; pos++;
        while (pos < css.Length && depth > 0)
        { if (css[pos] == '{') depth++; else if (css[pos] == '}') depth--; pos++; }
        return pos;
    }

    private static int SkipString(string css, int pos)
    {
        var q = css[pos]; pos++;
        while (pos < css.Length)
        { if (css[pos] == '\\') { pos += 2; continue; } if (css[pos] == q) { pos++; break; } pos++; }
        return pos;
    }

    private static int SkipParens(string css, int pos)
    {
        var depth = 1; pos++;
        while (pos < css.Length && depth > 0)
        { if (css[pos] == '(') depth++; else if (css[pos] == ')') depth--; else if (css[pos] == '\'' || css[pos] == '"') pos = SkipString(css, pos); pos++; }
        return pos;
    }

    private static int SkipBrackets(string css, int pos)
    {
        var depth = 1; pos++;
        while (pos < css.Length && depth > 0)
        { if (css[pos] == '[') depth++; else if (css[pos] == ']') depth--; pos++; }
        return pos;
    }
}
