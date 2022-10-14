using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    //Pool enemies

    public static EnemyPool sharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject skullToPool; //Start
    public GameObject groupSkullsToPool; //Start
    public GameObject spiderToPool; //Start
    public List<GameObject> spawnPoints; //SpawnEnemy

    private GameObject enemy; //SpawnEnemy
    private int amount; //Start

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
        //Create list of GameObject
        pooledObjects = new List<GameObject>();
        GameObject tmp;

        //Create Skull
        amount = 10;
        if(skullToPool != null)
        {
            for (int i = 0; i < amount; i++)
            {
                tmp = Instantiate(skullToPool, transform);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }

        //Create Group Skulls
        amount = 5;
        if (groupSkullsToPool != null)
        {
            for (int i = 0; i < amount; i++)
            {
                tmp = Instantiate(groupSkullsToPool, transform);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }

        //Create Spider
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
    
    //Spawn the enemies in RANDOM spawn points
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
