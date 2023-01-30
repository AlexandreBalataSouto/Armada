using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour
{
    //Enemy
    //It moves towards the StopPoint_2 then stops

    [SerializeField, Range(0f, 20f)] private float _speed = 2f;
    [SerializeField] private GameObject _stopPoint;

    // Update is called once per frame
    void Update()
    {
        //Move to the left
        Vector2 pos = transform.position;
        pos.x -= _speed * Time.fixedDeltaTime;

        //When reach stop point start coroutie RemoveShield
        if (pos.x > _stopPoint.transform.position.x)
        {
            transform.position = pos;
        }
    }
}
