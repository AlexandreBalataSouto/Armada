using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockShield : MonoBehaviour
{

    //Default value TEST
    //Shield 01 -> speed = 0.8f / shieldPositionX = 5.5f / shieldPositionY = 4f
    //Shield 02 -> speed = 0.75f / shieldPositionX = 4.5f / shieldPositionY = 3.5f
    //Shield 03 -> speed = 0.5f / shieldPositionX = 3.5f / shieldPositionY = 3f
    //Shield 04 -> speed = 0.25f / shieldPositionX = 3f / shieldPositionY = 2f

    public GameObject target;
    private Vector3 startPos;
    private Vector3 endPos;
    public float journeyTime = 1f;
    public float speed = 1f;

    private float startTime;
    private Vector3 centerPoint;
    private Vector3 startRelCenter;
    private Vector3 endRelCenter;

    public float shieldPositionX = 0f, shieldPositionY = 4f; //Use integer number

    public bool isDeployShield = false;

    // Update is called once per frame
    void Update()
    {
     
        if(isDeployShield)
        {
            GetCenter(Vector3.left);
            float fracComplete = Mathf.PingPong(Time.time - startTime, journeyTime / speed);
            transform.position = Vector3.Slerp(startRelCenter, endRelCenter, fracComplete * speed);
            transform.position += centerPoint;
        }
    }

    public void GetCenter(Vector3 direction)
    {
        startPos = new Vector3(target.transform.position.x - shieldPositionX, target.transform.position.y + shieldPositionY, 0);
        endPos = new Vector3(target.transform.position.x - shieldPositionX, target.transform.position.y - shieldPositionY, 0);
        centerPoint = (startPos + endPos) * 0.5f;
        centerPoint -= direction;
        startRelCenter = startPos - centerPoint;
        endRelCenter = endPos - centerPoint;
    }
}
