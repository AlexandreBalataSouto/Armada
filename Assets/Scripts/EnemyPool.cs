using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelSchema;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] public List<GameObject> PooledObjects;
    [SerializeField] private GameObject _skullToPool;
    [SerializeField] private GameObject _weirdSkullToPool;
    private Dictionary<string,GameObject> mapEnemies = new Dictionary<string,GameObject>();

    private void Awake() {
        mapEnemies.Add(_skullToPool.name,_skullToPool);
        mapEnemies.Add(_weirdSkullToPool.name,_weirdSkullToPool);
    }

    //Return enemy
    public GameObject GetPooledObject(int indexEnemy)
    {
        GameObject enemy = null;

        if(PooledObjects[indexEnemy] != null && !PooledObjects[indexEnemy].gameObject.activeInHierarchy)
        {
            enemy = PooledObjects[indexEnemy];
        }

        return enemy;
    }

    public void SetEnemiesLevel(List<Enemy> Enemies)
    {
        //Create list of GameObject
        PooledObjects = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < Enemies.Count; i++)
        {
            tmp = Instantiate(mapEnemies[Enemies[i].nameEnemy], transform);
            tmp.name = Enemies[i].nameEnemy + "_" +i;
            tmp.SetActive(false);
            PooledObjects.Add(tmp);
        }
    }
}
