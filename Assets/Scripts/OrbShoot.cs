using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbShoot : MonoBehaviour
{
    //Allows orb to shoots
    private Bullet _bullet;
    private float _fireRate;
    private float _nextFire;

    void Start()
    {
        _fireRate = Constants.Orb.FIRE_RATE;
        _nextFire = Constants.Orb.NEXT_FIRE;
    }

    void Update()
    {
        //Shoot and rate of fire
        if(gameObject.GetComponentInParent<Orb>().IsShootingEven 
            && (gameObject.name == Constants.Orb.ORB_SHOOT[0]
            || gameObject.name == Constants.Orb.ORB_SHOOT[2]
            || gameObject.name == Constants.Orb.ORB_SHOOT[4]
            || gameObject.name == Constants.Orb.ORB_SHOOT[6]))
        {
            if (Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;
                Shoot();
            }
        }

        if (gameObject.GetComponentInParent<Orb>().IsShootingOdd
                    && (gameObject.name == Constants.Orb.ORB_SHOOT[1]
                    || gameObject.name == Constants.Orb.ORB_SHOOT[3]
                    || gameObject.name == Constants.Orb.ORB_SHOOT[5]
                    || gameObject.name == Constants.Orb.ORB_SHOOT[7]))
        {
            if (Time.time > _nextFire)
            {
                _nextFire = Time.time + _fireRate;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        //Get bullet
        _bullet = ObjectPool.SharedInstance.GetPooledObject(Constants.Common.BULLET_ENEMY);

        if (_bullet != null)
        {
            //Direction/Position who shoot/Activate
            _bullet.transform.position = transform.position;
            _bullet.Direction(Constants.Common.ORB, Constants.Bullet.ORB_SHOOT_SPEED, transform);
            _bullet.gameObject.SetActive(true);
        }
    }
}
