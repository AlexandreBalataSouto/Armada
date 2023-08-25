using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Bullet behaviour
    [SerializeField]  private bool _isBulletEnemy = false;
    private float _speed = 0f; //Direction
    private float _moveX = 0f, _moveY = 0f; //Direction

    // Update is called once per frame
    void Update()
    {
        // Move bullet
        Vector2 movementDirection = new Vector2(_moveX, _moveY);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();
        transform.Translate(movementDirection * _speed * inputMagnitude * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MainCamera" && gameObject.tag != "LaserEnemy" && gameObject.tag != "Flame_Kraken")
        {
            gameObject.SetActive(false);
        }
        if (other.tag == "Limit" || other.tag == "LimitLaser" || other.tag == "Limit_Flame")
        {
            gameObject.SetActive(false);
        }
        if (other.tag == "LaserEnemy" && gameObject.tag != "Flame_Kraken")
        {
            gameObject.SetActive(false);
        }
        if(_isBulletEnemy == false && other.tag == "Flame_Kraken")
        {
            gameObject.SetActive(false);
        }
        if (_isBulletEnemy == false && (other.tag == "Enemy" || other.tag == "Enemy_Kraken"))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            GameManager.SharedInstance.DestroyEnemy(other.gameObject);
        }
        if ( _isBulletEnemy == true && other.tag == "Player" && gameObject.tag != "LaserEnemy"
          && gameObject.tag != "Flame_Kraken")
        {
            gameObject.SetActive(false);
        }
    }

    /* Change direction AND speed base on the user:
     * Player: right
     * Spider/Flame_Kraken: left
     * Orb: all direction
     * Wasp: to the player
     * Laser: down
     */
    public void Direction(string user, float otherSpeed, Transform otherTransform = default(Transform))
    {
        switch (user)
        {
            case "Player":
                _moveX = 1f;
                _moveY = 0f;
                _speed = otherSpeed;
            break;
            case "Spider":
            case "Flame_Kraken":
                _moveX = -1f;
                _moveY = 0f;
                _speed = otherSpeed;
            break;
            case "Orb":
                _moveX = otherTransform.localPosition.x;
                _moveY = otherTransform.localPosition.y;
                _speed = otherSpeed;

                Vector2 direction = transform.position - otherTransform.localPosition;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                float degrees = angle - Mathf.Atan2(_moveX * -1, _moveY * -1) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, degrees);

            break;
            case "Wasp":
                Vector3 normalize = transform.position - otherTransform.position;
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
