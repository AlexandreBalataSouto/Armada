using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbShoot : MonoBehaviour
{
    //Allows orb to shoots
    private Bullet _bullet;
    private float _fireRate = 0.25f;
    private float _nextFire = 0.0f;

    void Update()
    {
        //Shoot and rate of fire
        if(gameObject.GetComponentInParent<Orb>().IsShootingEven 
            && (gameObject.name == "Orb_shoot" 
            || gameObject.name == "Orb_shoot_2" 
            || gameObject.name == "Orb_shoot_4" 
            || gameObject.name == "Orb_shoot_6"))
        {
            if (Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;
                Shoot();
            }
        }

        if (gameObject.GetComponentInParent<Orb>().IsShootingOdd
                    && (gameObject.name == "Orb_shoot_1"
                    || gameObject.name == "Orb_shoot_3"
                    || gameObject.name == "Orb_shoot_5"
                    || gameObject.name == "Orb_shoot_7"))
        {
            if (Time.time > _nextFire) //delta?
            {
                _nextFire = Time.time + _fireRate;
                Shoot();
            }
        }

    }

    private void Shoot()
    {
        //Get bullet
        _bullet = ObjectPool.SharedInstance.GetPooledObject("BulletEnemy");

        if (_bullet != null)
        {
            //Direction/Position who shoot/Activate
            _bullet.transform.position = transform.position;
            _bullet.Direction("Orb", 6f, transform);
            _bullet.gameObject.SetActive(true);
        }
    }
}
