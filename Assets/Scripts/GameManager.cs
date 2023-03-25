using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static LevelSchema;
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

    private bool _isCourutineRunning = false;

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

         //Set enemies
        _enemyPool.SetEnemiesLevel(_levelSchema.Enemies);
    }
    
    private void Update() {
        
        _startPositionEnemy = GetStartOrEndPosition(true);
        EndPositionEnemy = GetStartOrEndPosition(false);
    }

    void FixedUpdate()
    {
         if(NumEnemiesAndBullets > 0 && _isCourutineRunning == false)
        {
            StartCoroutine(GetEnemy());
        }
    }

    IEnumerator GetEnemy()
    {
        _isCourutineRunning = true;
        int indexEnemy = 0;

        yield return new WaitForSeconds(2f);

        foreach(Enemy item in _levelSchema.Enemies)
        {
            yield return new WaitForSeconds(item.rateEnemy);

            if(indexEnemy > _enemyPool.PooledObjects.Count - 1)
            {   
                indexEnemy = 0;
            }
            _enemy = _enemyPool.GetPooledObject(indexEnemy);

            if (_enemy != null)
            {
                //Direction/Who shoot/Activate
                _enemy.transform.position =  new Vector2(_startPositionEnemy.x, 0);
                _enemy.gameObject.SetActive(true);
            }

            indexEnemy++;
        }
        _isCourutineRunning = false;
    }

    public void EnemyDestroy(GameObject enemyDestroyed)
    {
        //Adjust list and enemies queue
        _enemyPool.PooledObjects.Remove(enemyDestroyed);
        NumEnemiesAndBullets--;
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
