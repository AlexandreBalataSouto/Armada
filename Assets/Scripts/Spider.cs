using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    //Enemy
    //Appear and start to moving in circles, each time that reach the top and bottom shoot

    [SerializeField, Range(0f, 20f)] private float speed = 3;
    [SerializeField, Range(0f, 20f)] private float angle = 0;
    [SerializeField, Range(0f, 20f)] private float radius = 1.8f;
    private bool isShooting = false;
    private GameObject bullet;

    // Update is called once per frame
    void Update()
    {

        //Move in circles
        Vector2 pos = transform.position;
        angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
        pos.x = Mathf.Cos(angle) * radius;
        pos.y = Mathf.Sin(angle) * radius;
        transform.position = pos;
        //END Move in circles

        //If is on top OR bottom and is NOT shooting then shoot
        if ((FindDegree(Mathf.Round(pos.x), Mathf.Round(pos.y)) == 0f || FindDegree(Mathf.Round(pos.x), Mathf.Round(pos.y)) == 180f) && (isShooting == false))
        {
            isShooting = true;
            Shoot();
        } 
        //If is on left OR right and is shooting then stop shoot
        if ((FindDegree(Mathf.Round(pos.x), Mathf.Round(pos.y)) == 270f || FindDegree(Mathf.Round(pos.x), Mathf.Round(pos.y)) == 90f) && (isShooting == true))
        {
            isShooting = false;
        }
    }

    //Return degree of the circle
    public static float FindDegree(float x, float y)
    {
        float value = (float)((Mathf.Atan2(x, y) / Mathf.PI) * 180f);
        if (value < 0) value += 360f;

        return value;
    }

    private void Shoot()
    {
        //Get bullet
        bullet = ObjectPool.sharedInstance.GetPooledObject("BulletEnemy");

        if (bullet != null)
        {
            //Direction/Who shoot/Activate
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().Direction("Spider", 8f);
            bullet.SetActive(true);
        }

    }
}
