using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockTurret : MonoBehaviour
{
    private float _speed;
    private float _fireRate;
    private float _nextFire;
    private Bullet _bullet;
    private Transform _playerPosition;
    private Vector3 _turretPosition;
    private Vector3 _difference; //Turret orientation
    private float _rotationZ; //Turret orientation
    private float _turretRotationAux; //Turret orientation

    private bool _isInPosition = false;

    void Start()
    {
        _playerPosition = GameObject.FindWithTag(Constants.Common.PLAYER).transform;
        _speed = Constants.WarlockTurret.SPEED;
        _fireRate = Constants.WarlockTurret.FIRE_RATE;
        _nextFire = Constants.WarlockTurret.NEXT_FIRE;
        _rotationZ = 0f;
        _turretRotationAux = Constants.WarlockTurret.TURRET_ROTATION_AUX;

        //TODO Set a place in the Scene
        if(gameObject.tag == Constants.Common.UP)
        {
            _turretPosition = new Vector3(
              Constants.WarlockTurret.TURRET_UP.X,
              Constants.WarlockTurret.TURRET_UP.Y,
              Constants.WarlockTurret.TURRET_UP.Z
            );
        }
        else
        {
            _turretPosition = new Vector3(
              Constants.WarlockTurret.TURRET_DOWN.X,
              Constants.WarlockTurret.TURRET_DOWN.Y,
              Constants.WarlockTurret.TURRET_DOWN.Z
            );
        }
    }

    void Update()
    {
        if (gameObject.GetComponentInParent<Warlock>().IsDeployTurretUp && gameObject.tag == Constants.Common.UP)
        {
            transform.position = Vector2.MoveTowards(transform.position, _turretPosition, _speed * Time.deltaTime);

            if(transform.position == _turretPosition)
            {
                _isInPosition = true;
            }

            _difference = _playerPosition.position - transform.position;
            _rotationZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, _rotationZ - _turretRotationAux);
        }
        else if (gameObject.GetComponentInParent<Warlock>().IsDeployTurretDown && gameObject.tag == Constants.Common.DOWN)
        {
            transform.position = Vector2.MoveTowards(transform.position, _turretPosition, _speed * Time.deltaTime);

            if (transform.position == _turretPosition)
            {
                _isInPosition = true;
            }
             _difference = _playerPosition.position - transform.position;
            _rotationZ = Mathf.Atan2(_difference.y, _difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, _rotationZ - _turretRotationAux);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isInPosition)
        {
            if (Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;
                Shoot();
            }
        }
        else
        {
            _nextFire = Time.time + _fireRate;
        }
    }

    private void Shoot()
    {
        //Get bullet
        _bullet = ObjectPool.SharedInstance.GetPooledObject(Constants.Common.BULLET_ENEMY);

        if (_bullet != null)
        {
            //Direction/Position who shoot/Activate
            _bullet.transform.position = transform.position;
            _bullet.Direction(Constants.Common.WARLOCK, Constants.Bullet.WARLOCK_SPEED, _playerPosition);
            _bullet.gameObject.SetActive(true);
        }
    }
}
