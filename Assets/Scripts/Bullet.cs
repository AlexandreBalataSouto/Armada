using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Bullet behaviour
    private float _speed = 0f; //Direction
    [SerializeField]  private bool _isBulletEnemy = false; //OnTriggerEnter2D
    [SerializeField] private bool _isLasertEnemy = false;
    private float _moveX = 0, _moveY = 0; //Direction

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move bullet
        transform.Translate(_moveX * _speed * Time.deltaTime, _moveY * _speed * Time.deltaTime, 0);
    }

    /*Check if bullet collide to:
     * Limit
     * Enemy
     * Player
     */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MainCamera" && _isLasertEnemy == false)
        {
            gameObject.SetActive(false);
        }
        if (other.tag == "Limit" && _isLasertEnemy == false)
        {
            gameObject.SetActive(false);
        }
        if (other.tag == "LimitLaser" && _isLasertEnemy == true)
        {
            gameObject.SetActive(false);
        }
        if (other.tag == "LaserEnemy" && _isBulletEnemy == false)
        {
            gameObject.SetActive(false);
        }
        if (other.tag == "Enemy" && _isBulletEnemy == false)
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            GameManager.SharedInstance.DestroyEnemy(other.gameObject);
        }
        if (other.tag == "Player" && _isBulletEnemy == true)
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    /* Change direction AND speed base on the user:
     * Player: right
     * Spider: left
     * Orb: all direction
     * Wasp: to the player
     * Laser: down
     */
    public void Direction(string user, float otherSpeed, Vector3 otherTransform = default(Vector3))
    {
        switch (user)
        {
            case "Player":

                _moveX = 1f;
                _moveY = 0f;
                _speed = otherSpeed;
            break;

            case "Spider":

                _moveX = -1f;
                _moveY = 0f;
                _speed = otherSpeed;
            break;

            case "Orb":

                _moveX = otherTransform.x;
                _moveY = otherTransform.y;
                _speed = otherSpeed;
            break;

            case "Wasp":

                Vector3 normalize = transform.position - otherTransform;
                normalize = normalize.normalized;
                _moveX = normalize.x * -1;
                _moveY = normalize.y * -1;
                _speed = otherSpeed;
            break;

            case "Laser":
                _moveX = 0f;
                _moveY = -1f;
                _speed = otherSpeed;
                break;
        }
    }
}
