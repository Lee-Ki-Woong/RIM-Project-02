using UnityEngine;

public abstract class BaseManager<T> where T : BaseManager<T>, new()
{
    public static T Instance { get; private set; }

    protected BaseManager()
    {
        InitSingleTon();
        InitAction();
    }

    public static void Create()
    {
        if (Instance != null)
        {
            return;
        }

        new T(); // 생성자를 이 메서드에서 호출하여 Instance가 생성됨
    }

    private void InitSingleTon()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            throw new System.Exception($"이미 존재하고 있는 싱글톤 인스턴스 입니다. {typeof(T)}");
        }
    }

    protected virtual void InitAction()
    {
        // Initialize 할 때 필요한 추가 작업이 있다면 여기에 작성
    }

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
