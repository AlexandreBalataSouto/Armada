using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPoint : MonoBehaviour
{
    //Hidden turret that shoots a laser beam from top to bottom

    private Bullet _bullet; //Shoot

    //If the player is under the LaserPoint_8 will shoot a laser after X seconds
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && gameObject.GetComponentInParent<LaserCoordinator>().IsSecretShootActive)
        {
            StartCoroutine("SecretShoot");
        }
    }

    IEnumerator SecretShoot()
    {
        yield return new WaitForSeconds(1.5f);
        Shoot();
    }

    public void Shoot()
    {
        ////Get bullet
        _bullet = ObjectPool.SharedInstance.GetPooledObject("LaserEnemy");

        if (_bullet != null)
        {
            //Direction/Position who shoot/Activate
            _bullet.transform.position = new Vector3(transform.position.x, (transform.position.y + 8f), 0f); //The laser goes above the rest.
            _bullet.Direction("Laser", 8f);
            _bullet.gameObject.SetActive(true);
        }
    }
}
