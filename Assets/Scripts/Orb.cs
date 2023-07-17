using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    //Enemy
    //Moves toward the center of the screen, waits until shields disappear then shoot

    [SerializeField, Range(0f, 20f)] private float _speed = 1.5f;
    [SerializeField] private GameObject stopPoint;
    public bool IsShootingEven { get; private set; } = false;
    public bool IsShootingOdd { get; private set; } = false;
    public bool IsRemovingShieldEven { get; private set; } = false;
    public bool IsRemovingShieldOdd { get; private set; } = false;

    void Update()
    {

        //Move to the left
        Vector2 pos = transform.position;
        pos.x -= _speed * Time.deltaTime;


        //When reach stop point start coroutie RemoveShield
        if(pos.x > stopPoint.transform.position.x)
        {
            transform.position = pos;
        }
        else
        {
            StartCoroutine("RemoveShield");
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
