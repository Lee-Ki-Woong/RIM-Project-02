using System;

[Serializable]
public class UIDataBase
{
    public string Id;
}

[Serializable]
public class LoginUIDataBase : UIDataBase
{
    public string Text;
}

[Serializable]
public class TitleButtonUIDataBase : UIDataBase
{
    public string TitleText;
    public string ExitGameButton;
    public string CloseExitGamePanelButton;
}