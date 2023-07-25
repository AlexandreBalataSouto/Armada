using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    //Enemy
    //Moves to one of the two points and start shooting

    [SerializeField, Range(0f, 20f)] private float _speed = 10f;
    private Vector2 _waspPosition;
    private bool _isInWaspPosition = false;
    //private GameObject bullet;
    private Bullet _bullet;
    [SerializeField, Range(0f, 1f)] private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;
    private Transform _playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        _waspPosition = GetWaspPoint();
        _playerPosition = GameObject.FindWithTag("Player").transform;
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
        GameObject parentObject = GameObject.FindWithTag("StopPoints");
        int randomChildIndex = Random.Range(0, parentObject.transform.childCount);
        Transform randomChildTransform = parentObject.transform.GetChild(randomChildIndex);
        return randomChildTransform.position;
    }

    private void Shoot()
    {
        //Get bullet
        _bullet = ObjectPool.SharedInstance.GetPooledObject("BulletEnemy");

        if (_bullet != null)
        {
            //Direction/Position who shoot/Activate
            _bullet.transform.position = transform.position;
            //bullet.GetComponent<Bullet>().Direction("Wasp",10f, playerPosition.position);
            _bullet.Direction("Wasp", 10f, _playerPosition.position);
            _bullet.gameObject.SetActive(true);
        }

    }

}
