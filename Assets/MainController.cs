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

    public GameObject FBAnalitycs;
    public GameObject Analytics;
    public GameObject ADManager;
    FBAnalitycs fbAnalitycs;
    Analytics analytics;
    ADManager adManager;
    bool isTesterModeOn = false;
    float lastInterstitialAdTime;
    float periodInterstitialAdTime = 180.0f;


    // Start is called before the first frame update
    void Start()
    {
        fbAnalitycs = FBAnalitycs.GetComponent<FBAnalitycs>();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        analytics = Analytics.GetComponent<Analytics>();
        adManager = ADManager.GetComponent<ADManager>();
        analytics.EmitAnalyticsEvent("launch", "", "", "");
        lastInterstitialAdTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tryShowInterstitial()
    {
        float nowTime = Time.time;
        Debug.Log("tryShowInterstitial " + nowTime);
        if (nowTime > lastInterstitialAdTime+periodInterstitialAdTime)
        {
            lastInterstitialAdTime = nowTime;
            adManager.ShowInterstitial();
            //ShowIntersritial
        }
    }
    public void onRewardedClicked()
    {
        adManager.ShowRewarded();
        
    }
    public void onApplovinClicked()
    {
        adManager.ShowMediationDebugger();
    }
    public void onStartClicked(string gameMode)
    {

        //Debug.Log("onStartClicked");
        fbAnalitycs.EmitFBAnalyticsEvent("start_clicked", gameMode);

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

    public void onRaceRestart()
    {
        SceneManager.UnloadSceneAsync("GameRace");
        SceneManager.LoadScene("GameRace", LoadSceneMode.Additive);
    }

    public void EmitAnalyticsEvent(string event_name, string ed1, string ed2, string ed3)
    {
        Debug.Log("IA: " + event_name);
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
