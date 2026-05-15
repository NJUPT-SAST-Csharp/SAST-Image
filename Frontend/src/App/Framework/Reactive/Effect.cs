namespace App.Framework.Reactive;

internal sealed class Effect
{
    private readonly Action _execute;
    private readonly HashSet<IStateSignal> _dependencies = new();
    private readonly Dictionary<IStateSignal, int> _seenVersions = new();
    private Scope _scope = new();

    private const int MaxRerunIterations = 100;

    [ThreadStatic]
    internal static Stack<Effect>? ContextStack;

    [ThreadStatic]
    internal static Scope? OwnerScope;

    /// <summary>The effect currently executing on this thread.</summary>
    internal static Effect? Current =>
        ContextStack is { Count: > 0 } ? ContextStack.Peek() : null;

    internal Effect(Action fn) => _execute = fn;

    internal void AddDependency(IStateSignal signal)
    {
        if (_dependencies.Add(signal))
            _seenVersions[signal] = signal.Version;
    }

    internal void ClearDependencies()
    {
        foreach (var dep in _dependencies)
            dep.Unsubscribe(this);
        _dependencies.Clear();
        _seenVersions.Clear();
    }

    /// <summary>
    /// Run (or re-run) the effect body inside a fresh tracking context.
    /// Clears old dependencies and disposes the previous child scope first.
    /// After execution compares every recorded dependency version against
    /// current versions; any mismatch triggers a re-run so synchronous
    /// signal changes during execution are never silently dropped.
    /// </summary>
    internal void Execute()
    {

        int iterations = 0;
        bool needsRerun;
        do
        {
            if (++iterations > MaxRerunIterations) break;

            ContextStack ??= new();
            ContextStack.Push(this);

            ClearDependencies();

            var oldScope = _scope;
            _scope = new Scope();
            oldScope.Dispose();

            var prevOwner = OwnerScope;
            OwnerScope = _scope;

            try
            {
                _execute();
            }
            finally
            {
                ContextStack.Pop();
                OwnerScope = prevOwner;

                foreach (var dep in _dependencies)
                    dep.Subscribe(this, Execute);
            }

            needsRerun = false;
            foreach (var kv in _seenVersions)
            {
                if (kv.Key.Version != kv.Value)
                {
                    needsRerun = true;
                    break;
                }
            }
        } while (needsRerun);
    }

    internal void Dispose()
    {
        ClearDependencies();
        _scope.Dispose();
    }
}

/// <summary>
/// Owns a set of child effects and cleanup callbacks. When disposed, tears down
/// all effects (clears their dependencies and disposes their inner scopes) and
/// runs all cleanup callbacks in reverse order.
/// </summary>
public sealed class Scope : IDisposable
{
    private readonly List<WeakReference<Effect>> _effects = new();
    private readonly List<Action> _cleanups = new();
    private bool _disposed;

    internal void AddEffect(Effect effect) => _effects.Add(new(effect));

    internal void AddCleanup(Action cleanup) => _cleanups.Add(cleanup);

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        for (int i = _effects.Count - 1; i >= 0; i--)
        {
            if (_effects[i].TryGetTarget(out var effect))
                effect.Dispose();
        }
        _effects.Clear();

        for (int i = _cleanups.Count - 1; i >= 0; i--)
        {
            try { _cleanups[i](); }
            catch { }
        }
        _cleanups.Clear();
    }
}

public static class Rx
{
    private static readonly Scope s_globalScope = new();

    /// <summary>Create an auto-tracking effect, owned by the current scope.</summary>
    public static void CreateEffect(Action fn)
    {
        var effect = new Effect(fn);
        (Effect.OwnerScope ?? s_globalScope).AddEffect(effect);
        effect.Execute();
    }

    /// <summary>Create a reactive root scope that owns nested effects.</summary>
    public static Scope CreateRoot(Action fn)
    {
        var prevOwner = Effect.OwnerScope;
        var scope = new Scope();

        Effect.OwnerScope = scope;
        try
        {
            fn();
        }
        finally
        {
            Effect.OwnerScope = prevOwner;
        }

        return scope;
    }

    /// <summary>Run action outside tracking context — state reads won't create deps.</summary>
    public static void Untrack(Action action)
    {
        var savedStack = Effect.ContextStack;
        Effect.ContextStack = null;

        try
        {
            action();
        }
        finally
        {
            Effect.ContextStack = savedStack;
        }
    }

    /// <summary>Run fn outside tracking context — state reads won't create deps.</summary>
    public static T Untrack<T>(Func<T> fn)
    {
        var savedStack = Effect.ContextStack;
        Effect.ContextStack = null;
        try
        {
            return fn();
        }
        finally
        {
            Effect.ContextStack = savedStack;
        }
    }

    /// <summary>Register cleanup that runs on effect re-run or scope disposal.</summary>
    public static void OnCleanup(Action fn)
    {
        Effect.OwnerScope?.AddCleanup(fn);
    }

}
