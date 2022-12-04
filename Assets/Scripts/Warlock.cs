using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warlock : MonoBehaviour
{
    public GameObject stopPoint;
    [SerializeField, Range(0f, 20f)] private float speed = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        //Move to the left
        Vector2 pos = transform.position;
        pos.x -= speed * Time.fixedDeltaTime;


        //When reach stop point start coroutie RemoveShield
        if (pos.x > stopPoint.transform.position.x)
        {
            transform.position = pos;
        }
        else
        {
            StartCoroutine("DeployArsenal");
        }
    }

    IEnumerator DeployArsenal()
    {
        yield return new WaitForSeconds(2f);
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "WarlockTurret")
            {
                child.gameObject.GetComponent<WarlockTurret>().isDeployTurret = true;
            }
        }

        yield return new WaitForSeconds(4f);
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "WarlockTurret_1")
            {
                child.gameObject.GetComponent<WarlockTurret>().isDeployTurret = true;
            }
        }

        yield return new WaitForSeconds(6f);
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "WarlockShield"
                || child.gameObject.name == "WarlockShield_1"
                || child.gameObject.name == "WarlockShield_2"
                || child.gameObject.name == "WarlockShield_3")
            {
                child.gameObject.GetComponent<WarlockShield>().isDeployShield = true;
            }
        }

       
    }
}
