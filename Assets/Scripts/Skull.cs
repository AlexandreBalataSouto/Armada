using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    //Enemy
    //Move to the left

    [SerializeField, Range(0f, 20f)] private float _speed = 5f;

    void Update()
    {
        Vector2 pos = transform.position;

        pos.x -= _speed * Time.deltaTime;

        transform.position = pos;
    }
}
