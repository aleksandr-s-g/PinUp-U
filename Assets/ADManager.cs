using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class ADManager : MonoBehaviour
{
    public GameObject MainMenu;
    MainMenu mainMenu;
    public GameObject MainController;
    public GameObject Analytics;
    Analytics analytics;
    // Start is called before the first frame update
    void Start()
    {       analytics = Analytics.GetComponent<Analytics>();

            MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            //Debug.Log("AppLovin SDK is initialized, start loading ads");
            analytics.EmitAnalyticsEvent("applovin_sdk_inited", "", "", "");
            //MaxSdk.ShowMediationDebugger();
            InitializeRewardedAds();
            InitializeInterstitialAds();
    // AppLovin SDK is initialized, start loading ads
        }       ;

        //MaxSdk.SetSdkKey("rKJKJNGINemT8rUU6mCedTQVI9FEqSvqsOusK0i28brptIb5szmTrn4GcZEOyCcOzWhI8I7SQrWdnF1GdxeCUw");
        //MaxSdk.SetSdkKey("sdgndnetyjehtyjhetyjetyjnetyjnetyjhetyj");
        //MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
    }

#if UNITY_IOS
string adUnitId = "1d7abf7ca7fa580d";
#else // UNITY_ANDROID
string adUnitId = "44804c914b209391";
#endif
int retryAttempt;

public void ShowMediationDebugger(){
    MaxSdk.ShowMediationDebugger();
    analytics.EmitAnalyticsEvent("applovin_show_debugger", "", "", "");
}

public void InitializeRewardedAds()
{
    Debug.Log("Applovin - InitializeRewardedAds");
    // Attach callback
    MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
    MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
    MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
    MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
    MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
    MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
    MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
    MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
            
    // Load the first rewarded ad
    LoadRewardedAd();
}

    private void LoadRewardedAd()
    {
           // Debug.Log("Applovin - LoadRewardedAd");
        MaxSdk.LoadRewardedAd(adUnitId);
        analytics.EmitAnalyticsEvent("applovin_rewarded_try_load", "", "", "");
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.Log("Applovin - OnRewardedAdLoadedEvent");
        analytics.EmitAnalyticsEvent("applovin_rewarded_loaded", "", "", "");
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        retryAttempt = 0;
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).
 //Debug.Log("Applovin - OnRewardedAdLoadFailedEvent");
        analytics.EmitAnalyticsEvent("applovin_rewarded_load_failed", "", "", "");
        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));
        
        Invoke("LoadRewardedAd", (float) retryDelay);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {

       // Debug.Log("Applovin - OnRewardedAdDisplayedEvent");
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        //Debug.Log("Applovin - OnRewardedAdFailedToDisplayEvent");
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        LoadRewardedAd();
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {}

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        //Debug.Log("Applovin - OnRewardedAdHiddenEvent");
        analytics.EmitAnalyticsEvent("applovin_rewarded_hidden", "", "", "");
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        mainMenu = GameObject.FindGameObjectWithTag("MainMenuTag").GetComponent<MainMenu>();
        //Debug.Log("Applovin - OnRewardedAdReceivedRewardEvent");
        analytics.EmitAnalyticsEvent("applovin_rewarded_reward", "", "", "");
        mainMenu.rewardedSucceed();
        // The rewarded ad displayed and the user should receive the reward.
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Ad revenue paid. Use this callback to track user revenue.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
public void ShowRewarded()
{
    //Debug.Log("Applovin - ShowRewarded_try0");
    if (MaxSdk.IsRewardedAdReady(adUnitId))
    {
        //Debug.Log("Applovin - ShowRewarded_try1");
        analytics.EmitAnalyticsEvent("applovin_rewarded_show", "", "", "");
        MaxSdk.ShowRewardedAd(adUnitId);
    }
}
#if UNITY_IOS
string adUnitIdInter = "e04f3ab520ab4127";
#else // UNITY_ANDROID
string adUnitIdInter = "16381f122f1aaeda";
#endif
int retryAttemptInter;

public void InitializeInterstitialAds()
{
    // Attach callback
    MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
    MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
    MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
    MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
    MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
    MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;
    
    // Load the first interstitial
    LoadInterstitial();
}

private void LoadInterstitial()
{
    MaxSdk.LoadInterstitial(adUnitIdInter);
    analytics.EmitAnalyticsEvent("applovin_interstitial_try_load", "", "", "");
}

private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
{
    // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'
    analytics.EmitAnalyticsEvent("applovin_interstitial_loaded", "", "", "");
    // Reset retry attempt
    retryAttemptInter = 0;
}

private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
{
    // Interstitial ad failed to load 
    // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)
    analytics.EmitAnalyticsEvent("applovin_interstitial_load_fail", "", "", "");
    retryAttemptInter++;
    double retryDelay = Math.Pow(2, Math.Min(6, retryAttemptInter));
    
    Invoke("LoadInterstitial", (float) retryDelay);
}

private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {}

private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
{
    // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
    LoadInterstitial();
}

private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {}

private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
{
    // Interstitial ad is hidden. Pre-load the next ad.
    LoadInterstitial();
}

public void ShowInterstitial()
{
    if (MaxSdk.IsInterstitialReady(adUnitIdInter))
    {
        analytics.EmitAnalyticsEvent("applovin_interstitial_show", "", "", "");
        MaxSdk.ShowInterstitial(adUnitIdInter);
    }
}

}
