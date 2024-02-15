using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    public GameObject Ball;
    public GameObject GameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            KeyPressed(new Vector2(0, 1));
           
        }
            
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            KeyPressed(new Vector2(0, -1));
            
        }
            
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            KeyPressed(new Vector2(-1, 0));
           
        }
            
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            KeyPressed(new Vector2(1, 0));
           
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameController.GetComponent<GameControllerRace>().DoubleTap();
        }

    }
    void KeyPressed(Vector2 dir)
    {
            Ball.GetComponent<Ball>().Swipe(dir);
    }
}
