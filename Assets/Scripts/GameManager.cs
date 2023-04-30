using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static LevelSchema;
public class GameManager : MonoBehaviour
{
    //The game manager duh...
    public static GameManager SharedInstance;

    [SerializeField] public int NumEnemiesAndBullets { get; private set; } = 0;
    //Position
    private Camera _cam;
    private Vector2 _startPositionEnemy;
    public Vector2 EndPositionEnemy { get; private set; }
    private float _positionCorrection = 4f;

    //Enemy pool
    [SerializeField] private EnemyPool _enemyPool;
    private GameObject _enemy;

    //Level schema
    [SerializeField] private LevelSchema _levelSchema;

    //SpawnEnemy
    private IEnumerator thisCoroutine;
    private int _indexEnemy = 0;
    private bool _isEnemyDestroy = false;

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

    private void Start() {
        thisCoroutine = SpawnEnemy();
        StartCoroutine(thisCoroutine);
    }

    IEnumerator SpawnEnemy()
    {
        if(_isEnemyDestroy == false && NumEnemiesAndBullets > 0)
        {
            if(_indexEnemy >= _levelSchema.Enemies.Count)
            {
                _indexEnemy = 0;
            }

            string id = _levelSchema.Enemies[_indexEnemy].idEnemy;
            if(id != null)
            {
                _enemy = _enemyPool.GetPooledObject(id);
            }

            yield return new WaitForSeconds(_levelSchema.Enemies[_indexEnemy].intervalEnemy);

            if (_enemy != null)
            {
                //Direction/Who shoot/Activate
                _enemy.transform.position =  new Vector2(_startPositionEnemy.x, 0);
                _enemy.gameObject.SetActive(true);
            }

            _indexEnemy++;

            if(_enemy != null && _enemy.name == _levelSchema.Enemies[_levelSchema.Enemies.Count - 1].idEnemy)
            {
               yield return new WaitForSeconds(2f);
            }

            thisCoroutine = SpawnEnemy();
            StartCoroutine(thisCoroutine);
        }
    }

    public void DestroyEnemy(GameObject enemy)
    {
        StopCoroutine(thisCoroutine);
        _isEnemyDestroy = true;
        _levelSchema.Enemies.Remove(_levelSchema.Enemies.Find((item) => item.idEnemy == enemy.name));
        NumEnemiesAndBullets--;
        _enemyPool.PooledObjects.Remove(enemy);
        _isEnemyDestroy = false;
        StartCoroutine(thisCoroutine);
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
