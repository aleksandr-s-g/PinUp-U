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
        Debug.Log("Screen.height: " + Screen.height);
        Debug.Log("Screen.width: " + Screen.width);
        Debug.Log("Screen sceneWidth " + sceneWidth);
        Debug.Log("Screen orthoSize: " + orthoSize);
        float singleBlockPxSize = Screen.width / sceneWidth;
        cameraStartpositionY = Screen.height*0.5f/singleBlockPxSize;
        cameraStartpositionX = 5f;
        Camera.allCameras[0].orthographicSize = orthoSize;
        //Camera.main.orthographicSize = orthoSize;
        //foreach (Camera c in Camera.allCameras)
        //{
        //    c.orthographicSize = orthoSize;
        //}
        //Debug.Log("Screen Camera.current.orthographicSize: " + Camera.current.orthographicSize);
        //Debug.Log("Screen Camera.allCamerasCount: "+Camera.allCamerasCount);
        //Debug.Log("Screen Camera.allCameras: " + Camera.allCameras[0]);
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
