using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    //Makes shields (and guns) of orb move around them

    public GameObject target;
    [SerializeField, Range(0f, 200)] private float speed = 90;
    Vector3 axis = new Vector3(0, 0, 1);

    void Update()
    {
        // Spin the object around the target at 20 degrees/second.
        transform.RotateAround(target.transform.position, axis, speed * Time.deltaTime);
    }
}
