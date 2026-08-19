using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static void ActiveTrue(this MonoBehaviour monoBehaviour)
    {
        monoBehaviour.gameObject.SetActive(true);
    }

    public static void ActiveFalse(this MonoBehaviour monoBehaviour)
    {
        monoBehaviour.gameObject.SetActive(false);
    }
}
