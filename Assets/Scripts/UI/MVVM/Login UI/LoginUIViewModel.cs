public class LoginUIViewModel : BaseViewModel<LoginUIModel>
{
    public NotificationMessage LoginNotification => _model.LoginNotification;
    public NotificationMessage CreateAccountNotification => _model.CreateAccountNotification;

    public LoginUIViewModel(LoginUIModel model) : base(model)
    {
        GameSettingManager.Instance.OnLanguageChanged += OnLanguageChanged;
    }

    public override void Dispose()
    {
        base.Dispose();

        GameSettingManager.Instance.OnLanguageChanged -= OnLanguageChanged;
    }

    public void Login(string id, string password)
    {
        LoginResult result = NetworkManager.Instance.TryLogin(id, password, out AccountData account);

        if (result != LoginResult.Success)
        {
            _model.LoginNotification = new(GetLoginErrorMessage(result), NotificationType.Error);
        }
        else
        {
            GameManager.Instance.SetAccount(account);
        }
    }

    public bool CreateAccount(string id, string password, string passwordConfirm)
    {
        CreateAccountResult result = NetworkManager.Instance.TryCreateAccount(id, password, passwordConfirm, out AccountData account);

        if (result != CreateAccountResult.Success)
        {
            _model.CreateAccountNotification = new(GetAccountErrorMessage(result), NotificationType.Error);

            return false;
        }
        else
        {
            _model.LoginNotification = new(GetAccountSuccessMessage(id), NotificationType.Success);

            return true;
        }
    }

    private string GetLoginErrorMessage(LoginResult result)
    {
        string key = "Error_" + result.ToString();

        string errorMessage = GetText(key);

        return errorMessage;
    }

    private string GetAccountErrorMessage(CreateAccountResult result)
    {
        string key = "Error_" + result.ToString();

        string errorMessage = GetText(key);

        return errorMessage;
    }

    private string GetAccountSuccessMessage(string id)
    {
        string key = CreateAccountResult.Success.ToString();

        string format = GetText(key);

        string successMessage = string.Format(format, id);

        return successMessage;
    }

    public string GetText(string key)
    {
        if (_model.TextData.TryGetValue(key, out LoginUIDataBase data) == false)
        {
            LogWarning($"[{key}]에 해당하는 데이터가 LoginUIDataBase에 없습니다!!");

            return string.Empty;
        }

        string text = data.Text;

        return text;
    }

    private void OnLanguageChanged()
    {
        _model.TextData = UIDataManager.Instance.LoginUIData; // 이거 좀 마음에 안드네
    }
}