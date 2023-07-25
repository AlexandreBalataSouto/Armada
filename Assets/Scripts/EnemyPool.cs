using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelSchema;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] public List<GameObject> PooledObjects;
    [SerializeField] private GameObject _skullToPool;
    [SerializeField] private GameObject _weirdSkullToPool;
    [SerializeField] private GameObject _spiderToPool;
        [SerializeField] private GameObject _waspToPool;
    [SerializeField] private GameObject _orbToPool;
    private Dictionary<string,GameObject> mapEnemies = new Dictionary<string,GameObject>();

    private void Awake() {
        mapEnemies.Add(_skullToPool.name,_skullToPool);
        mapEnemies.Add(_weirdSkullToPool.name,_weirdSkullToPool);
        mapEnemies.Add(_spiderToPool.name,_spiderToPool);
        mapEnemies.Add(_waspToPool.name,_waspToPool);
        mapEnemies.Add(_orbToPool.name,_orbToPool);
    }

    //Return enemy
    public GameObject GetPooledObject(string idEnemy)
    {
        GameObject enemy = null;

        for(int i = 0; i < PooledObjects.Count; i++)
        {
            if(PooledObjects[i].name == idEnemy
            && PooledObjects[i] != null
            && !PooledObjects[i].gameObject.activeInHierarchy)
            {
                enemy = PooledObjects[i];
            }
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
            tmp.name = Enemies[i].idEnemy;
            tmp.SetActive(false);
            PooledObjects.Add(tmp);
        }
    }
}
