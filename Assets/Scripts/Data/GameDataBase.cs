using System;

[Serializable]
public class GameDataBase
{
    public string Id;
}

[Serializable]
public class SkinDataBase : GameDataBase
{
    public string SkinCategory;
    public string SkinName;
    public string SkinDescription;
    public string SkinIconAddress;
}