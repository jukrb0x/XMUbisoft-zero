using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private bool poolCanExpand = true;
    private GameObject parentObject;

    private List<GameObject> pooledObjects;

    private void Start()
    {
        parentObject = new GameObject("PlayerPool");
        Refill();
    }

    // 创建池
    public void Refill()
    {
        pooledObjects = new List<GameObject>();
        for (var i = 0; i < poolSize; i++) AddObjectToPool();
    }

    // 从池中返回一个对象
    public GameObject GetObjectFromPool()
    {
        // 如果我们发现一个禁用项，则从池中返回一个对象
        for (var i = 0; i < pooledObjects.Count; i++)
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];

        // 如果需要更多的对象，扩展池
        if (poolCanExpand) return AddObjectToPool();

        return null;
    }

    // 向池中添加一个对象
    public GameObject AddObjectToPool()
    {
        var newObject = Instantiate(objectPrefab);
        newObject.SetActive(false);
        newObject.transform.parent = parentObject.transform;

        pooledObjects.Add(newObject);
        return newObject;
    }
}
