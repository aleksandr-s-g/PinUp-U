using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCameraScript : MonoBehaviour
{
    public float sceneWidth = 10;
    public float cameraAscelleration = 0.05f;
    public GameObject ball;
    float cameraStartpositionY;
    float cameraStartpositionX;
    float cameraSpeedAdd;
    // Start is called before the first frame update
    void Start()
    {
        cameraSpeedAdd = 0f;
        float orthoSize = sceneWidth * Screen.height / Screen.width * 0.5f;
        float singleBlockPxSize = Screen.width / sceneWidth;
        cameraStartpositionY = Screen.height * 0.5f / singleBlockPxSize;
        cameraStartpositionX = 5f;
        Camera.main.orthographicSize = orthoSize;
        transform.position = new Vector3(cameraStartpositionX, cameraStartpositionY, -1);
    }

    // Update is called once per frame
    void Update()
    {
        float cameraTargetPositionY = ball.transform.position.y;
        float cameraCurrentPositionY = transform.position.y;
        float cameraTransformLen = cameraTargetPositionY - cameraCurrentPositionY;
        float cameraSpeed = cameraSpeedAdd;
        if (cameraTransformLen > 0) 
        {
           cameraSpeed = cameraTransformLen * cameraTransformLen + cameraSpeedAdd;
        }
        
        cameraSpeedAdd = cameraSpeedAdd + cameraAscelleration * Time.deltaTime;
        if (Mathf.Abs(cameraSpeed) < 0.1f) cameraSpeed = 0f;
        float cameraNewPositionY = cameraCurrentPositionY + cameraSpeed * Time.deltaTime;
        if (cameraNewPositionY > cameraCurrentPositionY)
        {
            transform.position = new Vector3(cameraStartpositionX, cameraNewPositionY, -1);
        }
        //Debug.Log(cameraSpeedAdd);
    }
}
