using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
 
    //Player controller duh...

    private float _moveX, _moveY; //Move
    //private Transform body; //Move
    //private GameObject _bullet; //Shoot
    private Bullet _bullet; //Shoot
    [SerializeField, Range(0f, 20f)] private float _speed = 8f; //Move
    [SerializeField, Range(0f, 2f)]  private float _fireRate = 0.5f; //Shoot
    private float _nextFire = 0.0f; //Shoot

    // Start is called before the first frame update
    void Start()
    {
        //body = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        Shoot();
    }

    //Move player in any direction
    private void Move()
    {
        _moveX = Input.GetAxisRaw("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical");

        if(_moveX != 0f)
        {
            transform.position += new Vector3(_moveX * _speed * Time.fixedDeltaTime, 0f, 0f);
        }

        if(_moveY != 0f)
        {
            transform.position += new Vector3(0f, _moveY * _speed * Time.fixedDeltaTime, 0f);
        }
    }

    //Shoot bullet
    private void Shoot()
    {
        //When press space and rate of fire
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            //Rate of fire
            _nextFire = Time.time + _fireRate;

            //Get bullet
            _bullet = ObjectPool.SharedInstance.GetPooledObject("Bullet");

            if (_bullet != null)
            {
                //Direction/Who shoot/Activate
                _bullet.transform.position = transform.position;
                _bullet.Direction("Player", 8f);
                //_bullet.GetComponent<Bullet>().Direction("Player", 8f);
                _bullet.gameObject.SetActive(true);
            }
        }
    }

    //Die if touch enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
