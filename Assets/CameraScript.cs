using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float sceneWidth = 10;
    // Start is called before the first frame update
    //Camera _camera;
    void Start()
    {
        float orthoSize = sceneWidth * Screen.height/Screen.width*0.5f;


        Camera.main.orthographicSize = orthoSize;
        transform.position = new Vector3(5,Screen.height/orthoSize/sceneWidth*0.5f,-1);

        //Debug.Log(Screen.height);
        //Debug.Log(orthoSize);
        //2340/10.83/2/10
        //transform.position = new Vector2(5,Screen.height/orthoSize/sceneWidth*0.5f);
                // _camera = GetComponent<Camera>();
        // float unitsPerPixel = sceneWidth / Screen.width;
        // float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
        // _camera.Size = desiredHalfHeight;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position);
        //transform.position = new Vector2(0,1000);
    }
}
