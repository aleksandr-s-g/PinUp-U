using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameMode = "journey";
    public void onGameModeJourneyClicked(bool state)
    {
        if (state) 
        {
            gameMode = "journey";
        }
    }
    public void onGameModeRaceClicked(bool state)
    {
        if (state)
        {
            gameMode = "race";
        }
    }
    public void onStartClicked()
    {
         if (gameMode == "journey")
        {
            SceneManager.LoadScene("GameJourney", LoadSceneMode.Single);

        }
        if (gameMode == "Race")
        {
            SceneManager.LoadScene("GameRace", LoadSceneMode.Single);

        }
        Debug.Log(gameMode);

    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
