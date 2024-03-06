using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string gameMode = "journey";
    public GameObject SaveManager;
    public GameObject Analytics;
    Analytics analytics;
    SaveManager saveManager;
    public Toggle toggleRace;
    public Toggle toggleJourney;
    public ToggleGroup toggleGroup;
    public float testerClickInterval = 0.5f; 
    public int testerClickCountTarget = 10; 
    private int testerClickCount = 0; 
    private float testerLastClickTime = 0f; 
    private bool isTester = false;
    public void onTesterButtonClicked()
    {
        float currentTime = Time.time;

        
        if (currentTime - testerLastClickTime < testerClickInterval)
        {
            testerClickCount++;

            if (testerClickCount >= testerClickCountTarget)
            {
                
                Debug.Log("is tester");
                isTester = true;
            }
            else
            {
                Debug.Log(testerClickCount);
            }
        }
        else
        {
            
            testerClickCount = 1;
            Debug.Log(testerClickCount);
        }

        
        testerLastClickTime = currentTime;
    }
    public void onGameModeJourneyClicked(bool state)
    {
        if (state) 
        {
            gameMode = "journey";
            analytics.EmitAnalyticsEvent("journey_mod_selected", "", "", "");
        }
    }
    public void onGameModeRaceClicked(bool state)
    {
        if (state)
        {
            gameMode = "race";
            analytics.EmitAnalyticsEvent("race_mod_selected", "", "", "");
        }
    }
    public void onStartClicked()
    {
        if (gameMode == "journey")
        {
            SceneManager.LoadScene("GameJourney", LoadSceneMode.Single);
            saveManager.setGameMode(gameMode);

        }
        if (gameMode == "race")
        {
            SceneManager.LoadScene("GameRace", LoadSceneMode.Single);
            saveManager.setGameMode(gameMode);
        }
        analytics.EmitAnalyticsEvent("start_clicked", gameMode.ToString(), "", "");

    }
    // Start is called before the first frame update
    void Start()
    {
        toggleRace.group = toggleGroup;
        toggleJourney.group = toggleGroup;
        saveManager = SaveManager.GetComponent<SaveManager>();
        analytics = Analytics.GetComponent<Analytics>();
        gameMode = saveManager.getGameMode();
        
        if (gameMode != "journey" && gameMode != "race")
        {
            gameMode = "journey";
            saveManager.setGameMode(gameMode);
        }
        if (gameMode == "race")
        {
            toggleRace.isOn = true;
        }
        if (gameMode == "journey")
        {
            toggleJourney.isOn = true;
        }
        analytics.EmitAnalyticsEvent("launch", "", "", "");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
