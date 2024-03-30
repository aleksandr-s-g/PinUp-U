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
        float singleBlockPxSize = Screen.width / sceneWidth;
        cameraStartpositionY = Screen.height*0.5f/singleBlockPxSize;
        cameraStartpositionX = 5f;
        Camera.allCameras[0].orthographicSize = orthoSize;
        transform.position = new Vector3(cameraStartpositionX,cameraStartpositionY,-1);
    }

    // Update is called once per frame
    void Update()
    {
            float cameraTargetPositionY = ball.transform.position.y;
            float cameraCurrentPositionY = transform.position.y;
            float cameraTransformLen = cameraTargetPositionY - cameraCurrentPositionY;
            float cameraSpeed = Mathf.Sign(cameraTransformLen)*cameraTransformLen*cameraTransformLen;
            if (Mathf.Abs(cameraSpeed) < 0.1f) cameraSpeed = 0f;
            float cameraNewPositionY = cameraCurrentPositionY + cameraSpeed*Time.deltaTime;
            if (cameraNewPositionY > cameraStartpositionY)
            {
                transform.position = new Vector3(cameraStartpositionX,cameraNewPositionY,-1);
            }
            
    }
}
