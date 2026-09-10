public static class PlayerStatExtensions
{
    public static PlayerStat ToPlayerStat(this PlayerLevelStatDataBase levelData)
    {
        PlayerStat playerStat = new();

        playerStat.MaxHp = levelData.MaxHp;
        playerStat.AttackDamage = levelData.AttackDamage;
        playerStat.AttackSpeed = levelData.AttackSpeed;
        playerStat.Armour = levelData.Armour;
        playerStat.CritRate = levelData.CritRate;
        playerStat.CritDamage = levelData.CritDamage;
        playerStat.MoveSpeed = levelData.MoveSpeed;

        return playerStat;
    }

    public static PlayerStat ToPlayerStat(this SkinDataBase skinData)
    {
        PlayerStat playerStat = new();

        playerStat.MaxHp = skinData.MaxHp;
        playerStat.AttackDamage = skinData.AttackDamage;
        playerStat.AttackSpeed = skinData.AttackSpeed;
        playerStat.Armour = skinData.Armour;
        playerStat.CritRate = skinData.CritRate;
        playerStat.CritDamage = skinData.CritDamage;
        playerStat.MoveSpeed = skinData.MoveSpeed;

        return playerStat;
    }
}