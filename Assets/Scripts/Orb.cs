using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    //Enemy
    //Moves toward the center of the screen, waits until shields disappear then shoot

    [SerializeField, Range(0f, 20f)] private float speed = 1.5f;
    public GameObject stopPoint;

    void FixedUpdate()
    {

        //Move to the left
        Vector2 pos = transform.position;
        pos.x -= speed * Time.fixedDeltaTime;


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

        foreach (Transform child in transform)
        {

            if(child.gameObject.name == "Shield" 
                || child.gameObject.name == "Shield_2"
                || child.gameObject.name == "Shield_4"
                || child.gameObject.name == "Shield_6")
            {
                child.gameObject.SetActive(false);
            }
            if (child.gameObject.name == "Orb_shoot"
                || child.gameObject.name == "Orb_shoot_2"
                || child.gameObject.name == "Orb_shoot_4"
                || child.gameObject.name == "Orb_shoot_6")
            {
                child.gameObject.GetComponent<OrbShoot>().isShooting = true;
            }
        }

        yield return new WaitForSeconds(8f);

        foreach (Transform child in transform)
        {

            if (child.gameObject.name == "Shield_1"
                || child.gameObject.name == "Shield_3"
                || child.gameObject.name == "Shield_5"
                || child.gameObject.name == "Shield_7")
            {
                child.gameObject.SetActive(false);
            }
            if (child.gameObject.name == "Orb_shoot_1"
                || child.gameObject.name == "Orb_shoot_3"
                || child.gameObject.name == "Orb_shoot_5"
                || child.gameObject.name == "Orb_shoot_7")
            {
                child.gameObject.GetComponent<OrbShoot>().isShooting = true;
            }
        }
    }
}
