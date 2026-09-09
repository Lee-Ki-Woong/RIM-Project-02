public enum Language
{
    Korean,
    English,
}

public enum SkinCategory
{
    Hair,
    Eyebrows,
    Eyes,
    Mouth,
    Cloth,
    Body,
    Weapon,
}

public enum IdValidationResult
{
    Valid,
    Empty,
    TooShort,
    TooLong,
    InvalidFormat,
}

public enum PasswordValidationResult
{
    Valid,
    Empty,
    TooShort,
    TooLong,
    InvalidFormat,
}

public enum LoginResult
{
    Success,
    Failed,
}

public enum CreateAccountResult
{
    Success,
    InvalidId,
    InvalidPassword,
    PasswordMismatch,
    AlreadyExists,
}

public enum PlayerAnimationState
{
    Idle_Relaxed,
    Idle_Combat,
    Walk_Relaxed,
    Walk_Combat,
    Skill,
}

public enum SkillCategory
{
    None,
    Attack,
    Skill,
    Ultimate,
}