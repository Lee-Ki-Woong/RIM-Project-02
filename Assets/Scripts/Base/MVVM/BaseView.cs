using UnityEngine;

public abstract class BaseView : MonoBehaviour
{
    public abstract void BindViewModel(BaseViewModel viewModel);

    protected abstract void OnPropertyChanged(string propertyName);

    protected virtual void OnDestroyAction()
    {
        // OnDestroy 할 때 필요한 추가 작업이 있다면 여기에 작성
        // BaseView<T>의 제네릭 값 BaseViewModel을 모르는 상태에서도 OnDestroyAction()을 호출할 수 있도록 설계함
    }
}

public abstract class BaseView<T> : BaseView where T : BaseViewModel
{
    protected T _viewModel;

    public override void BindViewModel(BaseViewModel viewModel)
    {
        if (viewModel is T typedViewModel)
        {
            InitViewModel(typedViewModel);
        }
        else
        {
            Debug.LogError($"가져온 viewModel을 {typeof(T)}로 형변환 할 수 없었습니다!!");
            return;
        }
    }

    private void InitViewModel(T viewModel)
    {
        if (_viewModel != null)
        {
            _viewModel.OnPropertyChangedForView -= OnPropertyChanged;
        }

        _viewModel = viewModel;
        _viewModel.OnPropertyChangedForView += OnPropertyChanged;
        _viewModel.PropertyChangedOnInit();
    }

    private void OnDestroy()
    {
        OnDestroyAction();

        if (_viewModel != null)
        {
            _viewModel.OnPropertyChangedForView -= OnPropertyChanged;
            _viewModel = null;
        }
    }

}
