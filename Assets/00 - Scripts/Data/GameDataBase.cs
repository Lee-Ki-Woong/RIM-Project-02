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
    public string SkinIconAddress;

    public string SkillId_Attack;
    public string SkillId_Skill;
    public string SkillId_Ultimate;

    public bool IsCreateSelectable;

    public int MaxHp;
    public int AttackDamage;
    public float AttackSpeed;
    public int Armour;
    public float CritRate;
    public float CritDamage;
    public float MoveSpeed;
}

[Serializable]
public class SkinLocalizedDataBase : GameDataBase
{
    public string SkinName;
    public string SkinDescription;
}

[Serializable]
public class PlayerLevelStatDataBase : GameDataBase
{
    public int Level;
    public int RequiredExp;

    public int MaxHp;
    public int AttackDamage;
    public float AttackSpeed;
    public int Armour;
    public float CritRate;
    public float CritDamage;
    public float MoveSpeed;
}

[Serializable]
public class SkillDataBase : GameDataBase
{
    public string AnimationName;
    public string NextComboSkillId;
    public string SkillIconAddress;

    public float CoolTime;
    public float DamageRatio;
}

[Serializable]
public class SkillLocalizedDataBase : GameDataBase
{
    public string SkillName;
    public string SkillDescription;
}