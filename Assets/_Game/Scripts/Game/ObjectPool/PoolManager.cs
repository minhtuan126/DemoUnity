using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

[DisallowMultipleComponent]
public class PoolManager : SingletonScene<PoolManager>, IPoolManager
{
	#region Members and Properties

	public ILogger logger = UnityEngine.Debug.unityLogger;

	protected Dictionary<Enum, PoolElement> poolDict = new Dictionary<Enum, PoolElement>();

	protected List<Enum> _listObjectTypeRegisted = new List<Enum>();

	public Dictionary<Enum, PoolElement> PoolDict
	{
		get { return poolDict; }
	}


	#endregion // Member and Properties
	///////////////////////////////////////////////////////////////////////////
	#region MonoBehaviour APIs
	protected override void Awake()
	{
		base.Awake();

		if (UnityEngine.Debug.isDebugBuild)
			//(default setting) will display all log messages.
			logger.filterLogType = LogType.Log;
		else
			//will display Assert, Error and Exception log messages.
			logger.filterLogType = LogType.Assert;
	}

	IEnumerator Start()
	{
		var poolObjects = GetComponentsInChildren<IPoolCreated>();
		foreach (var pool in poolObjects)
		{
			pool.InitObjectPool(this);
			yield return null;
		}
	}

	#endregion //     MonoBehaviour APIs
	///////////////////////////////////////////////////////////////////////////
	#region Class methods

	public void InitPool(GameObject objectOrigin, Enum objectPoolType, Transform root, int size = 10)
	{
		if (poolDict == null || poolDict.ContainsKey(objectPoolType))
		{
			return;
		}
		else
		{
			poolDict[objectPoolType] = new PoolElement(objectOrigin, root, objectPoolType, size);
		}
	}

	public void InitPool(GameObject objectOrigin, Enum objectPoolType, Transform objPool, GameObject[] objects, int size = 10)
	{
		if (poolDict == null || poolDict.ContainsKey(objectPoolType))
		{
			return;
		}
		else
		{
			poolDict[objectPoolType] = new PoolElement(objectOrigin, objPool, objectPoolType, objects, size);
		}
	}

	public static GameObject GetObjectFormPool(Enum objectPoolType, bool autoActive = true, int initCount = 10)
	{
		PoolElement pool = null;
		if (!Instance.poolDict.TryGetValue(objectPoolType, out pool))
		{
#if USE_DEBUG
			UnityEngine.Debug.LogWarning(objectPoolType.GetType().ToString() + "." + objectPoolType + " is null");
#endif
			return null;
		}

		if (pool != null)
		{
			return pool.NextObject(autoActive);
		}

#if USE_DEBUG
		Instance.logger.Log(LogType.Warning, "PoolManager", objectPoolType.GetType().ToString() + "." + objectPoolType + ": Wrong to get object from pool!!");
#endif

		return null;
	}

	/// <summary>
	/// Return obj to the pool
	/// </summary>
	/// <param name="go"></param>
	public static void ReturnObjectToPool(GameObject go, Enum type = default(Enum))
	{
		if (go == null)
		{
			Instance.logger.Log(LogType.Warning, "PoolManager", "Cant return null object !!");
			return;
		}
		else
		{
			if (type == default(Enum))
			{
				ObjectPoolElement el = go.GetComponent<ObjectPoolElement>();
				if (el == null) return;
				type = el.objectType;
			}


			PoolElement pool = null;
			if (Instance.poolDict.TryGetValue(type, out pool))
			{
				pool.ReturnObjectToPool(go);
				return;
			}
			else
			{
				Instance.logger.Log(LogType.Warning, "PoolManager", string.Format("Pool [{0}] is not found", go.name));
			}
		}
	}

	/// <summary>
	/// Return obj to the pool
	/// </summary>
	/// <param name="go"></param>
	public static void ReturnObjectToPool(ObjectPoolElement go)
	{
		if (go == null || go.objectType == default(Enum))
		{
			Instance.logger.Log(LogType.Warning, "PoolManager", "Cant return null object !!");
			return;
		}
		else
		{
			PoolElement pool = null;
			if (Instance.poolDict.TryGetValue(go.objectType, out pool))
			{
				pool.ReturnObjectToPool(go.gameObject);
				return;
			}
			else
			{
				Instance.logger.Log(LogType.Warning, "PoolManager", string.Format("Pool [{0}] is not found", go.name));
			}
		}
	}


	/// <summary>
	/// Return obj to the pool
	/// </summary>
	/// <param name="go"></param>
	public void DeleteObject(Enum objName)
	{
		PoolElement pool = null;
		if (poolDict.TryGetValue(objName, out pool))
		{
			pool.DestroyObjectPool();
			return;
		}
		else
		{
			logger.Log(LogType.Warning, "PoolManager", string.Format("Pool [{0}] is not found", objName));
		}
	}

	/// <summary>
	/// Create objects in "poolType" pool with "initialCount"
	/// </summary>
	/// /// <param name="poolType"></param>
	/// <param name="initialCount"></param>
	public void CreateObjectPools(Enum poolType, int initialCount = 10)
	{
		PoolElement pool = null;
		if (poolDict.TryGetValue(poolType, out pool) && pool.TotalObjectCount <= 0)
		{
			poolDict[poolType].CreateObjectPool(initialCount);
		}
	}

	public void OnCleanAllPool()
	{
		foreach (var poolKey in poolDict.Keys)
		{
			poolDict[poolKey].CleanPool();
		}
		// clear memory
		System.GC.Collect();
	}


	public void OnCleanPool(Enum keyPool)
	{
		PoolElement pool = null;
		if (poolDict.TryGetValue(keyPool, out pool))
			pool.CleanPool();
	}

	/// <summary>
	/// Register new types and clear pool not using in next stage
	/// </summary>
	/// <param name="poolTypes">List object pool type will using next stage</param>
	protected void OnRegisterObjectPool(List<Enum> poolTypes)
	{
		// Clear old pool
		foreach (var objType in _listObjectTypeRegisted)
		{
			if (!poolTypes.Contains(objType))
			{
				OnCleanPool(objType);
			}
		}

		// Create new pools
		foreach (var newType in poolTypes)
		{
			CreateObjectPools(newType);
		}

		_listObjectTypeRegisted.Clear();
		_listObjectTypeRegisted.AddRange(poolTypes);
	}

	#endregion Class methods
}


public interface IPoolManager
{
	void InitPool(GameObject objectOrigin, Enum objectPoolType, Transform objPool, GameObject[] objects, int size);

	void InitPool(GameObject objectOrigin, Enum objectPoolType, Transform root, int size);
}
