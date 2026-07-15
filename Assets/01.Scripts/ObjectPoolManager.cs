using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager instance;

    private Dictionary<Component, object> poolDictionary = new Dictionary<Component, object>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else Destroy(gameObject);
        
    }

    public T Get<T>(T prefab, Vector3 position, Quaternion rotation,int defaultCapacity = 10, int maxSize = 50) where T : Component
    {
        if (prefab == null) return null;

        if (!poolDictionary.ContainsKey(prefab))
        {
            CreateNewPool(prefab,defaultCapacity,maxSize);
        }
        IObjectPool<T> pool = (IObjectPool<T>)poolDictionary[prefab];

        T obj = pool.Get();

        obj.transform.SetPositionAndRotation(position, rotation);

        return obj;
    }

    public void CreateNewPool<T>(T prefab, int defaultCapacity =10 , int maxSize = 50) where T : Component
    {
        var newPool = new ObjectPool<T>(
            createFunc: () => Instantiate(prefab, transform),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
            );
        poolDictionary.Add(prefab, newPool);
    }

    public void returnObject<T>(T prefab, T obj) where T : Component
    {
        if (poolDictionary.TryGetValue(prefab, out object poolObj))
        {
            IObjectPool<T> pool = (IObjectPool<T>)poolObj;
            pool.Release(obj);
        } 
        else
        {
            Destroy(obj.gameObject);
        }
    }
}

