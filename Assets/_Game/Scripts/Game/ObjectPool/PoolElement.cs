using UnityEngine;
using System.Collections.Generic;
using System;

public class PoolElement
{
	protected static readonly Vector3 StartPosition = Vector3.zero;

	//public string poolName;
	public Enum objectType;

	public Transform rootTransform;

	public GameObject poolPrefab;

	protected int objectsInUse = 0;


	protected Queue<GameObject> availableObjStack = new Queue<GameObject>();

	protected List<GameObject> usingObjs = new List<GameObject>();

	public int TotalObjectCount { get; protected set; }

	public int initialCount
	{
		get; protected set;
	}

	public PoolElement(GameObject prefab, Transform rootTransform, Enum objectPoolType, int size = 10)
	{
		if (prefab == null)
		{
			return;
		}
		this.objectType = objectPoolType;
		poolPrefab = prefab;
		this.rootTransform = rootTransform;
		initialCount = Mathf.Max(size, 1);
		CreateObjectPool(size);
	}


	public PoolElement(GameObject prefab, Transform rootTransform, Enum objectPoolType, GameObject[] objects, int size = 10)
	{
		if (prefab == null)
		{
			return;
		}
		this.objectType = objectPoolType;
		poolPrefab = prefab;
		this.rootTransform = rootTransform;
		initialCount = Mathf.Max(size, 1);

		for (int index = 0; index < objects.Length; index++)
		{
			AddObjectToPool(objects[index]);
		}
	}

	public void CreateObjectPool(int count = 10)
	{
		if (count <= 0) return;
		initialCount = count;

		for (int index = 0; index < count; index++)
		{
			GameObject po = GameObject.Instantiate(poolPrefab);
			var obType = po.GetOrAddComponent<ObjectPoolElement>();
			obType.objectType = objectType;
			AddObjectToPool(po);
		}
	}

	protected void AddObjectToPool(GameObject poolObject)
	{
		//add to pool
		poolObject.transform.SetParent(null, true);
		poolObject.SetActive(false);
		poolObject.transform.SetParent(rootTransform, false);
		var objType = poolObject.GetOrAddComponent<ObjectPoolElement>();
		objType.objectType = objectType;

		if (!usingObjs.Remove(poolObject))
			TotalObjectCount++;

		availableObjStack.Enqueue(poolObject);
	}

	public GameObject NextObject(bool autoActive)
	{
		GameObject nextObj = null;
		if (availableObjStack.Count <= 1)
		{
			CreateObjectPool(initialCount);
		}

		nextObj = availableObjStack.Dequeue();
		if (nextObj != null)
		{
			objectsInUse++;
			usingObjs.Add(nextObj);

			if (autoActive)
			{
				nextObj.SetActive(true);
			}
		}
		nextObj.transform.position = StartPosition;

		// Create for next frame
		if (availableObjStack.Count == 0)
		{
			CreateObjectPool(initialCount);
		}

		return nextObj;
	}

	public void ReturnObjectToPool(GameObject obj)
	{
		objectsInUse--;
		AddObjectToPool(obj);
	}

	public void DestroyObjectPool()
	{
		objectsInUse--;
	}


	public void CleanPool()
	{
		while (availableObjStack.Count > 0)
		{
			var obj = availableObjStack.Dequeue();
			GameObject.Destroy(obj);
		}

		for (int i = usingObjs.Count - 1; i > 0; i--)
		{
			GameObject.Destroy(usingObjs[i]);
			usingObjs.RemoveAt(i);
		}

		TotalObjectCount = 0;
	}
}
