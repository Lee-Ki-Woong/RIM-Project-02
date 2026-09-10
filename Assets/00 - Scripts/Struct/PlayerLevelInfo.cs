public readonly struct PlayerLevelInfo
{
    public readonly int Level;
    public readonly int CurrentLevelExp;
    public readonly int CurrentLevelRequiredExp;
    public readonly bool IsMaxLevel;

    public PlayerLevelInfo(int level, int currentLevelExp, int currentLevelRequiredExp, bool isMaxLevel)
    {
        Level = level;
        CurrentLevelExp = currentLevelExp;
        CurrentLevelRequiredExp = currentLevelRequiredExp;
        IsMaxLevel = isMaxLevel;
    }

    public float GetProgress()
    {
        if (IsMaxLevel || CurrentLevelRequiredExp <= 0)
        {
            return 1f;
        }

        float expProgress = (float)CurrentLevelExp / CurrentLevelRequiredExp;

        return expProgress;
    }
}
