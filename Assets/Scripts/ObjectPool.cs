using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool sharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject bullettToPool;
    public GameObject bullettEnemyToPool;
    private int amount = 100;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(bullettToPool, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(bullettEnemyToPool, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    
    public GameObject GetPooledObject(string item)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == item)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    
}
