using System.Text.RegularExpressions;

namespace App.SourceGen;

public enum CssTokenType : byte
{
    Ident, String_, AtKeyword, Hash, Dot, Colon, ColonColon, Comma,
    OpenBrace, CloseBrace, OpenBracket, CloseBracket,
    OpenParen, CloseParen, Semicolon, Space,
    Asterisk, Gt, Plus, Tilde, Ampersand, Eof
}

public readonly struct CssToken(CssTokenType type, string text)
{
    public readonly CssTokenType Type = type;
    public readonly string Text = text;
}

public abstract class CssNode { private protected CssNode() { } }

public sealed class CssRule(List<string> selectors, List<CssNode> children) : CssNode
{
    public readonly List<string> Selectors = selectors;
    public readonly List<CssNode> Children = children;
}

public sealed class CssDecl(string property, string value) : CssNode
{
    public readonly string Property = property;
    public readonly string Value = value;
}

public sealed class CssAtRule : CssNode
{
    public readonly string Name;
    public readonly string Prelude;
    public readonly List<CssRule>? Children;
    public CssAtRule(string name, string prelude, List<CssRule>? children)
    { Name = name; Prelude = prelude; Children = children; }
}

public static class CssTokenizer
{
    public static List<CssToken> Tokenize(string css)
    {
        var tokens = new List<CssToken>(css.Length / 3);
        var i = 0;

        while (i < css.Length)
        {
            var ch = css[i];

            if (char.IsWhiteSpace(ch))
            {
                var start = i;
                while (i < css.Length && char.IsWhiteSpace(css[i])) i++;
                tokens.Add(new(CssTokenType.Space, css.Substring(start, i - start)));
                continue;
            }

            // Comment
            if (ch == '/' && Peek(css, i, 1) == '*')
            {
                i = css.IndexOf("*/", i + 2, StringComparison.Ordinal);
                if (i < 0) { i = css.Length; break; }
                i += 2;
                continue;
            }

            // String
            if (ch is '\'' or '"')
            {
                var start = i;
                i = SkipString(css, i);
                tokens.Add(new(CssTokenType.String_, css.Substring(start, i - start)));
                continue;
            }

            // At-keyword
            if (ch == '@')
            {
                var start = i;
                i++;
                while (i < css.Length && IsNameChar(css[i])) i++;
                tokens.Add(new(CssTokenType.AtKeyword, css.Substring(start, i - start)));
                continue;
            }

            // Hash
            if (ch == '#')
            {
                var start = i;
                i++;
                while (i < css.Length && IsNameChar(css[i])) i++;
                tokens.Add(new(CssTokenType.Hash, css.Substring(start, i - start)));
                continue;
            }

            // Double colon
            if (ch == ':' && Peek(css, i, 1) == ':')
            {
                tokens.Add(new(CssTokenType.ColonColon, "::"));
                i += 2;
                continue;
            }

            // Single-char tokens
            var tokenType = ch switch
            {
                '.' => CssTokenType.Dot,
                ':' => CssTokenType.Colon,
                ',' => CssTokenType.Comma,
                '{' => CssTokenType.OpenBrace,
                '}' => CssTokenType.CloseBrace,
                '[' => CssTokenType.OpenBracket,
                ']' => CssTokenType.CloseBracket,
                '(' => CssTokenType.OpenParen,
                ')' => CssTokenType.CloseParen,
                ';' => CssTokenType.Semicolon,
                '*' => CssTokenType.Asterisk,
                '>' => CssTokenType.Gt,
                '+' => CssTokenType.Plus,
                '~' => CssTokenType.Tilde,
                '&' => CssTokenType.Ampersand,
                _ => (CssTokenType?)null
            };

            if (tokenType is { } tt)
            {
                tokens.Add(new(tt, css[i].ToString()));
                i++;
                continue;
            }

            // Ident
            if (IsNameStart(ch))
            {
                var start = i;
                while (i < css.Length && IsNameChar(css[i])) i++;
                tokens.Add(new(CssTokenType.Ident, css.Substring(start, i - start)));
                continue;
            }

            i++; // skip unknown
        }

        tokens.Add(new(CssTokenType.Eof, ""));
        return tokens;
    }

    private static char Peek(string s, int i, int offset) =>
        i + offset < s.Length ? s[i + offset] : '\0';

    private static int SkipString(string s, int i)
    {
        var q = s[i]; i++;
        while (i < s.Length)
        {
            if (s[i] == '\\') { i += 2; continue; }
            if (s[i] == q) { i++; break; }
            i++;
        }
        return i;
    }

    private static bool IsNameStart(char c) =>
        char.IsLetterOrDigit(c) || c is '_' or '-' or '#';

    private static bool IsNameChar(char c) =>
        char.IsLetterOrDigit(c) || c is '_' or '-' or '%' or '#';
}

public static class CssParser
{
    private static readonly Regex s_classRe = new(@"\.([a-zA-Z_][\w-]*)", RegexOptions.Compiled);

    public static List<CssRule> Parse(List<CssToken> tokens)
    {
        var pos = 0;
        return ParseRules(tokens, ref pos, stopAtCloseBrace: false);
    }

    private static List<CssRule> ParseRules(List<CssToken> tokens, ref int pos, bool stopAtCloseBrace)
    {
        var rules = new List<CssRule>();
        while (pos < tokens.Count)
        {
            SkipSpace(tokens, ref pos);
            if (pos >= tokens.Count) break;

            switch (tokens[pos].Type)
            {
                case CssTokenType.CloseBrace:
                    if (stopAtCloseBrace) return rules;
                    pos++;
                    continue;
                case CssTokenType.AtKeyword:
                    var at = ParseAtRule(tokens, ref pos);
                    if (at.Children is { } c) rules.AddRange(c);
                    continue;
                case CssTokenType.Eof:
                    return rules;
                default:
                    rules.Add(ParseQualifiedRule(tokens, ref pos));
                    break;
            }
        }
        return rules;
    }

