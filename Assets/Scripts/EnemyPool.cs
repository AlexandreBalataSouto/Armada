using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool sharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject skullToPool;
    public GameObject groupSkullsToPool;
    public GameObject spiderToPool;
    public List<GameObject> spawnPoints;

    private GameObject enemy;
    private int amount;


    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        amount = 10;
        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(skullToPool, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        amount = 5;
        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(groupSkullsToPool, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        /*
        amount = 1;
        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(spiderToPool, transform);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        */
    }

    void FixedUpdate()
    {
        StartCoroutine("SpawnEnemy");
    }
    

    IEnumerator SpawnEnemy()
    {
        int originalLength = pooledObjects.Count;
        int indexSpawnPoint;

        if (pooledObjects.Count > 0)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                yield return new WaitForSeconds(1f);

                if (pooledObjects.Count == originalLength && !pooledObjects[i].activeInHierarchy)
                {
                    enemy = pooledObjects[i];

                    if (enemy != null)
                    {
                        indexSpawnPoint = Random.Range(0, spawnPoints.Count);

                        enemy.transform.position = spawnPoints[indexSpawnPoint].transform.position;
                        enemy.SetActive(true);
                    }
                }
            }
        }
    }


  
}
