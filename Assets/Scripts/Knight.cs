using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    //Enemy
    //Moves in different patterns and after a few secons charge to the player

    public Vector3 Distance, MovementFrequency;
    private Vector3 Moveposition;
    private Vector3 startPosition;

    public bool isFireRateChanged = false,isMoving = false, isReturning = false, isAttacking = false; //isAttacking == isShooting
    [SerializeField, Range(0f, 10f)] private float fireRate = 6f;
    private float nextFire = 0.0f;
    [SerializeField, Range(0f, 20f)] private float speedAttack = 15f;

    public Transform playerPosition;
    private Vector3 goToPlayer;

    private float timer = 0f, timerAux = 0f; //Compesate time

    public Transform spear, shield; //Spear and Shield position

    private Vector3 difference; //Spear orientation
    private float rotationZ = 0f; //Spear orientation
    private float spearRotationAux = 90f; //Spear orientation

    private float playerX = 0f, playerY = 0f, spearX = 0f, spearY = 0f,spearAux = 10f; //Draw Spear

    //Pattern TESTING -----> RANDOMIZE
    public  bool bretzelPattern = false;
    public bool infinitePattern = false;
    public bool potteryPattern = false;
    public bool attomPattern = false;
    public bool zPattern = false;

    private bool[] arr = new bool[6];
    private int index = 0;

    void Start()
    {
        //From your position start moving
        startPosition = transform.position;

        arr[0] = bretzelPattern;
        arr[1] = infinitePattern;
        arr[2] = potteryPattern;
        arr[3] = attomPattern;
        arr[4] = zPattern;

        index = Random.Range(1, arr.Length);
        arr[index - 1] = true;

        if (arr[0]) //bretzelPattern
        {
            Distance.x = 4f;
            Distance.y = 2f;
            MovementFrequency.x = 3f;
            MovementFrequency.y = 4f;
        }
        if (arr[1]) //infinitePattern
        {
            Distance.x = 4f;
            Distance.y = 3f;
            MovementFrequency.x = 2f;
            MovementFrequency.y = 4f;
        }
        if (arr[2]) //potteryPattern
        {
            Distance.x = 3f;
            Distance.y = 3f;
            MovementFrequency.x = 4f;
            MovementFrequency.y = 1f;
        }
        if (arr[3]) //attomPattern
        {
            Distance.x = 5f;
            Distance.y = 3f;
            MovementFrequency.x = 2.5f;
            MovementFrequency.y = 2f;
        }
        if (arr[4]) //zPattern
        {
            Distance.x = 6f;
            Distance.y = 4f;
            MovementFrequency.x = 6f;
            MovementFrequency.y = 2f;
        }

        //Start moving and after a few secons attack
        isMoving = true;
        nextFire = Time.time + fireRate;
    }


    // Update is called once per frame
    void Update()
    {
        //Fire rate random
        if(!isFireRateChanged)
        {
            fireRate = Mathf.Round(Random.Range(2f, 6f));
            isFireRateChanged = !isFireRateChanged;
        }

        //Move logic
        if (isMoving)
        {
            timer = Time.time - timerAux;
            Moveposition.x = startPosition.x + Mathf.Sin(timer * MovementFrequency.x) * Distance.x;
            Moveposition.y = startPosition.y + Mathf.Sin(timer * MovementFrequency.y) * Distance.y;
            transform.position = new Vector3(Moveposition.x, Moveposition.y, 0f);

        }
        //Attack and rate of fire
        if (!isAttacking && !isReturning && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            isAttacking = !isAttacking;
            isMoving = !isMoving;
            goToPlayer = playerPosition.position;

            //Spear orientation
            difference = playerPosition.position - transform.position;
            rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            spear.rotation = Quaternion.Euler(0f, 0f, rotationZ - spearRotationAux);
            //END Spear orientation

            //Draw the spear
            playerX = playerPosition.position.x;
            playerY = playerPosition.position.y; 
            spearX = spear.localPosition.x;
            spearY = spear.localPosition.y;
            spear.localPosition = new Vector3((playerX - spearX) / spearAux, (playerY - spearY) / spearAux, 1f);
            //END Draw the spear

            //Shield disabled
            shield.gameObject.SetActive(false);
        }

        //Go towards the player
        if (isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, goToPlayer, speedAttack * Time.time);

            //When reach the player position return to the start position
            if (transform.position == goToPlayer)
            {
                isReturning = !isReturning;
                isAttacking = !isAttacking;
            }
        }

        //Return to the start position 
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, speedAttack * Time.time);

            //Spear orientation
            spear.rotation = Quaternion.Euler(0f, 0f, 0f);
            //END Spear orientation

            //Return spear to 0,0,0
            spear.localPosition = new Vector3(0f, 0f, 1f);

            //Shield actived
            shield.gameObject.SetActive(true);

            //When reach the start position, start moving again
            if (transform.position == startPosition)
            {
                isMoving = !isMoving;
                isReturning = !isReturning;
                isFireRateChanged = !isFireRateChanged;
                nextFire = Time.time + fireRate;
                timerAux = Time.time;
            }
        }
    }
}

//Move up and down
//transform.position = new Vector3(x, maxMove * Mathf.Sin(Time.time * speed), 0);
//Make the character move in a circle like the Spider !!!
//transform.position = new Vector3(maxMove * Mathf.Sin(Time.time), maxMove * Mathf.Cos(Time.time), 0);
//Rotate
//transform.rotation = Quaternion.Euler(0f, 0f, maxMove * Mathf.Sin(Time.time * speed));