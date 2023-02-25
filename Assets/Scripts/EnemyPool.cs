using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] public List<GameObject> PooledObjects; //TODO check again
    [SerializeField] private GameObject _skullToPool;

    // Start is called before the first frame update
    void Start()
    {
        //Create list of GameObject
        PooledObjects = new List<GameObject>();
        GameObject tmp;

        //Create Skull
        if(_skullToPool != null)
        {
            for (int i = 0; i < GameManager.SharedInstance.NumEnemiesAndBullets; i++)
            {
                tmp = Instantiate(_skullToPool, transform);
                tmp.SetActive(false);
                PooledObjects.Add(tmp);
            }
        }
    }

    //Return enemy
    public GameObject GetPooledObject(string item)
    {
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            if (!PooledObjects[i].gameObject.activeInHierarchy && PooledObjects[i].gameObject.name.Contains(item))
            {
                return PooledObjects[i];
            }
        }
        return null;
    }

    // void FixedUpdate()
    // {
    //     StartCoroutine("SpawnEnemy");
    // }
    
    // //Spawn the enemies in RANDOM spawn points
    // IEnumerator SpawnEnemy()
    // {
    //     int originalLength = pooledObjects.Count;
    //     int indexSpawnPoint;

    //     if (pooledObjects.Count > 0)
    //     {
    //         for (int i = 0; i < pooledObjects.Count; i++)
    //         {
    //             yield return new WaitForSeconds(1f);

    //             if (pooledObjects.Count == originalLength && !pooledObjects[i].activeInHierarchy)
    //             {
    //                 enemy = pooledObjects[i];

    //                 if (enemy != null)
    //                 {
    //                     indexSpawnPoint = Random.Range(0, spawnPoints.Count);

    //                     enemy.transform.position = spawnPoints[indexSpawnPoint].transform.position;
    //                     enemy.SetActive(true);
    //                 }
    //             }
    //         }
    //     }
    // }

}
