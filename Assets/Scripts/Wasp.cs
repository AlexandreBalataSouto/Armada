using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    //Enemy
    //Moves to one of the two points and start shooting

    private float _speed = 10f;
    private Vector2 _waspPosition;
    private bool _isInWaspPosition = false;
    //private GameObject bullet;
    private Bullet _bullet;
    private float _fireRate;
    private float _nextFire;
    private Transform _playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        _speed = Constants.Wasp.SPEED;
        _fireRate = Constants.Wasp.FIRE_RATE;
        _nextFire = 0f;
        _waspPosition = GetWaspPoint();
        _playerPosition = GameObject.FindWithTag(Constants.Common.PLAYER).transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _waspPosition, _speed * Time.deltaTime);

        if(transform.position.x <= _waspPosition.x && _isInWaspPosition == false)
        {
            _isInWaspPosition = true;
        }

        if(_isInWaspPosition == true && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Shoot();
        }
    }

    private Vector2 GetWaspPoint()
    {
        GameObject parentObject = GameObject.FindWithTag(Constants.Common.STOP_POINTS);
        int randomChildIndex = Random.Range(0, parentObject.transform.childCount);
        Transform randomChildTransform = parentObject.transform.GetChild(randomChildIndex);
        return randomChildTransform.position;
    }

    private void Shoot()
    {
        //Get bullet
        _bullet = ObjectPool.SharedInstance.GetPooledObject(Constants.Common.BULLET_ENEMY);

        if (_bullet != null)
        {
            //Direction/Position who shoot/Activate
            _bullet.transform.position = transform.position;
            _bullet.Direction(Constants.Common.WASP, Constants.Bullet.WASP_SPEED, _playerPosition);
            _bullet.gameObject.SetActive(true);
        }
    }
}
