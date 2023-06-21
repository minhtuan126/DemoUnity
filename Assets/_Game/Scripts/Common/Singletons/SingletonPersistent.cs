using UnityEngine;

/// <summary>
/// Persistent singleton.
/// </summary>
public class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    protected static T _instance;
    protected static object locker = new object();

    public static bool HasInstance
    {
        get { return _instance != null; }
    }

    /// <summary>
    /// Singleton design pattern
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (locker)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        _instance = obj.AddComponent<T>();
                        obj.name = typeof(T).Name;
                    }
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// On awake, we check if there's already a copy of the object in the scene. If there's one, we destroy it.
    /// </summary>
    protected virtual void Awake()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            return;
        }
#endif


        lock (locker)
        {
            if (_instance == null)
            {
                //If I am the first instance, make me the Singleton
                _instance = this as T;
                DontDestroyOnLoad(transform.gameObject);
            }
            else
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
            {
				enabled = false;
                Destroy(this);
            }
        }
    }
}
