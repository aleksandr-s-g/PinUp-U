using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDRace : MonoBehaviour
{
    public TextMeshProUGUI scoresLabel;
    public TextMeshProUGUI bestraceLabel;
    public TextMeshProUGUI coinsLabel;
    public TextMeshProUGUI fPSLabel;
    public TextMeshProUGUI youWillLoseInLabel;
    public TextMeshProUGUI looseScoreLabel;
    public TextMeshProUGUI countDownLabel;
    public GameObject Spinner;
    public GameObject GameController;
    public Button button;
    public float rotationSpeed = 90f;
    // Start is called before the first frame update
    
    MainController mainController;
    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;
    void Start()
    {
        scoresLabel.text = "888";
        //button = GetComponent<Button>();
        
        mainController = GameObject.FindGameObjectWithTag("MainTag").GetComponent<MainController>();
    }

    // Update is called once per frame
    void Update()

    {
        
        if (m_timeCounter < m_refreshTime)
        {
            m_timeCounter += Time.deltaTime;
            m_frameCounter++;

        }
        else
        {
            //This code will break if you set your m_refreshTime to 0, which makes no sense.
            m_lastFramerate = (float)m_frameCounter / m_timeCounter;
            m_frameCounter = 0;
            m_timeCounter = 0.0f;
        }
        ShowFPS();
        float currentRotation = Spinner.transform.rotation.eulerAngles.z;

        
        float newRotation = currentRotation + rotationSpeed * Time.deltaTime;

        
        Spinner.transform.rotation = Quaternion.Euler(0f, 0f, newRotation);

    }

    public void SetScores(int scores)
    {
        scoresLabel.text = scores.ToString();
    }
    public void SetBestRace(int bestrace)
    {
        bestraceLabel.text = "Best: " + bestrace.ToString();
    }
    public void SetTimer(float timer)
    {
        youWillLoseInLabel.text = timer.ToString("F2");
        countDownLabel.text = timer.ToString("F2") + "s";
    }
    public void SetLooseScore(int scores)
    {
        looseScoreLabel.text =  "Score: " + scores.ToString();
    }
    public void SetCoins(int coins)
    {
        coinsLabel.text = coins.ToString();
    }
    public void ShowFPS()
    {
        fPSLabel.text = ((int)m_lastFramerate).ToString();
    }

    public void onBackButtonClicked()
    {
        mainController.EmitAnalyticsEvent("back_button_clicked", "race", "", "");
        // SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        mainController.onBackButtonClicked("GameRace");

    }
    public void onRestartClickedRace()
    {
        mainController.EmitAnalyticsEvent("restart_clicked", "", "", "");
        mainController.onRaceRestart();
        //SceneManager.LoadScene("GameRace", LoadSceneMode.Single);
    }
    public void onReslifeClickedRace()
    {

        mainController.EmitAnalyticsEvent("relife_clicked", "", "", "");
        GameController.GetComponent<GameControllerRace>().DoubleTap(); 
        
    }
    public void SetButtonInteractable(bool interactable)
    {
        button.interactable = interactable;
        
    }
}
