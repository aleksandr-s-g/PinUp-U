using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FBAnalitycs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!FB.IsInitialized) {
        // Initialize the Facebook SDK
        FB.Init(InitCallback, OnHideUnity);
        } else {
        // Already initialized, signal an app activation App Event
        FB.ActivateApp(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void InitCallback ()
    {
    if (FB.IsInitialized) {
        // Signal an app activation App Event
        FB.ActivateApp();
        // Continue with Facebook SDK
        // ...
    } else {
        Debug.Log("Failed to Initialize the Facebook SDK");
    }
    EmitFBAnalyticsEvent("launch", "");
    }

private void OnHideUnity (bool isGameShown)
{
    if (!isGameShown) {
        // Pause the game - we will need to hide
        Time.timeScale = 0;
    } else {
        // Resume the game - we're getting focus again
        Time.timeScale = 1;
    }

}
public void EmitFBAnalyticsEvent(string event_name, string event_details)
{
    if(!FB.IsInitialized)
    {
        Debug.Log("FBA: Not inited");
        return;
    }
    var tutParams = new Dictionary<string, object>();
    tutParams[AppEventParameterName.Description] = event_details;
    FB.LogAppEvent (
        event_name,
        parameters: tutParams
    );
    Debug.Log("FBA: "+event_name);
}

}
