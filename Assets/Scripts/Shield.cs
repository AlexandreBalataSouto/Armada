using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //Makes shields (and guns) of orb move around them

    private GameObject _target;
    private float _speed;
    private Vector3 _axis = new Vector3(0, 0, 1);

    void Start()
    {
        _target = gameObject.GetComponentInParent<Orb>().gameObject;
        _speed = Constants.Shield.SPEED;
    }

    void Update()
    {
        if(gameObject.GetComponentInParent<Orb>().IsRemovingShieldEven 
            && (gameObject.name == Constants.Orb.ORB_SHIELD[0]
            || gameObject.name == Constants.Orb.ORB_SHIELD[2]
            || gameObject.name == Constants.Orb.ORB_SHIELD[4]
            || gameObject.name == Constants.Orb.ORB_SHIELD[6]))
        {
            gameObject.SetActive(false);
        }
        if (gameObject.GetComponentInParent<Orb>().IsRemovingShieldOdd
            && (gameObject.name == Constants.Orb.ORB_SHIELD[1]
            || gameObject.name == Constants.Orb.ORB_SHIELD[3]
            || gameObject.name == Constants.Orb.ORB_SHIELD[5]
            || gameObject.name == Constants.Orb.ORB_SHIELD[7]))
        {
            gameObject.SetActive(false);
        }
        // Spin the object around the target at 20 degrees/second.
        transform.RotateAround(_target.transform.position, _axis, _speed * Time.deltaTime);
    }
}
