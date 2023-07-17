using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Player controller duh...

    private Rigidbody2D _rb; //Move
    private Vector2 _velocity, _inputMovement; //Move
    [SerializeField, Range(0f, 20f)] private float _speed; //Move

    private Bullet _bullet; //Shoot
    [SerializeField, Range(0f, 2f)]  private float _fireRate; //Shoot
    private float _nextFire; //Shoot
    [SerializeField] private int _bulletLimit;

    private float _reloadTime; //Reload
    private TextMesh _numBulletText;
    private bool isReloading = false; //Reload

    // Start is called before the first frame update
    void Start()
    {
        _speed = 6f;
        _fireRate = 0.5f;
        _nextFire = 0f;
        _bulletLimit = GameManager.SharedInstance.NumEnemiesAndBullets;
        _reloadTime = 3f;

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
        _bullet = ObjectPool.SharedInstance.GetPooledObject("Bullet");

        if (_bullet != null)
        {
            //Direction/Who shoot/Activate
            _bullet.transform.position = transform.position;
            _bullet.Direction("Player", 8f);
            _bullet.gameObject.SetActive(true);
        }

        if (_bulletLimit <= 0)
        {
            isReloading = !isReloading;
            StartCoroutine("Reload");
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

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadTime);
        _bulletLimit = GameManager.SharedInstance.NumEnemiesAndBullets;
        _numBulletText.text = _bulletLimit.ToString();
        yield return new WaitForSeconds(1f);
        _numBulletText.text = "";
        isReloading = !isReloading;
    }
}
