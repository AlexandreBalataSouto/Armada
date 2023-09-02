using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : MonoBehaviour
{
    private Transform _stopPointWarlock;
   private float _speed;
    public bool IsDeployShield { get; private set; } = false; //Read only, we CAN`T change the value
    public bool IsDeployTurretUp { get; private set; } = false; //Read only, we CAN`T change the value
    public bool IsDeployTurretDown { get; private set; } = false; //Read only, we CAN`T change the value

    void Start()
    {
        _speed = Constants.Warlock.SPEED;
        _stopPointWarlock = GameObject.FindWithTag(Constants.Common.STOP_POINT_WARLOCK).transform;
    }

    void Update()
    {
        //Move to the left
        Vector2 pos = transform.position;
        pos.x -= _speed * Time.deltaTime;


        //When reach stop point start coroutie RemoveShield
        if (pos.x > _stopPointWarlock.transform.position.x)
        {
            transform.position = pos;
        }
        else
        {
            StartCoroutine(DeployArsenal());
        }
    }

    IEnumerator DeployArsenal()
    {

        yield return new WaitForSeconds(2f);
        IsDeployTurretUp = true;

        yield return new WaitForSeconds(4f);
        IsDeployTurretDown = true;

        yield return new WaitForSeconds(6f);
        IsDeployShield = true;
    }
}
