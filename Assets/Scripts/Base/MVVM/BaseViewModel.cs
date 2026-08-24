using System;
using UnityEngine;
using System.ComponentModel;

public abstract class BaseViewModel : IDisposable
{
    public event Action<string> OnPropertyChangedForView;

    protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEvent)
    {
        OnPropertyChangedForView?.Invoke(propertyChangedEvent.PropertyName);
    }

    public abstract void PropertyChangedOnInit();

    public abstract void Dispose();
}

public abstract class BaseViewModel<T> : BaseViewModel where T : BaseModel
{
    protected readonly T _model;

    public BaseViewModel(T model)
    {
        _model = model;
        _model.PropertyChanged += OnPropertyChanged;
    }

    public override void PropertyChangedOnInit()
    {
        _model.PropertyChangedOnInit();
    }

    public override void Dispose()
    {
        _model.PropertyChanged -= OnPropertyChanged;
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