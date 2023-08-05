using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenFlame : MonoBehaviour
{
    private Bullet _bullet;
    private float _fireRate = 10f;
    private float _nextFire = 10f;

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponentInParent<Kraken>().IsInKrakenpPosition && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        //Get bullet
        _bullet = ObjectPool.SharedInstance.GetPooledObject("Flame_Kraken");

        if (_bullet != null)
        {
            //Direction/Position who shoot/Activate
            _bullet.transform.position = transform.position;
            _bullet.Direction("Flame_Kraken", 10f);
            _bullet.gameObject.SetActive(true);
        }
    }
}
