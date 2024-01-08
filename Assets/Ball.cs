using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float startVelocity = 30f;
    float minAbsVelocity = 0.1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   

    }


    public void Swipe(Vector2 dir)
    {
        float absVelocity = Mathf.Sqrt(rb.velocity.x*rb.velocity.x+rb.velocity.y*rb.velocity.y);
        if(absVelocity < minAbsVelocity) rb.velocity = dir * startVelocity;
        //if (dir.x!=0) rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        //if (dir.y!=0) rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }
}
