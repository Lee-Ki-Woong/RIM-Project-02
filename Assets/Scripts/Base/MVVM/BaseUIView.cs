public abstract class BaseUIView<T> : BaseView<T>, IUIView where T : BaseViewModel
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

    protected virtual void OnOpenAction()
    {
        // UI를 열 때 필요한 추가작업이 있다면 여기에 작성
    }

    protected virtual void OnCloseAction()
    {
        // UI를 닫을 때 필요한 추가작업이 있다면 여기에 작성
    }
}
