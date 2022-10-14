using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Create bullets when game start to toggle it on and off instead of Instance and Destroy.

    public static ObjectPool sharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject bullettToPool;
    public GameObject bullettEnemyToPool;
    private int amount = 1000;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    private void Start()
    {
        //Create list of GameObject
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        //Create bullets for player
        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(bullettToPool, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        //Create bullets for enemies
        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(bullettEnemyToPool, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    
    //Return bullet
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
