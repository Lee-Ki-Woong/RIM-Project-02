using UnityEngine;

public class BaseMonoManager<T> : MonoBehaviour where T : BaseMonoManager<T>
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        InitSingleTon();
        AwakeAction();
    }

    private void InitSingleTon()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void AwakeAction()
    {
        // Awake에서 추가 작업이 필요할 경우 여기에 작성
    }
}