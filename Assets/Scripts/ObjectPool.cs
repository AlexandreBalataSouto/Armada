using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Create bullets when game start to toggle it on and off instead of Instance and Destroy.
    public static ObjectPool SharedInstance;
    private List<Bullet> _pooledObjects = new List<Bullet>();
    private List<Bullet> _bulletList = new List<Bullet>();
    [SerializeField] private Bullet _bullettToPool, _bullettEnemyToPool, _lasertEnemyToPool, _flameEnemyToPool;
    private int _amount;

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
        _amount = Constants.Common.OBJECT_AMOUNT;
    }

    private void Start()
    {
        _bulletList.Add(_bullettToPool);
        _bulletList.Add(_bullettEnemyToPool);
        _bulletList.Add(_lasertEnemyToPool);
        _bulletList.Add(_flameEnemyToPool);

        //Create list of GameObject
        Bullet tmp;

        for (int i = 0; i < _bulletList.Count; i++)
        {
            for (int j = 0; j < _amount; j++)
            {
                //Create bullets for player/enemies/laser/flame for Kraken
                tmp = Instantiate(_bulletList[i], transform);
                tmp.gameObject.SetActive(false);
                _pooledObjects.Add(tmp);
            }
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
