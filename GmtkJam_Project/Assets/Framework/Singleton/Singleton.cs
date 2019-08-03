using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance { get; set; }

    public static T Instance()
    {
        _instance = FindObjectOfType<T>();
        if (_instance == null)
        {
            _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
        }
        return _instance;
    }
}