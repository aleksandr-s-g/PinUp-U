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
    public GameObject TesterPanel;
    Analytics analytics;
    SaveManager saveManager;
    public Toggle toggleRace;
    public Toggle toggleJourney;
    public ToggleGroup toggleGroup;
    public float testerClickInterval = 0.3f; 
    public int testerClickCountTarget = 10; 
    private int testerClickCount = 0; 
    private float testerLastClickTime = 0f; 
    
    public void onTesterButtonClicked()
    {
        float currentTime = Time.time;

        
        if (currentTime - testerLastClickTime < testerClickInterval)
        {
            testerClickCount++;

            if (testerClickCount >= testerClickCountTarget)
            {

                Debug.Log("is tester");
                TesterPanel.SetActive(!TesterPanel.activeSelf);
                saveManager.setTester(true);
                testerClickCount = 0;
                if (TesterPanel.activeSelf)
                {
                    analytics.EmitAnalyticsEvent("tester_panel", "on", "", "");
                }
                else
                {
                    analytics.EmitAnalyticsEvent("tester_panel", "off", "", "");
                }
                
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
    public void onSetCoins1000Clicked()
    {
        saveManager.setCoins(1000);
        analytics.EmitAnalyticsEvent("tester_set_coins", gameMode.ToString(), "1000", "");

    }
    public void onSetCoins0Clicked()
    {
        saveManager.setCoins(0);
        analytics.EmitAnalyticsEvent("tester_set_coins", gameMode.ToString(), "0", "");

    }
    // Start is called before the first frame update
    void Start()
    {
        toggleRace.group = toggleGroup;
        toggleJourney.group = toggleGroup;
        saveManager = SaveManager.GetComponent<SaveManager>();
        analytics = Analytics.GetComponent<Analytics>();
        gameMode = saveManager.getGameMode();
        TesterPanel.SetActive(false);
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
