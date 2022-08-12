using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float x, y;
    private Rigidbody2D rb;
    private Transform body;
    private GameObject bullet;
    [SerializeField, Range(0f, 20f)] private float speed = 8f;

    private Vector2 normal;

    [SerializeField, Range(0f, 1f)]  private float fireRate = 0.5f;
    private float nextFire = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        body = transform;
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

    private void Move()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if(x != 0f)
        {
            body.position += new Vector3(x * speed * Time.deltaTime, 0f, 0f);
        }

        if(y != 0f)
        {
            body.position += new Vector3(0f, y * speed * Time.deltaTime, 0f);
        }
    }
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            bullet = ObjectPool.sharedInstance.GetPooledObject("Bullet");

            if (bullet != null)
            {
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
