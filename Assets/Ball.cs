using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Ball : MonoBehaviour
{
    //public static Action onCoinCollected2;
    // Start is called before the first frame update
    //public delegate void SomeAction();
    public UnityAction onCoinCollected;
    public Rigidbody2D rb;
    public CircleCollider2D collider2d;
    public float startVelocity = 30f;
    public float resetVelocity = 30f;
    //public float resetTime = 2f;
    public float ballAcceleration = 5;
    float minAbsVelocity = 0.1f;
    private bool isReseting = false;
    //private Vector2 ballPosition;
    private Vector2 ballTargetPosition;
    //private Vector2 velocity = Vector2.zero;
    private float deccelerationY  = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<CircleCollider2D>();
        //onCoinCollected+=GameController.instance.CoinCollected;
    }

    // Update is called once per frame
    void Update()
    {
       if (isReseting)
        {
            collider2d.enabled = false;
            Vector2 direction = (ballTargetPosition - rb.position).normalized;
            
            if (Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y ) < minAbsVelocity)
            {

                rb.velocity = direction * resetVelocity;
                               
            }
            else
            {
                if (rb.position.y < deccelerationY)
                {
                    rb.velocity = (rb.velocity + direction * ballAcceleration * Time.deltaTime);
                }
                else
                {
                    rb.velocity = (rb.velocity - direction * ballAcceleration * Time.deltaTime);
                }
            }
            if (ballTargetPosition.y - rb.position.y < 1)
            {
                rb.velocity = Vector2.zero;
                rb.position = ballTargetPosition;
                isReseting = false;
                collider2d.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Coin")
        {
            Destroy(coll.gameObject);
            onCoinCollected?.Invoke();
        }
    }


    public void Swipe(Vector2 dir)
    {
        float absVelocity = Mathf.Sqrt(rb.velocity.x*rb.velocity.x+rb.velocity.y*rb.velocity.y);
        if(absVelocity < minAbsVelocity && !isReseting) rb.velocity = dir * startVelocity;
        //if (dir.x!=0) rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        //if (dir.y!=0) rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }
    public void ResetBall(int targetYPosition)
    {
        //rb.position = new Vector3(3f, targetYPosition, 0f);
        isReseting = true;
        ballTargetPosition = new Vector2 (3.5f, targetYPosition - 0.5f);
        deccelerationY = (targetYPosition - rb.position.y)/2 + rb.position.y;
        rb.velocity = Vector2.zero;
    }
    
}
