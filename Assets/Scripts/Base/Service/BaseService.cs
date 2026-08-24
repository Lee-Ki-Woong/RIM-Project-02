using UnityEngine;

public abstract class BaseService
{
    protected void Log(string text)
    {
        Debug.Log($"{this} : " + text);
    }

    protected void LogWarning(string text)
    {
        Debug.LogWarning($"{this} : " + text);
    }

    protected void LogError(string text)
    {
        Debug.LogError($"{this} : " + text);
    }
}
