using System;
using System.Collections.Generic;

[Serializable]
public class GameSettingData
{
    public string Language;

    public int ScreenWidth;
    public int ScreenHeight;
    public bool IsFullScreen;

    public int TargetFrameRate;
    public bool IsVSync;

    public float MasterVolume;
    public float BgmVolume;
    public float SfxVolume;

    public bool IsRememberId;
    public string RememberedId;
}

[Serializable]
public class AccountData
{
    public string Id;
    public string PasswordHash;
    public string PlayerUID;
    public GameSaveData GameSaveData = new();
}

[Serializable]
public class GameSaveData
{
    public PlayerData PlayerData = new();
}

[Serializable]
public class PlayerData
{
    public string PlayerName;

    public int Exp;
    public int CurrentHp;

    public List<string> OwnedSkins = new();
    public List<EquippedSkin> EquippedSkins = new();
}

[Serializable]
public class EquippedSkin
{
    public string Category;
    public string SkinId;

    public EquippedSkin() { }
    public EquippedSkin(string category, string skinId)
    {
        Category = category;
        SkinId = skinId;
    }
}