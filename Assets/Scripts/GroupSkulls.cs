using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSkulls : MonoBehaviour
{
    //Enemy
    //Move to the left doing waves

    [SerializeField, Range(0f, 20f)] private float _speed = 5f;
    [SerializeField, Range(0f, 20f)] private float _amplitude = 1f;
    [SerializeField, Range(0f, 20f)] private float _frequency = 1f;
    private float _sinCenterY;
    [SerializeField] private bool _inverted = false;

    // Start is called before the first frame update
    void Start()
    {
        _sinCenterY = transform.position.y;    
    }

    void Update()
    {
        Vector2 pos = transform.position;
        float sin = Mathf.Sin(pos.x * _frequency) * _amplitude;

        pos.x -= _speed * Time.deltaTime;

        if(_inverted)
        {
            sin *= -1;
        }
        pos.y = _sinCenterY + sin;

        transform.position = pos;
    }

}
