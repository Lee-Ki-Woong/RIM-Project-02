using System.Collections.Generic;

public class LoginUIModel : BaseModel
{
    public override void PropertyChangedOnInit()
    {
        OnPropertyChanged(nameof(LoginNotification));
        OnPropertyChanged(nameof(CreateAccountNotification));
        OnPropertyChanged(nameof(TextData));
    }

    public LoginUIModel(IReadOnlyDictionary<string, LoginUIDataBase> textData)
    {
        _loginNotification = NotificationMessage.Empty();
        _createAccountNotification = NotificationMessage.Empty();

        _textData = textData;
    }

    private NotificationMessage _loginNotification;
    public NotificationMessage LoginNotification
    {
        get => _loginNotification;
        set
        {
            _loginNotification = value;
            OnPropertyChanged();
        }
    }

    private NotificationMessage _createAccountNotification;
    public NotificationMessage CreateAccountNotification
    {
        get => _createAccountNotification;
        set
        {
            _createAccountNotification = value;
            OnPropertyChanged();
        }
    }

    private IReadOnlyDictionary<string, LoginUIDataBase> _textData;
    public IReadOnlyDictionary<string, LoginUIDataBase> TextData
    {
        get => _textData;
        set
        {
            if (_textData != value)
            {
                _textData = value;
                OnPropertyChanged();
            }
        }
    }
}