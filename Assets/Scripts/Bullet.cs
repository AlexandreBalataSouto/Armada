using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    public bool isBulletEnemy = false;

    // Update is called once per frame
    void Update()
    {
        if(isBulletEnemy == false)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Translate((speed * Time.deltaTime) * -1, 0, 0);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Limit")
        {
            gameObject.SetActive(false);
        }
        if(other.tag == "Enemy" && isBulletEnemy == false)
        {
            EnemyPool.sharedInstance.pooledObjects.Remove(other.gameObject);

            gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
        if (other.tag == "Player" && isBulletEnemy == true)
        {
            other.gameObject.SetActive(false);
        }
    }
}
