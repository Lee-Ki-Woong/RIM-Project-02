using UnityEngine;

public abstract class BaseSimpleUIView : MonoBehaviour, IUIView
{
    public void Open()
    {
        this.ActiveTrue();
        OnOpenAction();
    }

    public void Close()
    {
        OnCloseAction();
        this.ActiveFalse();
    }

    protected virtual void OnOpenAction() { }
    protected virtual void OnCloseAction() { }
}
