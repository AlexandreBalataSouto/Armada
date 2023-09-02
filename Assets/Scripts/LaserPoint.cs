using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPoint : MonoBehaviour
{
    //Hidden turret that shoots a laser beam from top to bottom
    private Bullet _bullet; //Shoot

    public void Shoot()
    {
        ////Get bullet
        _bullet = ObjectPool.SharedInstance.GetPooledObject(Constants.Common.LASER_ENEMY);

        if (_bullet != null)
        {
            //Direction/Position who shoot/Activate
            _bullet.transform.position = new Vector3(transform.position.x, transform.position.y + 8f, 0f); //The laser goes above the rest.
            _bullet.Direction(Constants.Common.LASER_ENEMY, Constants.Bullet.LASER_ENEMY_SPEED);
            _bullet.gameObject.SetActive(true);
        }
    }
}
