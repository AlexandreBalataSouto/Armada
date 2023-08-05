using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    //Enemy
    //Moves in different patterns and after a few secons charge to the player

    [SerializeField] private Vector3 _distance, _movementFrequency;
    private Vector3 _moveposition;
    private Vector3 _startPosition;

    private bool _isFireRateChanged = false;
    private bool _isMoving = false;
    private bool _isReturning = false;
    private bool _isAttacking = false;
    private bool _isInKnightPosition = false;

    [SerializeField, Range(0f, 10f)] private float _fireRate = 6f;
    private float _nextFire = 0.0f;
    [SerializeField, Range(0f, 20f)] private float _speedAttack = 15f;

    private Transform _playerPosition;
    private Transform _stopPointKnight;
    private Vector3 _goToPlayer;

    private float _timer = 0f, _timerAux = 0f; //Compesate time

    private Transform _spear, _shield; //_spear and _shield transform

    private Vector3 _difference; //_spear orientation
    private float _rotationZ = 0f; //_spear orientation
    private float _spearRotationAux = 90f; //_spear orientation

    private float _playerX = 0f, _playerY = 0f, _spearX = 0f, _spearY = 0f,_spearAux = 10f; //Draw _spear

    //Pattern TESTING -----> RANDOMIZE
    [SerializeField] private bool _bretzelPattern = false;
    [SerializeField] private bool _infinitePattern = false;
    [SerializeField] private bool _potteryPattern = false;
    [SerializeField] private bool _attomPattern = false;
    [SerializeField] private bool _zPattern = false;

    private bool[] _arr = new bool[6];
    private int _index = 0;

    void Start()
    {
        _stopPointKnight = GameObject.FindWithTag("StopPoint_Knight").transform;
        _playerPosition = GameObject.FindWithTag("Player").transform;

        //Get spear and shield (Children)
        _spear = gameObject.transform.Find("Spear");
        _shield = gameObject.transform.Find("Shield");

        _arr[0] = _bretzelPattern;
        _arr[1] = _infinitePattern;
        _arr[2] = _potteryPattern;
        _arr[3] = _attomPattern;
        _arr[4] = _zPattern;

        _index = Random.Range(1, _arr.Length);
        _arr[_index - 1] = true;

        if (_arr[0]) //_bretzelPattern
        {
            _distance.x = 4f;
            _distance.y = 2f;
            _movementFrequency.x = 3f;
            _movementFrequency.y = 4f;
        }
        if (_arr[1]) //_infinitePattern
        {
            _distance.x = 4f;
            _distance.y = 3f;
            _movementFrequency.x = 2f;
            _movementFrequency.y = 4f;
        }
        if (_arr[2]) //_potteryPattern
        {
            _distance.x = 3f;
            _distance.y = 3f;
            _movementFrequency.x = 4f;
            _movementFrequency.y = 1f;
        }
        if (_arr[3]) //_attomPattern
        {
            _distance.x = 5f;
            _distance.y = 3f;
            _movementFrequency.x = 2.5f;
            _movementFrequency.y = 2f;
        }
        if (_arr[4]) //_zPattern
        {
            _distance.x = 6f;
            _distance.y = 4f;
            _movementFrequency.x = 6f;
            _movementFrequency.y = 2f;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(_isInKnightPosition == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, _stopPointKnight.position, _speedAttack * Time.deltaTime);

            if(transform.position.x <= _stopPointKnight.position.x)
            {
                _isInKnightPosition = true;
                //From your position start moving
                _startPosition = transform.position;
                //Start moving and after a few secons attack
                _isMoving = true;
                _nextFire = Time.time + _fireRate;
            }
        }
        
        if(_isInKnightPosition == true)
        {
            //Fire rate random
            if (!_isFireRateChanged)
            {
                _fireRate = Mathf.Round(Random.Range(2f, 6f));
                _isFireRateChanged = !_isFireRateChanged;
            }

            //Move logic
            if (_isMoving) //TODO Fix jump position
            {
                _timer = Time.time - _timerAux;
                _moveposition.x = _startPosition.x + Mathf.Sin(_timer * _movementFrequency.x) * _distance.x;
                _moveposition.y = _startPosition.y + Mathf.Sin(_timer * _movementFrequency.y) * _distance.y;
                transform.position = new Vector3(_moveposition.x, _moveposition.y, 0f);

            }
            //Attack and rate of fire
            if (!_isAttacking && !_isReturning && Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;
                _isAttacking = !_isAttacking;
                _isMoving = !_isMoving;
                _goToPlayer = _playerPosition.position;

                //_spear orientation
                _difference = _playerPosition.position - transform.position;
                _rotationZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
                _spear.rotation = Quaternion.Euler(0f, 0f, _rotationZ - _spearRotationAux);
                //END _spear orientation

                //Draw the _spear
                _playerX = _playerPosition.position.x;
                _playerY = _playerPosition.position.y;
                _spearX = _spear.localPosition.x;
                _spearY = _spear.localPosition.y;
                _spear.localPosition = new Vector3((_playerX - _spearX) / _spearAux, (_playerY - _spearY) / _spearAux, 1f);
                //END Draw the _spear

                //_shield disabled
                _shield.gameObject.SetActive(false);
            }

            //Go towards the player
            if (_isAttacking)
            {
                transform.position = Vector2.MoveTowards(transform.position, _goToPlayer, _speedAttack * Time.deltaTime); //.deltaTime because .time it`s to fast

                //When reach the player position return to the start position
                if (transform.position == _goToPlayer)
                {
                    _isReturning = !_isReturning;
                    _isAttacking = !_isAttacking;
                }
            }

            //Return to the start position 
            if (_isReturning)
            {
                transform.position = Vector2.MoveTowards(transform.position, _startPosition, _speedAttack * Time.deltaTime); //.deltaTime because .time it`s to fast

                //_spear orientation
                _spear.rotation = Quaternion.Euler(0f, 0f, 0f);
                //END _spear orientation

                //Return _spear to 0,0,0
                _spear.localPosition = new Vector3(1f, 0.5f, 1f);

                //_shield actived
                _shield.gameObject.SetActive(true);

                //When reach the start position, start moving again
                if (transform.position == _startPosition)
                {
                    _isMoving = !_isMoving;
                    _isReturning = !_isReturning;
                    _isFireRateChanged = !_isFireRateChanged;
                    _nextFire = Time.time + _fireRate;
                    _timerAux = Time.time;
                }
            }
        }
    }
}

//Move up and down
//transform.position = new Vector3(x, maxMove * Mathf.Sin(Time.time * speed), 0);
//Make the character move in a circle like the Spider !!!
//transform.position = new Vector3(maxMove * Mathf.Sin(Time.time), maxMove * Mathf.Cos(Time.time), 0);
//Rotate
//transform.rotation = Quaternion.Euler(0f, 0f, maxMove * Mathf.Sin(Time.time * speed));