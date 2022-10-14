using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
 
    //Player controller duh...

    private float x, y; //Move
    private Transform body; //Move
    private GameObject bullet; //Shoot
    [SerializeField, Range(0f, 20f)] private float speed = 8f; //Move
    [SerializeField, Range(0f, 1f)]  private float fireRate = 0.5f; //Shoot
    private float nextFire = 0.0f; //Shoot

    // Start is called before the first frame update
    void Start()
    {
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

    //Move player in any direction
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

    //Shoot bullet
    private void Shoot()
    {
        //When press space and rate of fire
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            //Rate of fire
            nextFire = Time.time + fireRate;

            //Get bullet
            bullet = ObjectPool.sharedInstance.GetPooledObject("Bullet");

            if (bullet != null)
            {
                //Direction/Position who shoot/Activate
                bullet.transform.position = transform.position;
                bullet.GetComponent<Bullet>().Direction("Player", transform.position.x, transform.position.y);
                bullet.SetActive(true);
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
