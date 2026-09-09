using UnityEngine;

public abstract class BaseView<T> : MonoBehaviour, IView<T> where T : BaseViewModel
{
    protected T _viewModel;

    private readonly CompositeDisposable _subscriptions = new();

    public void BindViewModel(T viewModel)
    {
        Unbind();

        _viewModel = viewModel;

        Subscribe(_subscriptions);
    }

    protected abstract void Subscribe(CompositeDisposable subscriptions);

    protected virtual void OnDestroyAction() { }

    private void Unbind()
    {
        _subscriptions.Dispose();

        _viewModel = null;
    }

    private void OnDestroy()
    {
        OnDestroyAction();

        Unbind();
    }
}
