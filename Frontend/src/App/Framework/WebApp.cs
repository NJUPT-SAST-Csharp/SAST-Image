using App.Framework.Interop;
using App.Framework.Reactive;

namespace App.Framework;

/// <summary>Public entry point and VNode creation helpers.</summary>
public static class WebApp
{
    /// <summary>Mount a root component into the DOM element.</summary>
    public static void Mount<TComponent>(string cssSelector)
        where TComponent : IComponent, new()
    {
        var container = DomInterop.QuerySelector(cssSelector)
            ?? throw new InvalidOperationException($"No element found for selector: {cssSelector}");

        var rootComp = new TComponent();
        var componentFn = () => rootComp.Render();

        // The root component is treated as a VComponent inside a virtual parent.
        var parentComp = new ComponentInstance(() => new VText("__root__"), null, null);
        var vcomp = new VComponent(componentFn);

        Renderer.Reconcile(null, vcomp, container, parentComp);
    }

    // ── h() helpers ──────────────────────────────────────────────────

    /// <summary>Create an element with optional props and children.</summary>
    public static VNode H(
        string tag,
        IReadOnlyDictionary<string, object?>? props = null,
        params VNode[] children)
    {
        return new VElement(
            tag,
            props is null
                ? new Dictionary<string, object?>()
                : new Dictionary<string, object?>(props),
            children);
    }

    /// <summary>Create a component from a render function.</summary>
    public static VNode H(Func<VNode> component, string? key = null)
        => new VComponent(component, key);

    /// <summary>Create a component from its type. A new instance is created on mount.</summary>
    public static VNode H<T>(string? key = null) where T : IComponent, new()
        => new VComponent(() => new T().Render(), key);

    /// <summary>Wrap children in a fragment.</summary>
    public static VNode Fragment(params VNode[] children)
        => new VFragment(children);

    /// <summary>Create a context provider.</summary>
    public static VNode Provide<T>(Context<T> ctx, T value, VNode child) where T : class
        => new VContextProvider(typeof(Context<T>), value, child);
}

/// <summary>User component: provide a Render() method.</summary>
public interface IComponent
{
    VNode Render();
}
