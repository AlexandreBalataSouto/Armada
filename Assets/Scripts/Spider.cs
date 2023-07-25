using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    //Enemy
    //Appear and start to moving in circles
    private BoxCollider2D collider;
    private SpriteRenderer sprite;
    //Move
    private Vector2 _pos;
    private Transform _startPosition = null;
    [SerializeField, Range(0f, 20f)] private float _speed = 2.5f;
    [SerializeField, Range(0f, 20f)] private float _angle = 1f;
    [SerializeField, Range(0f, 20f)] private float _radius = 3f;
    private bool _isStartMoving = false;
    //Shoot
    private Bullet _bullet;
    [SerializeField, Range(0f, 1f)] private float _fireRate = 1f;
    private float _nextFire = 0.0f;
    
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(_startPosition != null)
        {
            if(_isStartMoving == true)
            {
                transform.position = MovingInCircle( _startPosition.transform.position);

                if(Time.time > _nextFire)
                {
                    _nextFire = Time.time + _fireRate;
                    Shoot();
                }
            }
        }else{
            GameObject parentObject = GameObject.FindWithTag("StopPoints");
            int randomChildIndex = Random.Range(0, parentObject.transform.childCount);
            Transform randomChildTransform = parentObject.transform.GetChild(randomChildIndex);
            _startPosition = randomChildTransform.transform;
            transform.position = MovingInCircle( _startPosition.transform.position);
            sprite.enabled = true;
            StartCoroutine("FadeIn");
        }
    }

    IEnumerator FadeIn()
    {
       yield return new WaitForSeconds(3f);
       _isStartMoving = true;
       collider.enabled = true;
    }

    private Vector2 MovingInCircle(Vector2 posParam) 
    {
        _pos = posParam;
        _angle += _speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
        _pos.x += Mathf.Cos(_angle) * _radius;
        _pos.y += Mathf.Sin(_angle) * _radius;
        return _pos;
    }

    private void Shoot()
    {
        //Get bullet
        _bullet = ObjectPool.SharedInstance.GetPooledObject("BulletEnemy");

        if (_bullet != null)
        {
            //Direction/Who shoot/Activate
            _bullet.transform.position = transform.position;
            _bullet.Direction("Spider", 8f);
            _bullet.gameObject.SetActive(true);
        }

    }
}
