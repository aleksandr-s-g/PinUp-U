using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDJourney : MonoBehaviour
{
    public TextMeshProUGUI scoresLabel;
    public TextMeshProUGUI coinsLabel;
    public TextMeshProUGUI fPSLabel;

    public float testerClickInterval = 0.3f;
    public int testerClickCountTarget = 10;
    private int testerClickCount = 0;
    private float testerLastClickTime = 0f;
    public GameObject TesterPanel;
    public GameObject TestModePanel;
    public GameObject Minimize;
    public GameObject SaveManager;
    SaveManager saveManager;
    MainController mainController;
    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;
    void Start()
    {
        scoresLabel.text = "777";
        
        mainController = GameObject.FindGameObjectWithTag("MainTag").GetComponent<MainController>();
    }

    // Update is called once per frame
    void Update()

    {
       
        if ( m_timeCounter < m_refreshTime )
    {
        m_timeCounter += Time.deltaTime;
        m_frameCounter++;

    }
    else
    {
        //This code will break if you set your m_refreshTime to 0, which makes no sense.
        m_lastFramerate = (float)m_frameCounter/m_timeCounter;
        m_frameCounter = 0;
        m_timeCounter = 0.0f;
    }
    ShowFPS();
    }

    public void SetScores (int scores)
    {
        scoresLabel.text = scores.ToString();
    }
    public void SetCoins (int coins)
    {
        coinsLabel.text = coins.ToString();
    }
    public void ShowFPS ()
    {
        fPSLabel.text = ((int)m_lastFramerate).ToString();
    }

    public void onBackButtonClicked()
    {

        mainController.EmitAnalyticsEvent("back_button_clicked", "journey", "", "");
        //SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        mainController.onBackButtonClicked("GameJourney");
    }
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

}
