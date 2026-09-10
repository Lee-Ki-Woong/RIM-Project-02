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

[Serializable]
public class CharacterCreateUIDataBase : UIDataBase
{
    public string DefaultPlayerName;
}

[Serializable]
public class CharacterSkillUIDataBase : UIDataBase
{
    public string CoolTime;
    public string CoolTimeUnit;
    public string DamageRatio;
}

[Serializable]
public class CharacterStatUIDataBase : UIDataBase
{
    public string PanelName;
    public string Level;
    public string CurrentHp;
    public string MaxHp;
    public string AttackDamage;
    public string AttackSpeed;
    public string Armour;
    public string CritRate;
    public string CritDamage;
    public string MoveSpeed;
}

[Serializable]
public class MainMenuUIDataBase : UIDataBase
{
    public string GameOptionButton;
    public string LogoutButton;
}

[Serializable]
public class GameOptionUIDataBase : UIDataBase
{
    public string PopupName;

    public string SoundCategoryButton;
    public string VideoCategoryButton;
    public string LanguageCategoryButton;

    public string SoundPanelName;
    public string MasterVolume;
    public string BgmVolume;
    public string SfxVolume;

    public string VideoPanelName;
    public string Resolution;
    public string FullScreen;
    public string TargetFrameRate;
    public string VSync;

    public string LanguagePanelName;
    public string LanguageSetting;

    public string LanguageSelectPanelName;
    public string CancelButton;
}