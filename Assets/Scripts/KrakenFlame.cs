using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenFlame : MonoBehaviour
{
    private Bullet _bullet;
    private float _fireRate;
    private float _nextFire;
    
    void Start() {
        _fireRate = Constants.Kraken_Flame.FIRE_RATE;
        _nextFire = Constants.Kraken_Flame.NEXT_FIRE;
    }

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
        _bullet = ObjectPool.SharedInstance.GetPooledObject(Constants.Common.FLAME_KRAKEN);

        if (_bullet != null)
        {
            //Direction/Position who shoot/Activate
            _bullet.transform.position = transform.position;
            _bullet.Direction(Constants.Common.FLAME_KRAKEN, Constants.Bullet.KRAKEN_FLAME_SPEED);
            _bullet.gameObject.SetActive(true);
        }
    }
}
