using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCameraScript : MonoBehaviour
{
    public float sceneWidth = 10;
    public float cameraAscelleration = 0.01f;
    public float cameraMinSpeed = 1f;
    public float cameraMaxSpeed = 10f;
    public GameObject ball;
    //public GameObject GameControllerRace;
   // GameControllerRace gameControllerRace;
    float cameraStartpositionY;
    float cameraStartpositionX;
    float cameraSpeedAdd;
    bool isGameStarted = false;
    private int currentScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        float orthoSize = sceneWidth * Screen.height / Screen.width * 0.5f;
        float singleBlockPxSize = Screen.width / sceneWidth;
        cameraStartpositionY = Screen.height * 0.5f / singleBlockPxSize;
        cameraStartpositionX = 5f;
        Camera.allCameras[0].orthographicSize = orthoSize;
        //Camera.main.orthographicSize = orthoSize;
        //Camera.current.orthographicSize = orthoSize;
        transform.position = new Vector3(cameraStartpositionX, cameraStartpositionY, -1);
        cameraSpeedAdd = cameraMinSpeed;
     //   gameControllerRace = gameControllerRace.GetComponent<GameControllerRace>();
    }

    // Update is called once per frame
    void Update()
    {
      //  currentScore = gameControllerRace.getCurretnScore();
       
        if ((int)ball.transform.position.y > currentScore)
        {
            currentScore = (int)ball.transform.position.y;
        }
        if (isGameStarted)
        {
            float cameraTargetPositionY = ball.transform.position.y;
            float cameraCurrentPositionY = transform.position.y;
            float cameraTransformLen = cameraTargetPositionY - cameraCurrentPositionY;
            float cameraSpeed = cameraSpeedAdd;

            if (cameraTransformLen > 0)
            {
                cameraSpeed = cameraTransformLen * cameraTransformLen + cameraSpeedAdd;
            }
            if (cameraSpeedAdd < cameraMaxSpeed) cameraSpeedAdd = cameraMinSpeed + currentScore * cameraAscelleration;
            if (Mathf.Abs(cameraSpeed) < 0.1f) cameraSpeed = 0f;
            float cameraNewPositionY = cameraCurrentPositionY + cameraSpeed * Time.deltaTime;
            if (cameraNewPositionY > cameraCurrentPositionY)
            {
                transform.position = new Vector3(cameraStartpositionX, cameraNewPositionY, -1);
            }
           
        }
        if (ball.transform.position.y > 1) isGameStarted = true;
    }
   // public void SetScoresCamera(int scores)
   // {
   //     currentScore = scores;
   // }
}
