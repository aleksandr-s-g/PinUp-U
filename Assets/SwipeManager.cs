using UnityEngine;
using System.Collections;
public class TouchInputManager : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 vector;
    public float min_swipe_len = 30f;
    public float max_diagonal_factor = 0.3f;
    string message;
    Vector2 direction;
    public GameObject Ball;

    void Start(){
        direction = new Vector2(0,0);
    }
    
    void Update()
    {        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    message = "Begun ";
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    // Determine direction by comparing the current touch position with the initial one
                    vector = touch.position - startPos;
                    float swipe_len = Mathf.Sqrt(vector.x*vector.x+vector.y*vector.y);
                    if(swipe_len > min_swipe_len)
                    {
                        float abs_x = Mathf.Abs(vector.x);
                        float abs_y = Mathf.Abs(vector.y);
                        float diagonal_factor;
                        if (abs_x > abs_y)
                        {
                            diagonal_factor = Mathf.Abs(vector.y)/Mathf.Abs(vector.x);
                            if (diagonal_factor < max_diagonal_factor)
                            {
                                if (vector.x > 0) direction = new Vector2(1,0);
                                else direction = new Vector2(-1,0);
                                Swiped(direction);
                            }
                            
                        }
                        else
                        {
                            diagonal_factor = Mathf.Abs(vector.x)/Mathf.Abs(vector.y);
                            if (diagonal_factor < max_diagonal_factor)
                            {
                                if (vector.y > 0) direction = new Vector2(0,1);
                                else direction = new Vector2(0,-1);
                                Swiped(direction);
                            }
                        }
                        
                        startPos = touch.position;
                    }
                    message = "Moving ";
                    //if (direction.x*direction.x+direction.)
                    //Debug.Log(Mathf.Sqrt(direction.x*direction.x+direction.y*direction.y));
                    break;

                case TouchPhase.Ended:
                    // Report that the touch has ended when it ends
                    message = "Ending ";
                    break;
            }
            //Debug.Log(message);
        }
    }

    void Swiped(Vector2 dir)
    {
            Ball.GetComponent<Ball>().Swipe(dir);
    }
}