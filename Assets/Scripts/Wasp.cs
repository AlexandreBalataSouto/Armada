using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wasp : MonoBehaviour
{
    //Enemy
    //Moves to one of the two points and start shooting

    [SerializeField, Range(0f, 20f)] private float speed = 5f;
    public List<GameObject> waspPoints;
    private int waspPosition;
    private bool isInWaspPosition = false;
    private GameObject bullet;
    [SerializeField, Range(0f, 1f)] private float fireRate = 0.25f;
    private float nextFire = 0.0f;
    public GameObject playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        waspPosition = GetWaspPoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, waspPoints[waspPosition].transform.position, speed * Time.fixedDeltaTime);

        if(transform.position.x <= waspPoints[waspPosition].transform.position.x && isInWaspPosition == false)
        {
            isInWaspPosition = true;
        }

        if(isInWaspPosition == true && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }


    private int GetWaspPoint()
    {
        int indexWaspPoint = 0;

        if (waspPoints.Count > 0)
        {
            indexWaspPoint = Random.Range(0, waspPoints.Count);
        }

        return indexWaspPoint;
    }

    private void Shoot()
    {
        //Get bullet
        bullet = ObjectPool.sharedInstance.GetPooledObject("BulletEnemy");

        if (bullet != null)
        {
            //Direction/Position who shoot/Activate
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().Direction("Wasp",10f, playerPosition.transform.position);
            bullet.SetActive(true);
        }

    }

}