    private static CssRule ParseQualifiedRule(List<CssToken> tokens, ref int pos)
    {
        var selectors = ParseSelectorList(tokens, ref pos);
        Expect(tokens, ref pos, CssTokenType.OpenBrace);
        pos++;

        var children = new List<CssNode>();
        while (pos < tokens.Count)
        {
            SkipSpace(tokens, ref pos);
            if (pos >= tokens.Count) break;

            if (tokens[pos].Type == CssTokenType.CloseBrace) { pos++; break; }
            if (tokens[pos].Type == CssTokenType.Eof) break;

            if (IsSelectorStart(tokens, pos))
                children.Add(ParseQualifiedRule(tokens, ref pos));
            else
                children.Add(ParseDeclaration(tokens, ref pos));
        }

        return new(selectors, children);
    }

    private static List<string> ParseSelectorList(List<CssToken> tokens, ref int pos)
    {
        var parts = new List<string>();
        var sb = new System.Text.StringBuilder();

        while (pos < tokens.Count)
        {
            var t = tokens[pos];
            if (t.Type is CssTokenType.OpenBrace or CssTokenType.Eof) break;

            if (t.Type == CssTokenType.Comma)
            {
                parts.Add(sb.ToString().Trim());
                sb.Clear();
                pos++;
                continue;
            }

            sb.Append(t.Text);
            pos++;
        }

        var last = sb.ToString().Trim();
        if (last.Length > 0) parts.Add(last);
        return parts;
    }

    private static CssDecl ParseDeclaration(List<CssToken> tokens, ref int pos)
    {
        var propSb = new System.Text.StringBuilder();
        var valSb = new System.Text.StringBuilder();
        var inValue = false;

        while (pos < tokens.Count)
        {
            var t = tokens[pos];

            if (t.Type == CssTokenType.Semicolon) { pos++; break; }
            if (t.Type is CssTokenType.CloseBrace or CssTokenType.Eof) break;
            if (t.Type == CssTokenType.OpenBrace) break;

            if (t.Type == CssTokenType.Colon && !inValue) { inValue = true; pos++; continue; }

            (inValue ? valSb : propSb).Append(t.Text);
            pos++;
        }

        return new(propSb.ToString().Trim(), valSb.ToString().Trim());
    }

    private static CssAtRule ParseAtRule(List<CssToken> tokens, ref int pos)
    {
        var name = tokens[pos].Text;
        pos++; // skip @ident

        var preludeSb = new System.Text.StringBuilder();
        while (pos < tokens.Count)
        {
            var t = tokens[pos];
            if (t.Type is CssTokenType.OpenBrace or CssTokenType.Semicolon or CssTokenType.Eof)
                break;
            preludeSb.Append(t.Text);
            pos++;
        }

        List<CssRule>? children = null;
        if (pos < tokens.Count && tokens[pos].Type == CssTokenType.OpenBrace)
        {
            pos++;
            children = ParseRules(tokens, ref pos, stopAtCloseBrace: true);
            if (pos < tokens.Count && tokens[pos].Type == CssTokenType.CloseBrace) pos++;
        }
        else if (pos < tokens.Count) { pos++; }

        return new(name, preludeSb.ToString().Trim(), children);
    }

    /// <summary>Find the next un-bracketed { or ; — if { comes first, it's a selector.</summary>
    private static bool IsSelectorStart(List<CssToken> tokens, int pos)
    {
        var depth = 0;
        for (var i = pos; i < tokens.Count; i++)
        {
            var type = tokens[i].Type;
            if (type == CssTokenType.OpenParen) depth++;
            else if (type == CssTokenType.CloseParen) depth--;
            else if (depth == 0 && type == CssTokenType.OpenBrace) return true;
            else if (depth == 0 && type == CssTokenType.Semicolon) return false;
            else if (type is CssTokenType.CloseBrace or CssTokenType.Eof) return false;
        }
        return false;
    }

    private static void SkipSpace(List<CssToken> tokens, ref int pos)
    {
        while (pos < tokens.Count && tokens[pos].Type == CssTokenType.Space) pos++;
    }

    private static void Expect(List<CssToken> tokens, ref int pos, CssTokenType type)
    {
        if (pos >= tokens.Count || tokens[pos].Type != type)
            throw new InvalidOperationException($"Expected {type} at pos {pos}, got {tokens[pos].Type}");
    }

    public static HashSet<string> ExtractLocalClasses(List<CssRule> rules)
    {
        var set = new HashSet<string>();
        CollectLocalClasses(rules, set);
        return set;
    }

    private static void CollectLocalClasses(List<CssRule> rules, HashSet<string> set)
    {
        foreach (var rule in rules)
        {
            foreach (var sel in rule.Selectors)
            {
                var local = TakeBeforeCombinator(StripAmpersand(sel));
                foreach (Match m in s_classRe.Matches(local))
                    set.Add(m.Groups[1].Value);
            }

            foreach (var child in rule.Children)
            {
                if (child is CssRule nested)
                    CollectLocalClasses([nested], set);
            }
        }
    }

    private static string StripAmpersand(string selector)
    {
        var trimmed = selector.TrimStart();
        return trimmed.StartsWith("&") ? trimmed.Substring(1).TrimStart() : selector;
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
            if (s[i] is ' ' or '>' or '+' or '~')
                return s.Substring(0, i);
        }
        return s;
    }
}
