using System.Runtime.InteropServices.JavaScript;

namespace App.Framework;

/// <summary>Minimal virtual DOM node types.</summary>
public abstract record VNode
{
    internal JSObject? DomNode { get; set; }
}

/// <summary>HTML element with tag, props, and children.</summary>
public sealed record VElement(
    string Tag,
    Dictionary<string, object?> Props,
    IReadOnlyList<VNode> Children
) : VNode;

/// <summary>Text node.</summary>
public sealed record VText(string Content) : VNode;

/// <summary>Fragment: children without a wrapper element.</summary>
public sealed record VFragment(IReadOnlyList<VNode> Children) : VNode;

/// <summary>Component node rendered by calling RenderFn() during reconciliation.</summary>
public sealed record VComponent(
    Func<VNode> RenderFn,
    string? Key = null
) : VNode;

/// <summary>Context provider that overrides a context value for its subtree.</summary>
internal sealed record VContextProvider(
    Type ContextType,
    object Value,
    VNode Child
) : VNode;
