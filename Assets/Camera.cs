using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float sceneWidth = 10;
    // Start is called before the first frame update
    Camera _camera;
    void Start()
    {
        // _camera = GetComponent<Camera>();
        // float unitsPerPixel = sceneWidth / Screen.width;
        // float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;
        // _camera.Size = desiredHalfHeight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
