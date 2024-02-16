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

    public void onGameModeJourneyClicked(bool state)
    {
        if (state) 
        {
            gameMode = "journey";
            analytics.AnalyticsEvent("net", "ty");
        }
    }
    public void onGameModeRaceClicked(bool state)
    {
        if (state)
        {
            gameMode = "race";
            analytics.AnalyticsEvent("ty", "pidor");
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
