using UnityEngine;

public class SingletonScene<T> : MonoBehaviour where T : Component
{
    protected static T _instance;
    protected static object locker = new object();

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

    public static T GetInstance() { return _instance; }

    /// <summary>
    /// On awake, we initialize our instance. Make sure to call base.Awake() in override if you need awake.
    /// </summary>
    protected virtual void Awake()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        lock (locker)
        {
            if (_instance == null)
            {
                //If I am the first instance, make me the Singleton
                _instance = this as T;
            }
            else
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
            {
                Destroy(gameObject);
            }
        }
    }
}
