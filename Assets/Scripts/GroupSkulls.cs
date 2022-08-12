using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSkulls : MonoBehaviour
{
    [SerializeField, Range(0f, 20f)] private float speed = 5f;
    [SerializeField, Range(0f, 20f)] private float amplitude = 1f;
    [SerializeField, Range(0f, 20f)] private float frequency = 1f;
   
    private float sinCenterY;
    public bool inverted = false;

    // Start is called before the first frame update
    void Start()
    {
        sinCenterY = transform.position.y;    
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        float sin = Mathf.Sin(pos.x * frequency) * amplitude;

        pos.x -= speed * Time.fixedDeltaTime;

        if(inverted)
        {
            sin *= -1;
        }
        pos.y = sinCenterY + sin;

        transform.position = pos;
    }

}
