public interface IView<T> where T : BaseViewModel
{
    void BindViewModel(T viewModel);
}
