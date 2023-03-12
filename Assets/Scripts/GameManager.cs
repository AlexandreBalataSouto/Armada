using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //The game manager duh...
    public static GameManager SharedInstance;

    [SerializeField] public int NumEnemiesAndBullets { get; private set; } = 0; // REMOVE ?
    //Position
    private Camera _cam;
    private Vector2 _startPositionEnemy;
    public Vector2 EndPositionEnemy { get; private set; }
    private float _positionCorrection = 4f;
    
    //Enemy pool
    [SerializeField] private EnemyPool _enemyPool;
    private GameObject _enemy;
    private float _enemyRate;
    private float _nextEnemy = 0f;

    //Level schema
    [SerializeField] private LevelSchema _levelSchema;

    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }

        _startPositionEnemy = GetStartOrEndPosition(true);
        EndPositionEnemy = GetStartOrEndPosition(false);

        //Set level
        string level = SceneManager.GetActiveScene().name;
        _levelSchema.SetLevelSchema(level);
        
        NumEnemiesAndBullets = _levelSchema.NumEnemiesAndBullets;
    }

    private void Start() {
         //Set enemies
        _enemyPool.SetEnemiesLevel(_levelSchema.Enemies);
    }
    
    private void Update() {
        
        _startPositionEnemy = GetStartOrEndPosition(true);
        EndPositionEnemy = GetStartOrEndPosition(false);
    }

    void FixedUpdate()
    {
        if(NumEnemiesAndBullets > 0)
        {
            if(Time.time > _nextEnemy )
            {
                GetEnemy();
            }
        }
    }

    //TODO check again
    private void GetEnemy()
    {
        _enemy = _enemyPool.GetPooledObject();

        if (_enemy != null)
        {
            //Direction/Who shoot/Activate
            _enemy.transform.position =  new Vector2(_startPositionEnemy.x, 0);
            _enemy.gameObject.SetActive(true);
        }
        //Rate of enemy
        _nextEnemy = Time.time + _levelSchema.EnemyRate;
    } 

    //TODO check again
    public void EnemyDestroy(GameObject enemyDestroyed)
    {
        NumEnemiesAndBullets--;
        int index = (int)_enemyPool.PooledObjects.FindIndex(a => a.name.Contains(enemyDestroyed.gameObject.name));
        _enemyPool.PooledObjects.RemoveAt(index);
    }

    //This is for EnemyCatcher and set the enemies in a start position
    private Vector2 GetStartOrEndPosition(bool flag)
    {
        Vector2 newVector2Position;

        if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return new Vector2(0,0); }
        _cam = Camera.main;
        if(flag)
        {
            newVector2Position = (Vector2)_cam.ScreenToWorldPoint(new Vector3(_cam.pixelWidth, 0, _cam.nearClipPlane));
            newVector2Position.x +=_positionCorrection;
            return newVector2Position;

        }else{
            newVector2Position = (Vector2)_cam.ScreenToWorldPoint(new Vector3(0, 0, _cam.nearClipPlane));
            newVector2Position.x += (_positionCorrection * -1);
            return newVector2Position;
        }
    }
}
