using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCatcher : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(GameManager.SharedInstance.EndPositionEnemy.x,0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
        }
    }
}
