using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : MonoBehaviour
{
    //Enemy
    //It moves towards the StopPoint_Kraken then stops
    private float _speed = 2f;

    public bool IsInKrakenpPosition { get; private set; } = false; //Read only, we CAN`T change the value
    private Vector2 _krakenPosition;

    void Start()
    {
        _krakenPosition = GameObject.FindWithTag("StopPoint_Kraken").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _krakenPosition, _speed * Time.deltaTime);

        if(transform.position.x <= _krakenPosition.x && IsInKrakenpPosition == false)
        {
            IsInKrakenpPosition = true;
        }
    }
}
