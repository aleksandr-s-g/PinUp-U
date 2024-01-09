using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    //public static Action onCoinCollected2;
    // Start is called before the first frame update
    //public delegate void SomeAction();
    public UnityAction onCoinCollected;
    public Rigidbody2D rb;
    public float startVelocity = 30f;
    float minAbsVelocity = 0.1f;
   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //onCoinCollected+=GameController.instance.CoinCollected;
    }

    // Update is called once per frame
    void Update()
    {   

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
        if(absVelocity < minAbsVelocity) rb.velocity = dir * startVelocity;
        //if (dir.x!=0) rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        //if (dir.y!=0) rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }
}
