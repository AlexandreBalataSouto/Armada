using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    [SerializeField, Range(0f, 20f)] private float speed = 5f;


    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        pos.x -= speed * Time.fixedDeltaTime;

        transform.position = pos;
    }
}
