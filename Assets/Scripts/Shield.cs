using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //Makes shields (and guns) of orb move around them

    [SerializeField] private GameObject _target;
    [SerializeField, Range(0f, 200)] private float _speed = 90;
    private Vector3 _axis = new Vector3(0, 0, 1);

    void Update()
    {
        if(gameObject.GetComponentInParent<Orb>().IsRemovingShieldEven 
            && (gameObject.name == "Shield" 
            || gameObject.name == "Shield_2" 
            || gameObject.name == "Shield_4" 
            || gameObject.name == "Shield_6"))
        {
            gameObject.SetActive(false);
        }
        if (gameObject.GetComponentInParent<Orb>().IsRemovingShieldOdd
            && (gameObject.name == "Shield_1"
            || gameObject.name == "Shield_3"
            || gameObject.name == "Shield_5"
            || gameObject.name == "Shield_7"))
        {
            gameObject.SetActive(false);
        }
        // Spin the object around the target at 20 degrees/second.
        transform.RotateAround(_target.transform.position, _axis, _speed * Time.deltaTime);
    }
}
