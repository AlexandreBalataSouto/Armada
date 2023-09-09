using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : MonoBehaviour
{
    //Enemy
    //Move to the left OR Move to the left doing waves
    private float _speed;
    private float _amplitude;
    private float _frequency;
    private float _sinCenterY;
    private bool _isWeird = false;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name.ToString().Contains(Constants.Common.WEIRD_SKULL))
        {
            _isWeird = true;
        }
        _sinCenterY = transform.position.y;
        _speed = Constants.Skull.SPEED;
        _amplitude = Constants.Skull.AMPLITUDE;
        _frequency = Constants.Skull.FREQUENCY;
    }

    void Update()
    {
        if(_isWeird)
        {
            Vector2 pos = transform.position;
            float sin = Mathf.Sin(pos.x * _frequency) * _amplitude;
            pos.x -= _speed * Time.deltaTime;
            pos.y = _sinCenterY + sin;
            transform.position = pos;
        }else
        {
            Vector2 pos = transform.position;
            pos.x -= _speed * Time.deltaTime;
            transform.position = pos;
        }
    }
}
