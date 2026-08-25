using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIView : BaseUIView<LoginUIViewModel>
{
    [Header("로그인 패널")]
    [SerializeField] private TMP_InputField InputField_LoginId;
    [SerializeField] private TMP_Text Text_LoginIdPlaceholder;

    [SerializeField] private TMP_InputField InputField_LoginPassword;
    [SerializeField] private TMP_Text Text_LoginPasswordPlaceholder;

    [SerializeField] private TMP_Text Text_LoginNotification;

    [SerializeField] private Button Button_Login;
    [SerializeField] private TMP_Text Text_LoginButton;

    [SerializeField] private Button Button_OpenCreateAccountPanel;
    [SerializeField] private TMP_Text Text_OpenCreateAccountPanelButton;

    [SerializeField] private Button Button_ExitUI;
    [SerializeField] private TMP_Text Text_ExitUIButton;

    [SerializeField] private Toggle Toggle_RememberId;
    [SerializeField] private TMP_Text Text_RememberIdToggle;

    [SerializeField] private Toggle Toggle_ShowLoginPassword;

    [Header("회원가입 패널")]
    [SerializeField] private GameObject Panel_CreateAccount;

    [SerializeField] private TMP_Text Text_CreateAccountPanelName;

    [SerializeField] private TMP_Text Text_CreateId;
    [SerializeField] private TMP_InputField InputField_CreateId;

    [SerializeField] private TMP_Text Text_CreatePassword;
    [SerializeField] private TMP_InputField InputField_CreatePassword;

    [SerializeField] private TMP_Text Text_CreatePasswordConfirm;
    [SerializeField] private TMP_InputField InputField_CreatePasswordConfirm;

    [SerializeField] private TMP_Text Text_CreateAccountNotification;

    [SerializeField] private Button Button_CreateAccount;
    [SerializeField] private TMP_Text Text_CreateAccountButton;

    [SerializeField] private Button Button_ExitCreateAccountPanel;
    [SerializeField] private TMP_Text Text_ExitCreateAccountPanelButton;

    [SerializeField] private Toggle Toggle_ShowCreatePassword;

    [Header("알림 색상")]
    [SerializeField] private Color Color_Notification_Error = Color.red;
    [SerializeField] private Color Color_Notification_Success = Color.green;
    [SerializeField] private Color Color_Notification_None = Color.white;

    private void Awake()
    {
        InputField_LoginPassword.characterLimit = AccountUtil.PasswordMaxLength;
        InputField_LoginPassword.contentType = TMP_InputField.ContentType.Password;

        InputField_CreatePassword.characterLimit = AccountUtil.PasswordMaxLength;
        InputField_CreatePasswordConfirm.characterLimit = AccountUtil.PasswordMaxLength;
    }

    private void OnEnable()
    {
        Button_Login.onClick.AddListener(OnLogin);
        Button_CreateAccount.onClick.AddListener(OnCreateAccount);

        Button_OpenCreateAccountPanel.onClick.AddListener(OnOpenCreateAccountPanel);
        Button_ExitCreateAccountPanel.onClick.AddListener(OnExitCreateAccountPanel);

        Button_ExitUI.onClick.AddListener(OnExitUI);

        Toggle_ShowLoginPassword.onValueChanged.AddListener(OnShowLoginPasswordChanged);
        Toggle_ShowCreatePassword.onValueChanged.AddListener(OnShowCreatePasswordChanged);

        GameManager.Instance.OnLoginSuccess += OnLoginSuccess;

        RefreshLoginPanel();
        Panel_CreateAccount.SetActive(false);
    }

    private void OnDisable()
    {
        Button_Login.onClick.RemoveListener(OnLogin);
        Button_CreateAccount.onClick.RemoveListener(OnCreateAccount);

        Button_OpenCreateAccountPanel.onClick.RemoveListener(OnOpenCreateAccountPanel);
        Button_ExitCreateAccountPanel.onClick.RemoveListener(OnExitCreateAccountPanel);

        Button_ExitUI.onClick.RemoveListener(OnExitUI);

        Toggle_ShowLoginPassword.onValueChanged.RemoveListener(OnShowLoginPasswordChanged);
        Toggle_ShowCreatePassword.onValueChanged.RemoveListener(OnShowCreatePasswordChanged);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnLoginSuccess -= OnLoginSuccess;
        }
    }

    protected override void OnPropertyChanged(string propertyName)
    {
        switch (propertyName)
        {
            case nameof(LoginUIModel.LoginNotification):
                {
                    ApplyNotification(Text_LoginNotification, _viewModel.LoginNotification);
                }
                break;
            case nameof(LoginUIModel.CreateAccountNotification):
                {
                    ApplyNotification(Text_CreateAccountNotification, _viewModel.CreateAccountNotification);
                }
                break;
            case nameof(LoginUIModel.TextData):
                {
                    RefreshTexts();
                }
                break;
        }
    }

    private void ApplyNotification(TMP_Text tmpText, NotificationMessage notificationMessage)
    {
        tmpText.text = notificationMessage.Text;
        tmpText.color = GetNotificationColor(notificationMessage.Type);
    }

    private Color GetNotificationColor(NotificationType notificationType)
    {
        switch (notificationType)
        {
            case NotificationType.Error:
                {
                    return Color_Notification_Error;
                }
            case NotificationType.Success:
                {
                    return Color_Notification_Success;
                }
            default:
                {
                    return Color_Notification_None;
                }
        }
    }

    private void RefreshTexts()
    {
        Text_LoginIdPlaceholder.text = _viewModel.GetText("LoginPanel_Placeholder_Id");
        Text_RememberIdToggle.text = _viewModel.GetText("LoginPanel_Text_RememberIdToggle");

        Text_LoginPasswordPlaceholder.text = _viewModel.GetText("LoginPanel_Placeholder_Password");

        Text_LoginButton.text = _viewModel.GetText("LoginPanel_Text_LoginButton");
        Text_OpenCreateAccountPanelButton.text = _viewModel.GetText("LoginPanel_Text_OpenCreateAccountPanelButton");
        Text_ExitUIButton.text = _viewModel.GetText("LoginPanel_Text_ExitUIPanelButton");

        Text_CreateAccountPanelName.text = _viewModel.GetText("CreateAccountPanel_Text_PanelName");

        Text_CreateId.text = _viewModel.GetText("CreateAccountPanel_Text_Id");
        Text_CreatePassword.text = _viewModel.GetText("CreateAccountPanel_Text_Password");
        Text_CreatePasswordConfirm.text = _viewModel.GetText("CreateAccountPanel_Text_PasswordConfirm");

        Text_CreateAccountButton.text = _viewModel.GetText("CreateAccountPanel_Text_CreateAccountButton");
        Text_ExitCreateAccountPanelButton.text = _viewModel.GetText("CreateAccountPanel_Text_ExitCreateAccountPanelButton");
    }

    private void OnLogin()
    {
        string id = InputField_LoginId.text;
        string password = InputField_LoginPassword.text;

        bool isRememberId = Toggle_RememberId.isOn;

        if (isRememberId)
        {
            GameSettingManager.Instance.SetRememberId(true, id);
        }
        else
        {
            GameSettingManager.Instance.SetRememberId(false, string.Empty);
        }

        _viewModel.Login(id, password);
    }

    private void OnLoginSuccess()
    {
        UIManager.Instance.TryCloseUI(UIType.LoginUI);
    }

    private void OnCreateAccount()
    {
        string id = InputField_CreateId.text;
        string password = InputField_CreatePassword.text;
        string passwordConfirm = InputField_CreatePasswordConfirm.text;

        if(_viewModel.CreateAccount(id, password, passwordConfirm) == false)
        {
            return;
        }

        OnExitCreateAccountPanel();
    }

    private void OnOpenCreateAccountPanel()
    {
        RefreshCreateAccountPanel();
        Panel_CreateAccount.SetActive(true);
    }

    private void OnExitCreateAccountPanel()
    {
        Panel_CreateAccount.SetActive(false);
    }

    private void OnExitUI()
    {
        UIManager.Instance.TryCloseUI(UIType.LoginUI);
    }

    private void RefreshLoginPanel()
    {
        GameSettingData setting = GameSettingManager.Instance.GameCurrentSettingData;

        Toggle_RememberId.isOn = setting.IsRememberId;

        if (setting.IsRememberId)
        {
            InputField_LoginId.text = setting.RememberedId;
        }
        else
        {
            InputField_LoginId.text = string.Empty;
        }

        InputField_LoginPassword.text = string.Empty;  

        Text_LoginNotification.text = string.Empty;

        Toggle_ShowLoginPassword.isOn = false;
        SetPasswordVisible(InputField_LoginPassword, false);
    }

    private void RefreshCreateAccountPanel()
    {
        InputField_CreateId.text = string.Empty;
        InputField_CreatePassword.text = string.Empty;
        InputField_CreatePasswordConfirm.text = string.Empty;

        Text_CreateAccountNotification.text = string.Empty;

        Toggle_ShowCreatePassword.isOn = false;
        SetPasswordVisible(InputField_CreatePassword, false);
        SetPasswordVisible(InputField_CreatePasswordConfirm, false);
    }

    private void OnShowLoginPasswordChanged(bool isShow)
    {
        SetPasswordVisible(InputField_LoginPassword, isShow);
    }

    private void OnShowCreatePasswordChanged(bool isShow)
    {
        SetPasswordVisible(InputField_CreatePassword, isShow);
        SetPasswordVisible(InputField_CreatePasswordConfirm, isShow);
    }

    private void SetPasswordVisible(TMP_InputField inputField, bool isShow)
    {
        if(isShow)
        {
            inputField.contentType = TMP_InputField.ContentType.Standard;
        }
        else
        {
            inputField.contentType = TMP_InputField.ContentType.Password;
        }

        inputField.ForceLabelUpdate();
    }
}
