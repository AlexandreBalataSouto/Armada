using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockTurret : MonoBehaviour
{

    [SerializeField, Range(0f, 10f)] private float _speed = 4f;

    //Shoot
    private Bullet _bullet;
    [SerializeField, Range(0f, 4f)] private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;
    private Transform _playerPosition;
    private Vector3 _turretPosition;

    private Vector3 _difference; //Turret orientation
    private float _rotationZ = 0f; //Turret orientation
    private float _turretRotationAux = 90f; //Turret orientation

    [SerializeField] private bool _upOrDown = false;
    private bool _isInPosition = false;

    void Start()
    {
        _playerPosition = GameObject.FindWithTag("Player").transform;
        //TODO Set a place in the Scene
        if(_upOrDown)
        {
            _turretPosition = new Vector3(-8, 4, 0); //Up
        }
        else
        {
            _turretPosition = new Vector3(-8, -4, 0); //Down
        }
    }

    void Update()
    {
        if (gameObject.GetComponentInParent<Warlock>().IsDeployTurretUp && _upOrDown)
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
        else if (gameObject.GetComponentInParent<Warlock>().IsDeployTurretDown && !_upOrDown)
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
        _bullet = ObjectPool.SharedInstance.GetPooledObject("BulletEnemy");

        if (_bullet != null)
        {
            //Direction/Position who shoot/Activate
            _bullet.transform.position = transform.position;
            _bullet.Direction("Wasp", 10f, _playerPosition);
            _bullet.gameObject.SetActive(true);
        }
    }
}
