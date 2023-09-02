using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarlockShield : MonoBehaviour
{
    //Movement
    private Vector3 _startPos;
    private Vector3 _endPos;
    private float _journeyTime;
    private float _speed;

    //GetCenter
    private GameObject _target;
    private float _shieldX, _shieldY;
    private float _startTime;
    private Vector3 _centerPoint;
    private Vector3 _startRelCenter;
    private Vector3 _endRelCenter;

    void Start()
    {
        _target = gameObject.GetComponentInParent<Warlock>().gameObject;
        _journeyTime = Constants.WarlockShieldList.FirstOrDefault(item => item.SHIELD_NAME == gameObject.name.ToString()).JOURNEY_TIME;
        _speed = Constants.WarlockShieldList.FirstOrDefault(item => item.SHIELD_NAME == gameObject.name.ToString()).SPEED;
        _shieldX = Constants.WarlockShieldList.FirstOrDefault(item => item.SHIELD_NAME == gameObject.name.ToString()).SHIELD_X;
        _shieldY = Constants.WarlockShieldList.FirstOrDefault(item => item.SHIELD_NAME == gameObject.name.ToString()).SHIELD_Y;
    }

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
        _centerPoint = (_startPos + _endPos) * Constants.Warlock.AUX;
        _centerPoint -= direction;
        _startRelCenter = _startPos - _centerPoint;
        _endRelCenter = _endPos - _centerPoint;
    }
}
