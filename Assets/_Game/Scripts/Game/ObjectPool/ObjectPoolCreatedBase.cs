using UnityEngine;
using System;

public class ObjectPoolCreatedBase<T> : MonoBehaviour where T : Enum
{
    #region Members and Properties
    public GameObject originObjects;

    [ReadOnly]
    public T objectPoolType;

    [Header("Init count, defualt = 10")]
    [Range(1, 50)]
    [SerializeField] private int _initCount = 10;

    public string Name { get; protected set; }


    #endregion // Member and Properties
    ///////////////////////////////////////////////////////////////////////////
    #region MonoBehaviour APIs

    protected virtual IPoolManager GetPoolManager()
    {
        IPoolManager poolManager = transform.root.GetComponent<PoolManager>();
        return poolManager;
    }

    /// Use this for initialization
    // void Awake()
    public virtual void InitObjectPool(IPoolManager poolManager)
    {
        if (originObjects == null || objectPoolType == null)
        {
#if USE_DEBUG
            Debug.LogError("Pool : " + gameObject.name + " is NULL prefab refrence");
#endif
            Destroy(this);
            return;
        }

#if !UNITY_EDITOR
        originObjects.SetActive(false);
#endif

        _initCount = _initCount == 0 ? 5 : _initCount;

        if (poolManager == null)
        {
#if USE_DEBUG
            Debug.LogError("Pool manager is NULL refrence");
#endif
            Destroy(this);
            return;
        }
        // Init new  pool
        poolManager.InitPool(originObjects, objectPoolType, transform, _initCount);

        Destroy(this);
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        UpdateName();
    }
    public virtual void UpdateName()
    {
        if (originObjects != null)
        {
            string objName = originObjects.name;
            gameObject.name = objName;
            try
            {
                objectPoolType = (T)Enum.Parse(typeof(T), objName);
            }
            catch
            {
                objectPoolType = default(T);
            }

            Name = objName;
        }
    }

#endif

    #endregion //     MonoBehaviour APIs
    ///////////////////////////////////////////////////////////////////////////
    #region Class methods


    #endregion Class methods
}

