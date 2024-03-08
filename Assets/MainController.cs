using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Facebook.Unity;

public class MainController : MonoBehaviour
{
   /* void Awake()
    {
        GetComponent<MainMenu>().startClicked.AddListener(onStartClicked);
    }    */
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        Debug.Log("FB.IsInitialized1: " + FB.IsInitialized);
        if (!FB.IsInitialized) {
        // Initialize the Facebook SDK
        FB.Init(InitCallback, OnHideUnity);
        } else {
        // Already initialized, signal an app activation App Event
        FB.ActivateApp(); 
        }
        

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onStartClicked(string gameMode)
    {
        Debug.Log("onStartClicked");
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
    Debug.Log("FB.IsInitialized2: " + FB.IsInitialized);
    var tutParams = new Dictionary<string, object>();
    tutParams[AppEventParameterName.ContentID] = "pinup-u-started";
    tutParams[AppEventParameterName.Description] = "some event desctiption";
    tutParams[AppEventParameterName.Success] = "1";

    FB.LogAppEvent (
        AppEventName.CompletedTutorial,
        parameters: tutParams
    );
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
}
