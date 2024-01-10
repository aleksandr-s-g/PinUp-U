using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI scoresLabel;
    public TextMeshProUGUI coinsLabel;
    public TextMeshProUGUI fPSLabel;
    // Start is called before the first frame update

    int m_frameCounter = 0;
    float m_timeCounter = 0.0f;
    float m_lastFramerate = 0.0f;
    public float m_refreshTime = 0.5f;
    void Start()
    {
        scoresLabel.text = "777";
    }

    // Update is called once per frame
    void Update()
    {
        if( m_timeCounter < m_refreshTime )
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
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
