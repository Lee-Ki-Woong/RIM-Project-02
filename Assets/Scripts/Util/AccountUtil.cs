using System;
using System.Security.Cryptography;
using System.Text;

public static class AccountUtil
{
    public const int IdMinLength = 4;
    public const int IdMaxLength = 16;

    public const int PasswordMinLength = 8;
    public const int PasswordMaxLength = 64;

    public const string PasswordSpecialCharacters = "!@#$%^&*()-_=+[]{};:,./<>?";

    public static IdValidationResult ValidateId(string id)
    {
        if(string.IsNullOrEmpty(id))
        {
            return IdValidationResult.Empty;
        }

        if(id.Length < IdMinLength)
        {
            return IdValidationResult.TooShort;
        }

        if(id.Length > IdMaxLength)
        {
            return IdValidationResult.TooLong;
        }

        foreach (char character in id)
        {
            bool isAllowed = IsIdCharacterValid(character);

            if (isAllowed == false)
            {
                return IdValidationResult.InvalidFormat;
            }
        }

        return IdValidationResult.Valid;
    }

    private static bool IsIdCharacterValid(char character)
    {
        if(character >= 'a' && character <= 'z')
        {
            return true;
        }

        if(character >= 'A' && character <= 'Z')
        {
            return true;
        }

        if(character >= '0' && character  <= '9')
        {
            return true;
        }

        return false;
    }

    public static PasswordValidationResult ValidatePassword(string password)
    {
        if(string.IsNullOrEmpty(password))
        {
            return PasswordValidationResult.Empty;
        }

        if(password.Length < PasswordMinLength)
        {
            return PasswordValidationResult.TooShort;
        }

        if (password.Length > PasswordMaxLength)
        {
            return PasswordValidationResult.TooLong;
        }

        foreach(char character in password)
        {
            bool isAllowed = IsPasswordCharacterValid(character);

            if(isAllowed == false)
            {
                return PasswordValidationResult.InvalidFormat;
            }
        }

        return PasswordValidationResult.Valid;
    }

    private static bool IsPasswordCharacterValid(char character)
    {
        if (character >= 'a' && character <= 'z')
        {
            return true;
        }

        if (character >= 'A' && character <= 'Z')
        {
            return true;
        }

        if (character >= '0' && character <= '9')
        {
            return true;
        }

        if (PasswordSpecialCharacters.IndexOf(character) >= 0)
        {
            return true;
        }

        return false;
    }

    public static string HashPassword(string password)
    {
        using SHA256 sha256 = SHA256.Create();

        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = sha256.ComputeHash(bytes);

        string hashPassword = Convert.ToBase64String(hash);

        return hashPassword;
    }
}
