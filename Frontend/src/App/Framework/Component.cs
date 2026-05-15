using System.Runtime.InteropServices.JavaScript;
using App.Framework.Interop;
using App.Framework.Reactive;

namespace App.Framework;

/// <summary>Mounted component instance: hook state, render Effect, child refs, context.</summary>
internal sealed class ComponentInstance : IDisposable
{
    public string Key { get; }
    public Func<VNode> RenderFn { get; }
    public ComponentInstance? Parent { get; set; }

    // Hook state — indexed by call order, reset each render.
    public int HookIndex { get; set; }
    public List<object?> HookStates { get; } = new();
    public List<Action> PostRenderActions { get; } = new();

    // Context overrides from ancestor <Provider> nodes.
    public Dictionary<Type, object> ContextOverrides { get; } = new();

    // DOM — the parent element that owns this component's output.
    public JSObject? ParentDom { get; set; }

    // Rendering state.
    public VNode? OldChildVNode { get; set; }
    public bool IsDirty { get; set; } = true;
    public bool IsMounted { get; private set; }
    private bool _isDisposed;
    private Effect? _renderEffect;

    // Child component tracking keyed by (type hash, key).
    public Dictionary<(int TypeHash, string? Key), ComponentInstance> ChildInstances { get; } = new();
    private readonly HashSet<(int, string?)> _usedChildren = new();

    [ThreadStatic]
    private static Stack<ComponentInstance>? s_renderStack;

    public static ComponentInstance? Current =>
        s_renderStack is { Count: > 0 } ? s_renderStack.Peek() : null;

    public ComponentInstance(Func<VNode> renderFn, string? key = null, ComponentInstance? parent = null)
    {
        RenderFn = renderFn;
        Key = key ?? $"c_{Guid.NewGuid():N}";
        Parent = parent;
    }

    public static void PushFrame(ComponentInstance comp)
    {
        s_renderStack ??= new();
        s_renderStack.Push(comp);
        comp.HookIndex = 0;
    }

    public static void PopFrame()
    {
        s_renderStack!.Pop();
    }

    internal void MarkDirty()
    {
        if (_isDisposed) return;
        IsDirty = true;
        Scheduler.Enqueue(this);
    }

    /// <summary>Create/re-execute the auto-tracking render Effect.</summary>
    internal void ExecuteRender(JSObject parentDom)
    {
        if (_isDisposed) return;
        ParentDom = parentDom;
        IsMounted = true;

        if (_renderEffect == null)
        {
            _renderEffect = new Effect(FullRenderCycle);
        }

        if (IsDirty)
        {
            _renderEffect.Execute();
            IsDirty = false;
        }
    }

    private void FullRenderCycle()
    {
        // Runs inside the Effect's tracking context — state reads auto-subscribe.
        PushFrame(this);
        PostRenderActions.Clear();

        VNode rendered;
        try
        {
            rendered = RenderFn();
        }
        finally
        {
            PopFrame();
        }

        // DOM reconciliation runs untracked.
        Rx.Untrack(() =>
        {
            _usedChildren.Clear();

            OldChildVNode = Renderer.Reconcile(
                OldChildVNode, rendered, ParentDom!, this);

            // Dispose child instances that weren't used in this render.
            var toRemove = new List<(int, string?)>();
            foreach (var (key, child) in ChildInstances)
            {
                if (!_usedChildren.Contains(key))
                {
                    child.Dispose();
                    toRemove.Add(key);
                }
            }
            foreach (var key in toRemove)
                ChildInstances.Remove(key);
        });

        // Run post-render actions (useEffect setups).
        foreach (var action in PostRenderActions)
            action();
        PostRenderActions.Clear();
    }

    internal void MarkChildUsed(int typeHash, string? key)
    {
        _usedChildren.Add((typeHash, key));
    }

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;

        foreach (var child in ChildInstances.Values)
            child.Dispose();
        ChildInstances.Clear();

        _renderEffect?.ClearDependencies();
    }
}
