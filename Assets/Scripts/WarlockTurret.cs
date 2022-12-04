using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockTurret : MonoBehaviour
{
    public GameObject target;

    private GameObject bullet;
    [SerializeField, Range(0f, 4f)] private float fireRate = 0.25f;
    private float nextFire = 0.0f;

    public GameObject playerPosition;

    private Vector3 difference; //Turret orientation
    private float rotationZ = 0f; //Turret orientation
    private float turretRotationAux = 90f; //Turret orientation

    public bool isDeployTurret = false;
    public bool upOrDown = false;

    // Start is called before the first frame update
    void Start()
    {
        //if(upOrDown == true)
        //{
        //    transform.localPosition = new Vector3(-8, 4, 0);
        //}
        //else
        //{
        //    transform.localPosition = new Vector3(-8, -4, 0);
        //}
    }

    void Update()
    {
        if(isDeployTurret)
        {
            if (upOrDown == true)
            {
                transform.localPosition = new Vector3(-8, 4, 0);
            }
            else
            {
                transform.localPosition = new Vector3(-8, -4, 0);
            }

            difference = playerPosition.transform.position - transform.position;
            rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - turretRotationAux);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isDeployTurret)
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }
        }
        else
        {
            nextFire = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        //Get bullet
        bullet = ObjectPool.sharedInstance.GetPooledObject("BulletEnemy");

        if (bullet != null)
        {
            //Direction/Position who shoot/Activate
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().Direction("Wasp", 10f, playerPosition.transform.position);
            bullet.SetActive(true);
        }

    }
}
