using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float sceneWidth = 10;
    public GameObject ball;
    float cameraStartpositionY;
    float cameraStartpositionX;
    void Start()
    {
        float orthoSize = sceneWidth * Screen.height/Screen.width*0.5f;
        cameraStartpositionY = Screen.height/orthoSize/sceneWidth*0.5f;
        cameraStartpositionX = 5f;
        Camera.main.orthographicSize = orthoSize;
        transform.position = new Vector3(cameraStartpositionX,cameraStartpositionY,-1);
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.transform.position.y > cameraStartpositionY)
        {
            float cameraTargetPositionY = ball.transform.position.y;
            float cameraCurrentPositionY = transform.position.y;
            float cameraTransformLen = cameraTargetPositionY - cameraCurrentPositionY;
            float cameraSpeed = Mathf.Sign(cameraTransformLen)*cameraTransformLen*cameraTransformLen/1000;
            float cameraNewPositionY = cameraCurrentPositionY + cameraSpeed;
            transform.position = new Vector3(cameraStartpositionX,cameraNewPositionY,-1);
            //Debug.Log(ball.transform.position);
        }
        
        //transform.position = new Vector2(0,1000);
    }
}
