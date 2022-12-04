using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Bullet behaviour

    public float speed = 0f; //Direction
    public bool isBulletEnemy = false; //OnTriggerEnter2D
    public float x = 0, y = 0; //Direction


    // Update is called once per frame
    void Update()
    {
        // Move bullet
        transform.Translate(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0);
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
            //EnemyPool.sharedInstance.pooledObjects.Remove(other.gameObject);

            //gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
        if (other.tag == "Player" && isBulletEnemy == true)
        {
            other.gameObject.SetActive(false);
        }
    }

    /* Change direction AND speed base on the user:
     * Player: right
     * Spider: left
     * Orb: all direction
     * Wasp: to the player
     */
    public void Direction(string user, float otherSpeed, Vector3 otherTransform = default(Vector3))
    {
        switch (user)
        {
            case "Player":

                x = 1f;
                y = 0f;
                speed = otherSpeed;
            break;

            case "Spider":

                x = -1f;
                y = 0f;
                speed = otherSpeed;
            break;

            case "Orb":

                x = otherTransform.x;
                y = otherTransform.y;
                speed = otherSpeed;
            break;

            case "Wasp":

                Vector3 normalize = transform.position - otherTransform;
                normalize = normalize.normalized;
                x = normalize.x * -1;
                y = normalize.y * -1;
                speed = otherSpeed;

            break;
        }
    }
}
