using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    //Enemy
    //Appear and start to moving in circles
    private BoxCollider2D _collider;
    private SpriteRenderer _sprite;
    //Move
    private Vector2 _pos;
    private Transform _startPosition = null;
    private float _speed;
    private float _angle;
    private float _radius;
    private bool _isStartMoving = false;
    //Shoot
    private Bullet _bullet;
    private float _fireRate;
    private float _nextFire;
    
    void Start()
    {
        _speed = Constants.Spider.SPEED;
        _angle = Constants.Spider.ANGLE;
        _radius = Constants.Spider.RADIUS;
        _fireRate = Constants.Spider.FIRE_RATE;
        _nextFire = 0f;
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = false;
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.enabled = false;
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
            GameObject parentObject = GameObject.FindWithTag(Constants.Common.STOP_POINTS);
            int randomChildIndex = Random.Range(0, parentObject.transform.childCount);
            Transform randomChildTransform = parentObject.transform.GetChild(randomChildIndex);
            _startPosition = randomChildTransform.transform;
            transform.position = MovingInCircle( _startPosition.transform.position);
            _sprite.enabled = true;
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
       yield return new WaitForSeconds(3f);
       _isStartMoving = true;
       _collider.enabled = true;
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
        _bullet = ObjectPool.SharedInstance.GetPooledObject(Constants.Common.BULLET_ENEMY);

        if (_bullet != null)
        {
            //Direction/Who shoot/Activate
            _bullet.transform.position = transform.position;
            _bullet.Direction(Constants.Common.SPIDER, Constants.Bullet.SPIDER_SPEED);
            _bullet.gameObject.SetActive(true);
        }

    }
}
