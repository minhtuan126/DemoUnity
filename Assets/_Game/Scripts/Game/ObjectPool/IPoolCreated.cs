using UnityEngine;

public interface IPoolCreated
{
    string Name { get; }
    void InitObjectPool(IPoolManager poolManager);

#if UNITY_EDITOR
    void UpdateName();
#endif
}
