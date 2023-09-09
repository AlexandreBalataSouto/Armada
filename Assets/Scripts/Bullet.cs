﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Bullet behaviour
    private float _speed; //Direction
    private float _moveX, _moveY; //Direction
    private Vector2 _direction;
    private float _angle;
    private float _degrees;

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
        if (other.tag == Constants.Common.MAIN_CAMERA &&
          gameObject.tag != Constants.Common.LASER_ENEMY &&
          gameObject.tag != Constants.Common.FLAME_KRAKEN)
        {
            gameObject.SetActive(false);
        }
        if (other.tag == Constants.Common.LIMIT ||
          other.tag == Constants.Common.LIMIT_LASER ||
          other.tag == Constants.Common.LIMIT_FLAME)
        {
            gameObject.SetActive(false);
        }
        if (other.tag == Constants.Common.LASER_ENEMY &&
          gameObject.tag != Constants.Common.FLAME_KRAKEN)
        {
            gameObject.SetActive(false);
        }
        if(gameObject.tag == Constants.Common.BULLET &&
          other.tag == Constants.Common.FLAME_KRAKEN)
        {
            gameObject.SetActive(false);
        }
        if (gameObject.tag == Constants.Common.BULLET &&
          (other.tag == Constants.Common.ENEMY || other.tag == Constants.Common.ENEMY_KRAKEN))
        {
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
            GameManager.SharedInstance.DestroyEnemy(other.gameObject);
        }
        if ( gameObject.tag == Constants.Common.BULLET_ENEMY &&
          other.tag == Constants.Common.PLAYER &&
          gameObject.tag != Constants.Common.LASER_ENEMY &&
          gameObject.tag != Constants.Common.FLAME_KRAKEN)
        {
            gameObject.SetActive(false);
        }
    }

    /* Change _direction AND _speed base on the user:
     * Player: right
     * Spider/Flame_Kraken: left
     * Orb: all _direction
     * Wasp: to the player
     * Laser: down
     */
    public void Direction(string user, float otherSpeed, Transform otherTransform = default(Transform))
    {
        switch (user)
        {
            case Constants.Common.PLAYER:
                _moveX = 1f;
                _moveY = 0f;
                _speed = otherSpeed;
            break;
            case Constants.Common.SPIDER:
            case Constants.Common.FLAME_KRAKEN:
                _moveX = -1f;
                _moveY = 0f;
                _speed = otherSpeed;
            break;
            case Constants.Common.ORB:
                _moveX = otherTransform.localPosition.x;
                _moveY = otherTransform.localPosition.y;
                _speed = otherSpeed;

                _direction = transform.position - otherTransform.localPosition;
                _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                _degrees = _angle - Mathf.Atan2(_moveX * -1, _moveY * -1) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, _degrees);
            break;
            case Constants.Common.WASP:
                _direction = transform.position - otherTransform.position;
                _moveX = _direction.normalized.x * -1;
                _moveY = _direction.normalized.y * -1;
                _speed = otherSpeed;

                _degrees = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, _degrees);
            break;
            case Constants.Common.WARLOCK:
                _direction = transform.position - otherTransform.position;
                _moveX = _direction.normalized.x * -1;
                _moveY = _direction.normalized.y * -1;
                _speed = otherSpeed;

                _degrees = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, _degrees);
            break;
            case Constants.Common.LASER_ENEMY:
                _moveX = 0f;
                _moveY = -1f;
                _speed = otherSpeed;
            break;
        }
    }
}
