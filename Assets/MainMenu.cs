using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string gameMode = "journey";
    public GameObject SaveManager;
    SaveManager saveManager;
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
            saveManager.setGameMode(gameMode);

        }
        if (gameMode == "race")
        {
            SceneManager.LoadScene("GameRace", LoadSceneMode.Single);
            saveManager.setGameMode(gameMode);
        }
        Debug.Log(gameMode);

    }
    // Start is called before the first frame update
    void Start()
    {
        saveManager = SaveManager.GetComponent<SaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
