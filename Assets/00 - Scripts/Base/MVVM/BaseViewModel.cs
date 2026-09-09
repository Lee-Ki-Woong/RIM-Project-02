using System;
using UnityEngine;

public abstract class BaseViewModel : IDisposable
{
    public abstract void Dispose();
}

public abstract class BaseViewModel<T> : BaseViewModel
{
    protected readonly T _model;

    protected readonly CompositeDisposable _subscriptions = new();

    public BaseViewModel(T model)
    {
        _model = model;
    }

    public override void Dispose()
    {
        _subscriptions.Dispose();
    }

    protected void Log(string text)
    {
        Debug.Log($"{this} : " + text);
    }

    protected void LogWarning(string text)
    {
        Debug.LogWarning($"{this} : " + text);
    }

    protected void LogError(string text)
    {
        Debug.LogError($"{this} : " + text);
    }
}
