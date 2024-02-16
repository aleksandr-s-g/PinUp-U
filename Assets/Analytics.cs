using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using static UnityEngine.InputSystem.LowLevel.InputEventTrace;

public class Analytics : MonoBehaviour
{
    string path = Application.dataPath + "/Test.txt";
    string device_info;
    string user_info = "tester";
    float fps = 0f;
    bool isTester = true;
    // Start is called before the first frame update
    void Start()
    {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        string country = Application.systemLanguage.ToString();
        string language = Application.systemLanguage.ToString();
        string operatingSystem = SystemInfo.operatingSystem;
        string osVersion = System.Environment.OSVersion.Version.ToString();
        string deviceModel = SystemInfo.deviceModel;
        string processorModel = SystemInfo.processorType;
        string global_ip = "0.0.0.0";
        int screenWidth = Screen.currentResolution.width;
        int screenHeight = Screen.currentResolution.height;
        string userID = SystemInfo.deviceUniqueIdentifier;
        device_info = "{\"device_id\": " + deviceID
            + "\"fps\": " + fps.ToString("0")
            + "\"global_ip\"" + global_ip
            + "\"is_tester\": " + isTester
            + "\"locale\": " + country
            + "\"locale_language\": " + language
            + "\"model_name\": " + deviceModel
            + "\"os_name\": " + operatingSystem
            + "\"os_version\": " + osVersion
            + "\"processor_name\": " + processorModel
            + "\"screen_h\": " + screenHeight
            + "\"screen_w\": " + screenWidth
            + "\"user_id\": " + userID
            + "};";
    }

    // Update is called once per frame
    void Update()
    {
         fps = 1.0f / Time.deltaTime;
    }

    public void AnalyticsEvent(string event_name, string event_details)
    {
        
        File.WriteAllText(path, "{\"event_name\" : \"" + event_name + "\"," + "\"event_details\" : \"" + event_details + "\"," + "\"device_info\" : \"" + device_info + "\"," + "\"user_info\" : \"" + user_info + "\"}");
       // Debug.Log(path);
    }
}
