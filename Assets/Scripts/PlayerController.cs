using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player controller duh...
    private Rigidbody2D _rb; //Move
    private Vector2 _velocity, _inputMovement; //Move
    private Bullet _bullet; //Shoot
    private TextMesh _numBulletText;
    private float _speed; //Move
    private float _fireRate; //Shoot
    private float _nextFire; //Shoot
    private float _reloadTime; //Reload
    private int _bulletLimit;
    private bool isReloading = false; //Reload

    // Start is called before the first frame update
    void Start()
    {
        _nextFire = 0f;
        _speed = Constants.Player.SPEED;
        _fireRate = Constants.Player.FIRE_RATE;
        _bulletLimit = GameManager.SharedInstance.NumEnemiesAndBullets;
        _reloadTime = Constants.Player.RELOAD_TIME;
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _velocity = new Vector2(_speed, _speed);
        _numBulletText = gameObject.GetComponentInChildren<TextMesh>();
        _numBulletText.text = "";
    }

    void Update()
    {
        _inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire && _bulletLimit > 0)
        {
            Shoot();
        }
        
        if(Time.time > _nextFire && !isReloading)
        {
            _numBulletText.text = "";
        }

        if(_inputMovement.x != 0 || _inputMovement.y !=0)
        {
            Move();
        }
    }

    //Move player in any direction
    private void Move()
    {
        Vector2 delta = _inputMovement * _velocity * Time.deltaTime;
        Vector2 newPosition = _rb.position + delta;
        _rb.MovePosition(newPosition);
    }

    //Shoot bullet
    private void Shoot()
    {
        //One less bullet
        _bulletLimit--;
        _numBulletText.text = _bulletLimit.ToString();

        //Rate of fire
        _nextFire = Time.time + _fireRate;

        //Get bullet
        _bullet = ObjectPool.SharedInstance.GetPooledObject(Constants.Common.BULLET);

        if (_bullet != null)
        {
            //Direction/Who shoot/Activate
            _bullet.transform.position = transform.position;
            _bullet.Direction(Constants.Common.PLAYER, Constants.Bullet.PLAYER_SPEED);
            _bullet.gameObject.SetActive(true);
        }

        if (_bulletLimit <= 0)
        {
            isReloading = !isReloading;
            StartCoroutine(Reload());
        }
    }

    //Die if touch enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Constants.Common.ENEMY ||
            other.tag == Constants.Common.BULLET_ENEMY ||
            other.tag == Constants.Common.LASER_ENEMY ||
            other.tag == Constants.Common.FLAME_KRAKEN ||
            other.tag == Constants.Common.LIMIT)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == Constants.Common.LIMIT)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator Reload()
    {
        _numBulletText.text = "R";
        yield return new WaitForSeconds(_reloadTime);
        _bulletLimit = GameManager.SharedInstance.NumEnemiesAndBullets;
        _numBulletText.text = _bulletLimit.ToString();
        yield return new WaitForSeconds(1f);
        _numBulletText.text = "";
        isReloading = !isReloading;
    }
}
