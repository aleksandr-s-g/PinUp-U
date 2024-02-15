using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using static UnityEngine.InputSystem.LowLevel.InputEventTrace;

public class Analytics : MonoBehaviour
{
    string path = Application.dataPath + "/Test.txt";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnalyticsEvent(string event_name, string event_details, string	device_info, string	user_info)
    {
        
        File.WriteAllText(path, "1" + event_name + "2" + event_details + "3" + device_info + "4" + user_info);
        Debug.Log(path);
    }
}
