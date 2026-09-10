using UnityEngine;

public abstract class BaseMonoManager<T> : MonoBehaviour where T : BaseMonoManager<T>
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        InitSingleTon();
        AwakeAction();
    }

    private void OnDestroy()
    {
        OnDestroyAction();

        if(ReferenceEquals(Instance, this))
        {
            Instance = null;
        }
    }

    private void InitSingleTon()
    {
        if (Instance == null)
        {
            Instance = this as T;

            if (transform.parent == null)
            {
                DontDestroyOnLoad(gameObject);
            }
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
    
    protected virtual void OnDestroyAction()
    {
        // OnDestroy에서 추가 작업이 필요할 경우 여기에 작성
    }
}