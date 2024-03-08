using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;
using static UnityEngine.InputSystem.LowLevel.InputEventTrace;
using UnityEngine.Networking;
using System.Net;
using UnityEditor;

public class Analytics : MonoBehaviour
{
    public GameObject SaveManager;
    SaveManager saveManager;
    int curentFps = 0;
   
    
    // Start is called before the first frame update

    [System.Serializable]
    public class DeviceInfo
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
        public int fps;
              
    }
    DeviceInfo baseDeviceInfo = new DeviceInfo();
    [System.Serializable]
    private class EventDetails
    {
        public string ed1;
        public string ed2;
        public string ed3;
    }
    EventDetails eventDetails = new EventDetails();
    [System.Serializable]
    public class UserInfo
    {
        public string uuid;
        public int cuurentCoins;
        public int currentScores;
        public int bestRaceScores; 
        public string installDate;
        public string gameVersion;
        public string buildNumber;
        public bool isTester;
       
    }
    UserInfo baseUserInfo = new UserInfo();
    [System.Serializable]
    private class AnalyticsEvent
    {
        public DeviceInfo device_info;
        public EventDetails event_details;
        public UserInfo user_info;
        public string event_name;
    }

    void Start()
    {
        saveManager = SaveManager.GetComponent<SaveManager>();
        baseDeviceInfo.deviceID = SystemInfo.deviceUniqueIdentifier.ToString();
        baseDeviceInfo.language = Application.systemLanguage.ToString();
        baseDeviceInfo.operatingSystem = SystemInfo.operatingSystem.ToString();
        baseDeviceInfo.osVersion = System.Environment.OSVersion.Version.ToString();
        baseDeviceInfo.deviceModel = SystemInfo.deviceModel.ToString();
        baseDeviceInfo.processorModel = SystemInfo.processorType.ToString();
        baseDeviceInfo.global_ip = "0.0.0.0";
        baseDeviceInfo.screenWidth = Screen.currentResolution.width;
        baseDeviceInfo.screenHeight = Screen.currentResolution.height;
        baseDeviceInfo.fps = 0;
        baseUserInfo.uuid = saveManager.getUuid();
        baseUserInfo.cuurentCoins = saveManager.getCoins();
        baseUserInfo.currentScores = saveManager.getScores();
        baseUserInfo.bestRaceScores = saveManager.getBestRace();
        baseUserInfo.installDate = saveManager.getInstallDate();
        baseUserInfo.gameVersion = Application.version;
        baseUserInfo.buildNumber = GetBuildNumber();
        baseUserInfo.isTester = saveManager.getTester();
        //Debug.Log(baseUserInfo.uuid);
        //Debug.Log("Operating System: " + SystemInfo.operatingSystem.ToString());

        StartCoroutine(GetIPAdress());
        // Debug.Log("1");



    }
    private string GetBuildNumber()
    {

        return ("undifined");
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
                //Debug.Log("Error: " + webRequest.error);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        curentFps = Mathf.FloorToInt(1.0f / Time.deltaTime);
        baseDeviceInfo.fps = curentFps;
        //Debug.Log(curentFps);
    }

   public void EmitAnalyticsEvent(string event_name, string ed1, string ed2, string ed3)
    {
        event_name = "test-" + event_name;
        eventDetails.ed1 = ed1;
        eventDetails.ed2 = ed2;
        eventDetails.ed3 = ed3;
        AnalyticsEvent analyticsEvent = new AnalyticsEvent();
        analyticsEvent.device_info = baseDeviceInfo;
        analyticsEvent.event_name = event_name;
        analyticsEvent.user_info = baseUserInfo;
        analyticsEvent.event_details = eventDetails;
        StartCoroutine(SendEvent(analyticsEvent));
       
        //Debug.Log(event_name);
        
    }
    public DeviceInfo GetBaseDeviceInfo()
    {
        return baseDeviceInfo;
    }
    public UserInfo GetBaseUserInfo()
    {
        return baseUserInfo;
    }
    IEnumerator SendEvent(AnalyticsEvent analyticsEvent)
    {
        // URL, на который будем отправлять POST-запрос
        string url = "https://asgavril.ru/stat/log_event.php";
        //Debug.Log("11");

        //string jsonData = JsonUtility.ToJson(analyticsEvent);
        string jsonData = "{\"event_name\":\"" +
            analyticsEvent.event_name +
            "\", \"event_details\":\"" + JsonUtility.ToJson(analyticsEvent.event_details).Replace("\"", "\\\"") +
            "\", \"device_info\":\"" + JsonUtility.ToJson(analyticsEvent.device_info).Replace("\"","\\\"") +
            "\", \"user_info\":\"" + JsonUtility.ToJson(analyticsEvent.user_info).Replace("\"", "\\\"") +
            "\"}";
        //Debug.Log(jsonData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        var request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log("Post request successful!");
            //Debug.Log("Response: " + request.downloadHandler.text);
        }
        else
        {
            //Debug.Log("Error: " + request.error);
        }
    }
}
