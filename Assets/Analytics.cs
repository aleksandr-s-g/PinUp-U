using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using static UnityEngine.InputSystem.LowLevel.InputEventTrace;
using UnityEngine.Networking;

public class Analytics : MonoBehaviour
{
    string path = Application.dataPath + "/Test.txt";
    string device_info;
    string user_info = "tester";
    float curentFps = 0f;
    bool isTester = true;
    
    // Start is called before the first frame update

    [System.Serializable]
    private class DeviceInfo
    {
        public string deviceID;
        public string language;
        public string operatingSystem;
        public string osVersion;
        public string deviceModel;
        public string processorModel;
        public string global_ip;
        public int screenWidth;
        public int screenHeight;
              
    }
    DeviceInfo baseDeviceInfo = new DeviceInfo();
    [System.Serializable]
    private class EventDetails
    {
        public string ed1;
        public string ed2;
        public string ed3;
    }
    [System.Serializable]
    private class UserInfo
    {
        public string uuid;
        public int cuurentCoins;
        public int currentScores;
        public int bestRaceScores; 
        public string installDate;
       
    }
    [System.Serializable]
    private class AnalyticsEvent
    {
        public DeviceInfo device_info;
        public EventDetails event_details;
        public UserInfo userInfo;
        public string event_name;
    }

    void Start()
    {
         baseDeviceInfo.deviceID = SystemInfo.deviceUniqueIdentifier.ToString();
         baseDeviceInfo.language = Application.systemLanguage.ToString();
         baseDeviceInfo.operatingSystem = SystemInfo.operatingSystem.ToString();
         baseDeviceInfo.osVersion = System.Environment.OSVersion.Version.ToString();
         baseDeviceInfo.deviceModel = SystemInfo.deviceModel.ToString();
         baseDeviceInfo.processorModel = SystemInfo.processorType.ToString();
         baseDeviceInfo.global_ip = "0.0.0.0";
         baseDeviceInfo.screenWidth = Screen.currentResolution.width;
         baseDeviceInfo.screenHeight = Screen.currentResolution.height;
        //Debug.Log("Operating System: " + SystemInfo.operatingSystem.ToString());
        
        StartCoroutine(GetIPAdress());
       // Debug.Log("1");



    }

    IEnumerator GetIPAdress()
    {
        string url = "https://api.ipify.org/";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
            //Debug.Log("Get request successful!");
            // Debug.Log("Response: " + webRequest.downloadHandler.text);
            baseDeviceInfo.global_ip = webRequest.downloadHandler.text;
            //Debug.Log(baseDeviceInfo.global_ip);
            }
            else
            {
                Debug.Log("Error: " + webRequest.error);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
         curentFps = 1.0f / Time.deltaTime;
    }

   public void EmitAnalyticsEvent(string event_name, string event_details)
    {
       AnalyticsEvent analyticsEvent = new AnalyticsEvent();
       analyticsEvent.device_info = baseDeviceInfo;
       analyticsEvent.event_name = event_name;
        StartCoroutine(SendEvent(analyticsEvent));
       // SendEvent(analyticsEvent);
        Debug.Log(event_name);
        // File.WriteAllText(path, "{\"event_name\" : \"" + event_name + "\"," + "\"event_details\" : \"" + event_details + "\"," + "\"device_info\" : \"" + device_info + "\"," + "\"user_info\" : \"" + user_info + "\"}");
        // Debug.Log(path);
    }
    IEnumerator SendEvent(AnalyticsEvent analyticsEvent)
    {
        // URL, на который будем отправлять POST-запрос
        string url = "https://asgavril.ru/stat/log_event.php";
        Debug.Log("11");

        //string jsonData = JsonUtility.ToJson(analyticsEvent);
        string jsonData = "{\"event_name\":\"" + analyticsEvent.event_name + "\", \"device_info\":\"" + JsonUtility.ToJson(analyticsEvent.device_info).Replace("\"","\\\"") + "\"}";
        Debug.Log(jsonData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        var request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Post request successful!");
            Debug.Log("Response: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error: " + request.error);
        }
    }
}
