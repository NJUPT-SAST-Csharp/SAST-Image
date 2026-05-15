using App.Framework.Reactive;

namespace App.Framework;

/// <summary>React-like hooks backed by the reactive core.</summary>
public static class Hooks
{
    /// <summary>Create or reuse reactive state. Setter marks component dirty.</summary>
    public static (T value, Action<T> setter) UseState<T>(T initial) where T : notnull
    {
        var comp = ComponentInstance.Current
            ?? throw new InvalidOperationException("useState must be called during component render");

        var idx = comp.HookIndex++;
        if (idx >= comp.HookStates.Count)
        {
            var state = new State<T>(initial);
            comp.HookStates.Add(state);
            return (state.Value, v => { state.Set(v); comp.MarkDirty(); });
        }

        var existing = (State<T>)comp.HookStates[idx]!;
        return (existing.Value, v => { existing.Set(v); comp.MarkDirty(); });
    }

    /// <summary>Run side effect after render. Auto-tracked when deps is null.</summary>
    public static void UseEffect(Action setup, object?[]? deps = null)
    {
        var comp = ComponentInstance.Current
            ?? throw new InvalidOperationException("useEffect must be called during component render");

        var idx = comp.HookIndex++;
        var prevDeps = idx < comp.HookStates.Count ? (object?[]?)comp.HookStates[idx] : null;

        if (prevDeps == null || !DepsEqual(prevDeps, deps))
        {
            comp.HookStates.EnsureCapacity(idx + 1);
            while (comp.HookStates.Count <= idx)
                comp.HookStates.Add(null);

            comp.HookStates[idx] = deps?.ToArray();

            comp.PostRenderActions.Add(() =>
            {
                if (deps == null)
                {
                    var eff = new Effect(setup);
                    eff.Execute();
                }
                else
                {
                    setup();
                }
            });
        }
    }

    /// <summary>Create a mutable ref that persists across renders.</summary>
    public static Ref<T> UseRef<T>(T initial)
    {
        var comp = ComponentInstance.Current
            ?? throw new InvalidOperationException("useRef must be called during component render");

        var idx = comp.HookIndex++;
        if (idx >= comp.HookStates.Count)
        {
            var r = new Ref<T>(initial);
            comp.HookStates.Add(r);
            return r;
        }

        return (Ref<T>)comp.HookStates[idx]!;
    }

    /// <summary>Consume context from nearest ancestor provider.</summary>
    public static T UseContext<T>(Context<T> ctx) where T : class
    {
        var comp = ComponentInstance.Current
            ?? throw new InvalidOperationException("useContext must be called during component render");

        var current = comp;
        while (current != null)
        {
            if (current.ContextOverrides.TryGetValue(typeof(Context<T>), out var value))
                return (T)value;
            current = current.Parent;
        }

        throw new InvalidOperationException(
            $"No provider found for context '{typeof(Context<T>).GenericTypeArguments[0].Name}'. " +
            "Wrap the consuming component tree in a Provide(...) call.");
    }

    private static bool DepsEqual(object?[]? a, object?[]? b)
    {
        if (a == null && b == null) return true;
        if (a == null || b == null) return false;
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; i++)
        {
            if (!Equals(a[i], b[i])) return false;
        }
        return true;
    }
}

/// <summary>Mutable reference that persists across renders.</summary>
public sealed class Ref<T>
{
    public T Value { get; set; }
    public Ref(T initial) => Value = initial;
}
