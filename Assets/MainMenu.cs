using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public string gameMode = "journey";
    public GameObject SaveManager;
    
    public GameObject TesterPanel;
    public GameObject TestModePanel;
    public GameObject Minimize;
    public GameObject RewardedDialog;
    public GameObject RewardedSucceedMsg;
    float fpsTimer = 0f;
    float fpsSecondPerUpdate = 1f;
    SaveManager saveManager;
    public Toggle toggleRace;
    public Toggle toggleJourney;
    public ToggleGroup toggleGroup;
    public float testerClickInterval = 0.3f; 
    public int testerClickCountTarget = 10; 
    private int testerClickCount = 0; 
    private float testerLastClickTime = 0f;
    MainController mainController;

    public TextMeshProUGUI testHeadText;
    public TextMeshProUGUI testBodyText;
    bool isTesterModeOn = false;

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
                isTesterModeOn = TestModePanel.activeSelf;
                mainController.SetTesterModeIsOn(isTesterModeOn);
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
    public void onRewardedWatchClicked()
    {
        
        mainController.onRewardedClicked();
        RewardedDialog.SetActive(false);
        mainController.EmitAnalyticsEvent("rewarded_clicked", gameMode.ToString(), "", "");
    }
    public void onRewardedClicked()
    {
        RewardedDialog.SetActive(true);
    }
    public void onRewardedCloseClicked()
    {
        RewardedDialog.SetActive(false);
    }

    public void onRewardedSucceedMsgCloseClicked(){
        RewardedSucceedMsg.SetActive(false);
    }
    public void rewardedSucceed(){
        saveManager.setCoins(saveManager.getCoins()+100);
        RewardedSucceedMsg.SetActive(true);
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
        isTesterModeOn = TestModePanel.activeSelf;
        mainController.SetTesterModeIsOn(isTesterModeOn);
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
        
        mainController = GameObject.FindGameObjectWithTag("MainTag").GetComponent<MainController>();
        isTesterModeOn = mainController.GetTesterModeIsOn();
        gameMode = saveManager.getGameMode();
        //TestModePanel.SetActive(isTesterModeOn);
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
        //mainController.GetBaseDeviceInfo();
        //testHeadText.text = "TEST MODE\r\nFPS: " + mainController.GetBaseDeviceInfo().fps.ToString();
        /*if (isTesterModeOn)
        {
            fpsTimer = fpsTimer + Time.deltaTime;
            if (fpsTimer > fpsSecondPerUpdate)
            {
                fpsTimer = 0;
                testHeadText.text = "TEST MODE\r\nFPS: " + mainController.GetBaseDeviceInfo().fps.ToString();
               /* testBodyText.text = "UUID: " + mainController.GetBaseUserInfo().uuid.ToString() +
                    "\r\nIP: " + mainController.GetBaseDeviceInfo().global_ip.ToString() +
                    "\r\nDeviceInfo:" + mainController.GetBaseDeviceInfo().ToString();  
            }
            
        }*/
        
        

    }

}
