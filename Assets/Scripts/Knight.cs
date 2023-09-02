using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    //Enemy
    //Moves in different patterns and after a few secons charge to the player

    private Vector3 _distance, _movementFrequency, _startPosition, _goToPlayer, _difference;
    private bool _isFireRateChanged = false;
    private bool _isMoving = false;
    private bool _isReturning = false;
    private bool _isAttacking = false;
    private bool _isInKnightPosition = false;
    private float _fireRate;
    private float _nextFire = 0f;
    private float _speedAttack;
    private float _timer = 0f, _timerAux = 0f; //Compesate time
    private float _rotationZ = 0f; //_spear orientation
    private float _spearRotationAux; //_spear orientation
    private float _playerX = 0f, _playerY = 0f, _spearX = 0f, _spearY = 0f, _spearAux; //Draw _spear
    private int _indexPattern = Constants.Knight.RANGE_INDEX_PATTERN[0];
    private Transform _playerPosition, _stopPointKnight;
    private Transform _spear, _shield; //_spear and _shield transform

    void Awake() {
        _fireRate = Constants.Knight.FIRE_RATE;
        _speedAttack = Constants.Knight.SPEED_ATTACK;
        _spearRotationAux = Constants.Knight.SPEAR_ROTATION_AUX;
        _spearAux = Constants.Knight.SPEAR_AUX;
    }

    void Start()
    {
        _stopPointKnight = GameObject.FindWithTag(Constants.Common.STOP_POINT_KNIGHT).transform;
        _playerPosition = GameObject.FindWithTag(Constants.Common.PLAYER).transform;

        //Get spear and shield (Children)
        _spear = gameObject.transform.Find(Constants.Common.SPEAR);
        _shield = gameObject.transform.Find(Constants.Common.SHIELD);

        _indexPattern = Random.Range(Constants.Knight.RANGE_INDEX_PATTERN[0],
          Constants.Knight.RANGE_INDEX_PATTERN[1]);

        if (_indexPattern == 1)
        {
            _distance.x = Constants.Knight.Pattern.Bretzel.DISTANCE_X;
            _distance.y = Constants.Knight.Pattern.Bretzel.DISTANCE_Y;
            _movementFrequency.x = Constants.Knight.Pattern.Bretzel.MOVE_FREQ_X;
            _movementFrequency.y = Constants.Knight.Pattern.Bretzel.MOVE_FREQ_Y;
        }
        if (_indexPattern == 2)
        {
            _distance.x = Constants.Knight.Pattern.Pottery.DISTANCE_X;
            _distance.y = Constants.Knight.Pattern.Pottery.DISTANCE_Y;
            _movementFrequency.x = Constants.Knight.Pattern.Pottery.MOVE_FREQ_X;
            _movementFrequency.y = Constants.Knight.Pattern.Pottery.MOVE_FREQ_Y;
        }
        if (_indexPattern == 3)
        {
            _distance.x = Constants.Knight.Pattern.Attom.DISTANCE_X;
            _distance.y = Constants.Knight.Pattern.Attom.DISTANCE_Y;
            _movementFrequency.x = Constants.Knight.Pattern.Attom.MOVE_FREQ_X;
            _movementFrequency.y = Constants.Knight.Pattern.Attom.MOVE_FREQ_Y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_isInKnightPosition == false)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                _stopPointKnight.position, _speedAttack * Time.deltaTime
            );

            if(transform.position.x <= _stopPointKnight.position.x)
            {
                _isInKnightPosition = true;
                //From your position start moving
                _startPosition = transform.position;
                //Start moving and after a few secons attack
                _isMoving = true;
                _nextFire = Time.time + _fireRate;
                _timerAux = Time.time;
            }
        }
        
        if(_isInKnightPosition == true)
        {
            //Fire rate random
            if (!_isFireRateChanged)
            {
                _fireRate = Mathf.Round(Random.Range(Constants.Knight.RANGE_FIRE_RATE[0],
                  Constants.Knight.RANGE_FIRE_RATE[1]));
                _isFireRateChanged = !_isFireRateChanged;
            }

            //Move logic
            if (_isMoving)
            {
                transform.position = Move();
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
                transform.position = Vector2.MoveTowards(transform.position, _goToPlayer, _speedAttack * Time.deltaTime);

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
                transform.position = Vector2.MoveTowards(transform.position, _startPosition, _speedAttack * Time.deltaTime);

                //_spear orientation
                _spear.rotation = Quaternion.Euler(0f, 0f, 0f);
                //END _spear orientation

                //Return _spear to 0,0,0
                _spear.localPosition = new Vector3(
                  Constants.Knight.SPEAR_LOCAL_POSITION.X,
                  Constants.Knight.SPEAR_LOCAL_POSITION.Y,
                  Constants.Knight.SPEAR_LOCAL_POSITION.Z
                );

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

    private Vector2 Move() 
    {
        Vector2 pos;
        _timer = Time.time - _timerAux;
        pos.x = _startPosition.x + Mathf.Sin(_timer * _movementFrequency.x) * _distance.x;
        pos.y = _startPosition.y + Mathf.Sin(_timer * _movementFrequency.y) * _distance.y;
        return pos;
    }
}