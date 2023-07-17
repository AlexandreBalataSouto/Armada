using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockShield : MonoBehaviour
{

    //Default value TEST
    //Shield 01 -> _speed = 0.8f / _shieldX = 5.5f / _shieldY = 4f
    //Shield 02 -> _speed = 0.75f / _shieldX = 4.5f / _shieldY = 3.5f
    //Shield 03 -> _speed = 0.5f / _shieldX = 3.5f / _shieldY = 3f
    //Shield 04 -> _speed = 0.25f / _shieldX = 3f / _shieldY = 2f

    //Movement
    private Vector3 _startPos;
    private Vector3 _endPos;
    [SerializeField] private float _journeyTime = 1f;
    [SerializeField] private float _speed = 1f;

    //GetCenter
    [SerializeField] private GameObject _target;
    [SerializeField] private float _shieldX = 0f, _shieldY = 4f; //Use integer number
    private Vector3 _shieldPosition;

    private float _startTime;
    private Vector3 _centerPoint;
    private Vector3 _startRelCenter;
    private Vector3 _endRelCenter;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInParent<Warlock>().IsDeployShield)
        {
            GetCenter(Vector3.left);
            float fracComplete = Mathf.PingPong(Time.time - _startTime, _journeyTime / _speed);
            transform.position = Vector3.Slerp(_startRelCenter, _endRelCenter, fracComplete * _speed);
            transform.position += _centerPoint;
        }
    }

    public void GetCenter(Vector3 direction)
    {
        _startPos = new Vector3(_target.transform.position.x - _shieldX, _target.transform.position.y + _shieldY, 0);
        _endPos = new Vector3(_target.transform.position.x - _shieldX, _target.transform.position.y - _shieldY, 0);
        _centerPoint = (_startPos + _endPos) * 0.5f;
        _centerPoint -= direction;
        _startRelCenter = _startPos - _centerPoint;
        _endRelCenter = _endPos - _centerPoint;
    }
}
