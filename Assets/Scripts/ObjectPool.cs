using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Create bullets when game start to toggle it on and off instead of Instance and Destroy.

    public static ObjectPool SharedInstance;
    //[SerializeField] private List<GameObject> _pooledObjects;
    [SerializeField] private List<Bullet> _pooledObjects;
    //[SerializeField] private GameObject _bullettToPool;
    [SerializeField] private Bullet _bullettToPool;
    [SerializeField] private Bullet _bullettEnemyToPool;
    [SerializeField] private Bullet _lasertEnemyToPool;
    [SerializeField, Range(0f, 1000f)] private int _amount;

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
    }

    private void Start()
    {
        //Create list of GameObject
        //_pooledObjects = new List<GameObject>();
        _pooledObjects = new List<Bullet>();
        //GameObject tmp;
        Bullet tmp;

        //Create bullets for player
        for (int i = 0; i < _amount; i++)
        {
            tmp = Instantiate(_bullettToPool, transform);
            tmp.gameObject.SetActive(false);
            _pooledObjects.Add(tmp);
        }
        //Create bullets for enemies
        for (int i = 0; i < _amount; i++)
        {
            tmp = Instantiate(_bullettEnemyToPool, transform);
            tmp.gameObject.SetActive(false);
            _pooledObjects.Add(tmp);
        }
        //Create laser for enemies
        for (int i = 0; i < _amount; i++)
        {
            tmp = Instantiate(_lasertEnemyToPool, transform);
            tmp.gameObject.SetActive(false);
            _pooledObjects.Add(tmp);
        }
    }
    
    //Return bullet
    public Bullet GetPooledObject(string item)
    {
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            
            if (!_pooledObjects[i].gameObject.activeInHierarchy && _pooledObjects[i].tag == item)
            {
                return _pooledObjects[i];
            }
        }
        return null;
    }
}
