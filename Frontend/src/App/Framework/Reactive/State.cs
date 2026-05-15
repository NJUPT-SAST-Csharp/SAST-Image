namespace App.Framework.Reactive;

public sealed class State<T> : IStateSignal
    where T : notnull
{
    private T _value;
    private int _version;
    private readonly List<(object Subscriber, Action Callback)> _subscribers = new();

    int IStateSignal.Version => _version;

    public State(T initial) => _value = initial;

    /// <summary>
    /// Read the value and auto-track this signal as a dependency of the current Effect.
    /// </summary>
    public T Value
    {
        get
        {
            Effect.Current?.AddDependency(this);
            return _value;
        }
    }

    /// <summary>
    /// Read the value without tracking.
    /// </summary>
    public T Peek() => _value;

    /// <summary>
    /// Set the value. Notifies subscribers only if the value actually changed.
    /// </summary>
    public void Set(T value)
    {
        if (EqualityComparer<T>.Default.Equals(_value, value))
            return;
        _value = value;
        _version++;
        Notify();
    }

    void IStateSignal.Subscribe(object subscriber, Action callback)
    {
        _subscribers.Add((subscriber, callback));
    }

    void IStateSignal.Unsubscribe(object subscriber)
    {
        _subscribers.RemoveAll(s => s.Subscriber == subscriber);
    }

    private void Notify()
    {
        var snapshot = _subscribers.ToArray();
        foreach (var (_, callback) in snapshot)
            callback();
    }
}

internal interface IStateSignal
{
    int Version { get; }
    void Subscribe(object subscriber, Action callback);
    void Unsubscribe(object subscriber);
}
