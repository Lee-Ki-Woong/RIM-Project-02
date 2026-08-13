using UnityEngine;

public static class ComponentExtension
{
    public static void Log(this Component component, string text)
    {
        Debug.Log($"{component.gameObject.name}.{component.GetType().Name} : {text}", component);
    }

    public static void LogWarning(this Component component, string text)
    {
        Debug.LogWarning($"{component.gameObject.name}.{component.GetType().Name} : {text}", component);
    }

    public static void LogError(this Component component, string text)
    {
        Debug.LogError($"{component.gameObject.name}.{component.GetType().Name} : {text}", component);
    }
}
