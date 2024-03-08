using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using static Analytics;

public class MainController : MonoBehaviour
{
    /* void Awake()
     {
         GetComponent<MainMenu>().startClicked.AddListener(onStartClicked);
     }    */

    public GameObject Analytics;
    Analytics analytics;
    bool isTesterModeOn = false;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        analytics = Analytics.GetComponent<Analytics>();
        analytics.EmitAnalyticsEvent("launch", "", "", "");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onStartClicked(string gameMode)
    {
        
        if (gameMode == "journey")
        {
            SceneManager.LoadScene("GameJourney", LoadSceneMode.Additive);
            //saveManager.setGameMode(gameMode);
            SceneManager.UnloadSceneAsync("MainMenu");
        }
        if (gameMode == "race")
        {
            SceneManager.LoadScene("GameRace", LoadSceneMode.Additive);
            //saveManager.setGameMode(gameMode);
            SceneManager.UnloadSceneAsync("MainMenu");
        }
        //analytics.EmitAnalyticsEvent("start_clicked", gameMode.ToString(), "", "");
        

    }
    public void onBackButtonClicked(string gameMode)
    {
        //analytics.EmitAnalyticsEvent("back_button_clicked", gameMode, "", "");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameMode);
    }
    public void EmitAnalyticsEvent(string event_name, string ed1, string ed2, string ed3)
    {
        analytics.EmitAnalyticsEvent(event_name, ed1, ed2, ed3);

    }
    public DeviceInfo GetBaseDeviceInfo()
    {
        return analytics.GetBaseDeviceInfo();        
    }
    public UserInfo GetBaseUserInfo()
    {
        return analytics.GetBaseUserInfo();
    }
    public void SetTesterModeIsOn(bool isOn)
    {
        isTesterModeOn = isOn;
    }
    public bool GetTesterModeIsOn()
    {
        return isTesterModeOn;
    }
}
