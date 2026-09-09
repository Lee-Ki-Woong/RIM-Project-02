using System;

public interface IReadOnlyObservableProperty<T>
{
    T Value { get; }

    IDisposable Subscribe(Action<T> onChanged, bool invokeImmediately = true);
}
