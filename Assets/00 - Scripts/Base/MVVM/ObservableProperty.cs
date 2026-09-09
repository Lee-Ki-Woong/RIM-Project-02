using System;
using System.Collections.Generic;

public sealed class ObservableProperty<T> : IReadOnlyObservableProperty<T>
{
    private T _value;
    
    private Action<T> _onValueChanged;

    public ObservableProperty() { }

    public ObservableProperty(T initialValue)
    {
        _value = initialValue;
    }

    public T Value
    {
        get => _value;
        set
        {
            if (EqualityComparer<T>.Default.Equals(_value, value))
            {
                return;
            }

            _value = value;
            _onValueChanged?.Invoke(_value);
        }
    }

    public IDisposable Subscribe(Action<T> onChanged, bool invokeImmediately = true)
    {
        _onValueChanged += onChanged;

        if (invokeImmediately)
        {
            onChanged(_value);
        }

        IDisposable subscription = new Subscription(this, onChanged);

        return subscription;
    }

    private class Subscription : IDisposable
    {
        private ObservableProperty<T> _owner;
        private Action<T> _handler;

        public Subscription(ObservableProperty<T> owner, Action<T> handler)
        {
            _owner = owner;
            _handler = handler;
        }

        public void Dispose()
        {
            if (_owner == null)
            {
                return;
            }

            _owner._onValueChanged -= _handler;
            _owner = null;
            _handler = null;
        }
    }
}