using System.Runtime.InteropServices.JavaScript;

namespace App.Framework;

public static class Tags
{
    public static VNode txt(string text) => new VText(text);

    public static VNode div(params VNode[] children) =>
        WebApp.H("div", null, children);
    public static VNode div(string @class, params VNode[] children) =>
        WebApp.H("div", new Props(@class), children);
    public static VNode div(Props props, params VNode[] children) =>
        WebApp.H("div", props, children);

    public static VNode span(params VNode[] children) =>
        WebApp.H("span", null, children);
    public static VNode span(string @class, params VNode[] children) =>
        WebApp.H("span", new Props(@class), children);
    public static VNode span(Props props, params VNode[] children) =>
        WebApp.H("span", props, children);

    public static VNode p(params VNode[] children) =>
        WebApp.H("p", null, children);
    public static VNode p(string @class, params VNode[] children) =>
        WebApp.H("p", new Props(@class), children);
    public static VNode p(Props props, params VNode[] children) =>
        WebApp.H("p", props, children);

    public static VNode h1(params VNode[] children) =>
        WebApp.H("h1", null, children);
    public static VNode h1(string @class, params VNode[] children) =>
        WebApp.H("h1", new Props(@class), children);
    public static VNode h1(Props props, params VNode[] children) =>
        WebApp.H("h1", props, children);

    public static VNode h2(params VNode[] children) =>
        WebApp.H("h2", null, children);
    public static VNode h2(string @class, params VNode[] children) =>
        WebApp.H("h2", new Props(@class), children);
    public static VNode h2(Props props, params VNode[] children) =>
        WebApp.H("h2", props, children);

    public static VNode h3(params VNode[] children) =>
        WebApp.H("h3", null, children);
    public static VNode h3(string @class, params VNode[] children) =>
        WebApp.H("h3", new Props(@class), children);
    public static VNode h3(Props props, params VNode[] children) =>
        WebApp.H("h3", props, children);

    public static VNode h4(params VNode[] children) =>
        WebApp.H("h4", null, children);
    public static VNode h4(string @class, params VNode[] children) =>
        WebApp.H("h4", new Props(@class), children);
    public static VNode h4(Props props, params VNode[] children) =>
        WebApp.H("h4", props, children);

    public static VNode header(params VNode[] children) =>
        WebApp.H("header", null, children);
    public static VNode header(string @class, params VNode[] children) =>
        WebApp.H("header", new Props(@class), children);
    public static VNode header(Props props, params VNode[] children) =>
        WebApp.H("header", props, children);

    public static VNode nav(params VNode[] children) =>
        WebApp.H("nav", null, children);
    public static VNode nav(string @class, params VNode[] children) =>
        WebApp.H("nav", new Props(@class), children);
    public static VNode nav(Props props, params VNode[] children) =>
        WebApp.H("nav", props, children);

    public static VNode main(params VNode[] children) =>
        WebApp.H("main", null, children);
    public static VNode main(string @class, params VNode[] children) =>
        WebApp.H("main", new Props(@class), children);
    public static VNode main(Props props, params VNode[] children) =>
        WebApp.H("main", props, children);

    public static VNode button(params VNode[] children) =>
        WebApp.H("button", null, children);
    public static VNode button(string @class, params VNode[] children) =>
        WebApp.H("button", new Props(@class), children);
    public static VNode button(Props props, params VNode[] children) =>
        WebApp.H("button", props, children);
    public static VNode button(string @class, Action onclick, params VNode[] children) =>
        WebApp.H("button", new Props(@class).OnClick(onclick), children);

    public static VNode input(string type, string placeholder, string value, Action<JSObject> oninput) =>
        WebApp.H("input", new Props
        {
            ["class"] = "md-field",
            ["type"] = type,
            ["placeholder"] = placeholder,
            ["value"] = value,
            ["oninput"] = oninput
        });

    public static VNode img(string src, string alt) =>
        WebApp.H("img", new Props().Src(src).Alt(alt));
}

public sealed class Props : Dictionary<string, object?>
{
    public Props() { }
    public Props(string @class) { this["class"] = @class; }

    public Props Cls(string v) { this["class"] = v; return this; }
    public Props OnClick(Action v) { this["onclick"] = v; return this; }
    public Props OnInput(Action<JSObject> v) { this["oninput"] = v; return this; }
    public Props Type(string v) { this["type"] = v; return this; }
    public Props Placeholder(string v) { this["placeholder"] = v; return this; }
    public Props Value(string v) { this["value"] = v; return this; }
    public Props Disabled(string v) { this["disabled"] = v; return this; }
    public Props TitleAttr(string v) { this["title"] = v; return this; }
    public Props Src(string v) { this["src"] = v; return this; }
    public Props Alt(string v) { this["alt"] = v; return this; }
}
