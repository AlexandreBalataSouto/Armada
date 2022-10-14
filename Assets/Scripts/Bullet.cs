using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Bullet behaviour

    public float speed = 8f; //Direction
    public bool isBulletEnemy = false; //OnTriggerEnter2D
    public float x = 0, y = 0; //Direction


    // Update is called once per frame
    void Update()
    {
        // Move bullet
        transform.Translate(x * Time.deltaTime, y * Time.deltaTime, 0);
    }

    /*Check if bullet collide to:
     * Limit
     * Enemy
     * Player
     */
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

    /* Change direction base on the user:
     * Player: right
     * Enemy01(Spider): left
     * Enemy02(Orb): all direction
     */
    public void Direction(string user, float xOther, float yOther)
    {
        switch(user)
        {
            case "Player":
                x = speed;
            break;

            case "Enemy01":
                x = speed * -1;
            break;

            case "Enemy02":
                x = speed * xOther;
                y = speed * yOther;
            break;

        }
    }
}
