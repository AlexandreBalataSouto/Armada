using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelSchema;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] public List<GameObject> PooledObjects;
    [SerializeField] private GameObject _skullToPool;
    [SerializeField] private GameObject _groupSkullToPool;
    private Dictionary<string,GameObject> mapEnemies = new Dictionary<string,GameObject>();

    private int indexEnemy = 0;

    private void Awake() {
        mapEnemies.Add(_skullToPool.name,_skullToPool);
        mapEnemies.Add(_groupSkullToPool.name,_groupSkullToPool);
    }

    //Return enemy
    public GameObject GetPooledObject()
    {
        GameObject enemy = PooledObjects[indexEnemy];
        indexEnemy++;
        if(indexEnemy >= PooledObjects.Count)
        {
            indexEnemy = 0;
        }
        return enemy;
    }
    //TODO check again
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

    public void SetEnemiesLevel(List<Enemy> Enemies)
    {
        //Create list of GameObject
        PooledObjects = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < Enemies.Count; i++)
        {
            for (int j = 0; j < Enemies[i].amountEnemy; j++)
            {
                tmp = Instantiate(mapEnemies[Enemies[i].nameEnemy], transform);
                tmp.SetActive(false);
                PooledObjects.Add(tmp);
            }
        }
    }
}
