using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{
    public string gameMode = "journey";
    public GameObject SaveManager;
    
    public GameObject TesterPanel;
    public GameObject TestModePanel;
    public GameObject Minimize;
     
    
    SaveManager saveManager;
    public Toggle toggleRace;
    public Toggle toggleJourney;
    public ToggleGroup toggleGroup;
    public float testerClickInterval = 0.3f; 
    public int testerClickCountTarget = 10; 
    private int testerClickCount = 0; 
    private float testerLastClickTime = 0f;
    MainController mainController;



    public void onTesterButtonClicked()
    {
        float currentTime = Time.time;

        
        if (currentTime - testerLastClickTime < testerClickInterval)
        {
            testerClickCount++;

            if (testerClickCount >= testerClickCountTarget)
            {

               // Debug.Log("is tester");
                TesterPanel.SetActive(!TesterPanel.activeSelf);
                TestModePanel.SetActive(!TesterPanel.activeSelf);
                saveManager.setTester(true);
                testerClickCount = 0;
                if (TesterPanel.activeSelf)
                {
                    mainController.EmitAnalyticsEvent("tester_panel", "on", "", "");
                }
                else
                {
                    mainController.EmitAnalyticsEvent("tester_panel", "off", "", "");
                }
                
            }
            else
            {
               // Debug.Log(testerClickCount);
            }
        }
        else
        {
            
            testerClickCount = 1;
            //Debug.Log(testerClickCount);
        }

        
        testerLastClickTime = currentTime;
    }
    public void onGameModeJourneyClicked(bool state)
    {
        if (state) 
        {
            gameMode = "journey";
            mainController.EmitAnalyticsEvent("journey_mod_selected", "", "", "");
        }
    }
    public void onGameModeRaceClicked(bool state)
    {
        if (state)
        {
            gameMode = "race";
            mainController.EmitAnalyticsEvent("race_mod_selected", "", "", "");
        }
    }
   public void onStartClicked()
    {
        /* if (gameMode == "journey")
         {
             SceneManager.LoadScene("GameJourney", LoadSceneMode.Single);
             saveManager.setGameMode(gameMode);

         }
         if (gameMode == "race")
         {
             SceneManager.LoadScene("GameRace", LoadSceneMode.Single);
             saveManager.setGameMode(gameMode);
         }
         analytics.EmitAnalyticsEvent("start_clicked", gameMode.ToString(), "", "");*/
        // startClicked.Invoke(gameMode);
        mainController.onStartClicked(gameMode);

    }
    public void onSetCoins1000Clicked()
    {
        saveManager.setCoins(1000);
        mainController.EmitAnalyticsEvent("tester_set_coins", gameMode.ToString(), "1000", "");
       // Debug.Log("coins 1000");
    }
    public void onSetCoins0Clicked()
    {
        saveManager.setCoins(0);
        mainController.EmitAnalyticsEvent("tester_set_coins", gameMode.ToString(), "0", "");
        //Debug.Log("coins 0");
    }
    public void onTesterCloseClicked()
    {
        TestModePanel.SetActive(false);
    }
    public void onTesterMinimizeClicked()
    {
        
        TesterPanel.SetActive(!TesterPanel.activeSelf);

        Minimize.transform.Rotate(0, 0, 180);

    }
    
    void Start()
    {
        toggleRace.group = toggleGroup;
        toggleJourney.group = toggleGroup;
        saveManager = SaveManager.GetComponent<SaveManager>();
        
        gameMode = saveManager.getGameMode();
        mainController = GameObject.FindGameObjectWithTag("MainTag").GetComponent<MainController>();
        
        TestModePanel.SetActive(false);
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
