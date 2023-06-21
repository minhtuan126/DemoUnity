using System;
using UnityEngine;
[DisallowMultipleComponent]
public class ObjectPoolElement : MonoBehaviour
{
    #region Members and Properties

    public Enum objectType;


    #endregion // Member and Properties
    #region Class methods


    public virtual void OnReturnToPool()
    {
        if (PoolManager.GetInstance() != null)
            PoolManager.ReturnObjectToPool(gameObject, objectType);
    }

    protected void OnParticleSystemStopped()
    {
        OnReturnToPool();
    }

    #endregion Class methods
}
