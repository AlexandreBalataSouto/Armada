using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbShoot : MonoBehaviour
{
    //Allows orb to shoot

    private GameObject bullet;
    public bool isShooting = false;
    [SerializeField, Range(0f, 1f)] private float fireRate = 0.25f;
    private float nextFire = 0.0f;

    void FixedUpdate()
    {
        //Shoot and rate of fire
        if(isShooting && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
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
            bullet.GetComponent<Bullet>().Direction("Enemy02",transform.position.x,transform.position.y);
            bullet.SetActive(true);
        }

    }

}
