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
    public CircleCollider2D cc2d;
    public float startVelocity = 30f;
    public float resetTime = 2f;
    float minAbsVelocity = 0.1f;
    private bool isReseting = false;
    private Vector2 ballPosition;
    private Vector2 ballTargetPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
        //onCoinCollected+=GameController.instance.CoinCollected;
    }

    // Update is called once per frame
    void Update()
    {
        ballPosition = rb.position;
        if (isReseting)
        {
            cc2d.enabled = false;
            if (Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y ) < minAbsVelocity)
            {
                Vector2 direction = (ballTargetPosition - ballPosition).normalized;
                 
                Vector2 velocity = direction * startVelocity;
                                
                rb.velocity = velocity;
            }
            if (rb.position.y > ballTargetPosition.y)
            {
                rb.velocity = Vector2.zero;
                isReseting = false;
                cc2d.enabled = true;
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
        ballTargetPosition = new Vector2 (3, targetYPosition);
    }

}
