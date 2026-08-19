public enum Language
{
    Korean,
    English,
}

public enum SkinCategory
{
    Hair,
    EyeBrows,
    Eyes,
    Mouth,
    Cloth,
    Body,
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

public enum AccountResult
{
    Success,
    InvalidId,
    InvalidPassword,
    AlreadyExists,
    Failed,
}