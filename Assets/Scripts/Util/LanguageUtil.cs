using UnityEngine;

public static class LanguageUtil
{
    public static string GetLanguage(Language language)
    {
        switch (language)
        {
            case Language.English:
                {
                    return "English";
                }
            case Language.Korean:
                {
                    return "Korean";
                }
            default:
                {
                    Debug.LogWarning($"GameUtil : 잘못된 [Enum : Language] 값이 들어왔습니다!! 값 : {language}");
                    return "Korean";
                }
        }
    }

    public static Language GetLanguage(string language)
    {
        switch (language)
        {
            case "English":
                {
                    return Language.English;
                }
            case "Korean":
                {
                    return Language.Korean;
                }
            default:
                {
                    Debug.LogWarning($"GameUtil : 잘못된 [string : Language] 값이 들어왔습니다!! 값 : {language}");
                    return Language.Korean;
                }
        }
    }
}
