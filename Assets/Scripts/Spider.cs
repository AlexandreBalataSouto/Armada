using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    [SerializeField, Range(0f, 20f)] private float speed = 3;
    [SerializeField, Range(0f, 20f)] private float angle = 0;
    [SerializeField, Range(0f, 20f)] private float radius = 1.8f;
    private bool isShooting = false;
    private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
        pos.x = Mathf.Cos(angle) * radius;
        pos.y = Mathf.Sin(angle) * radius;

        transform.position = pos;

        if((FindDegree(Mathf.Round(pos.x), Mathf.Round(pos.y)) == 0f || FindDegree(Mathf.Round(pos.x), Mathf.Round(pos.y)) == 180f) && (isShooting == false))
        {
            isShooting = true;
            Shoot();
        }

        if ((FindDegree(Mathf.Round(pos.x), Mathf.Round(pos.y)) == 270f || FindDegree(Mathf.Round(pos.x), Mathf.Round(pos.y)) == 90f) && (isShooting == true))
        {
            isShooting = false;
        }
    }

    public static float FindDegree(float x, float y)
    {
        float value = (float)((Mathf.Atan2(x, y) / Mathf.PI) * 180f);
        if (value < 0) value += 360f;

        return value;
    }

    private void Shoot()
    {
        
        bullet = ObjectPool.sharedInstance.GetPooledObject("BulletEnemy");

        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
        }
        
    }
}
