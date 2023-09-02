using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    //Enemy
    //Moves toward the center of the screen, waits until shields disappear then shoot

    private float _speed;
    private Transform _stopPointOrb;
    public bool IsShootingEven { get; private set; } = false;
    public bool IsShootingOdd { get; private set; } = false;
    public bool IsRemovingShieldEven { get; private set; } = false;
    public bool IsRemovingShieldOdd { get; private set; } = false;

    void Start()
    {
        _speed = Constants.Orb.SPEED;
        _stopPointOrb = GameObject.FindWithTag(Constants.Common.STOP_POINT_ORB).transform;
    }

    void Update()
    {
        //Move to the left
        Vector2 pos = transform.position;
        pos.x -= _speed * Time.deltaTime;

        //When reach stop point start coroutie RemoveShield
        if(pos.x > _stopPointOrb.position.x)
        {
            transform.position = pos;
        }
        else
        {
            StartCoroutine(RemoveShield());
        }
    }

    //Wait and remove first 4 shields and allow to shoot, then wait to remove the rest of the shield and shoot more.
    IEnumerator RemoveShield()
    {
        yield return new WaitForSeconds(4f);

        IsRemovingShieldEven = true;
        IsShootingEven = true;

        yield return new WaitForSeconds(8f);

        IsRemovingShieldOdd = true;
        IsShootingOdd = true;
    }
}
