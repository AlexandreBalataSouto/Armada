using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    //Enemy
    //Moves to one of the two points and start shooting

    [SerializeField, Range(0f, 20f)] private float _speed = 5f;
    [SerializeField] private List<GameObject> _waspPoints;
    private int _waspPosition;
    private bool _isInWaspPosition = false;
    //private GameObject bullet;
    private Bullet _bullet;
    [SerializeField, Range(0f, 1f)] private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;
    [SerializeField] private Transform _playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        _waspPosition = GetWaspPoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, _waspPoints[_waspPosition].transform.position, _speed * Time.fixedDeltaTime);

        if(transform.position.x <= _waspPoints[_waspPosition].transform.position.x && _isInWaspPosition == false)
        {
            _isInWaspPosition = true;
        }

        if(_isInWaspPosition == true && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Shoot();
        }
    }


    private int GetWaspPoint()
    {
        int indexWaspPoint = 0;

        if (_waspPoints.Count > 0)
        {
            indexWaspPoint = Random.Range(0, _waspPoints.Count);
        }

        return indexWaspPoint;
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
