using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MainController : MonoBehaviour
{
   /* void Awake()
    {
        GetComponent<MainMenu>().startClicked.AddListener(onStartClicked);
    }    */
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onStartClicked(string gameMode)
    {
        Debug.Log("onStartClicked");
        if (gameMode == "journey")
        {
            SceneManager.LoadScene("GameJourney", LoadSceneMode.Additive);
            //saveManager.setGameMode(gameMode);
            SceneManager.UnloadSceneAsync("MainMenu");
        }
        if (gameMode == "race")
        {
            SceneManager.LoadScene("GameRace", LoadSceneMode.Additive);
            //saveManager.setGameMode(gameMode);
            SceneManager.UnloadSceneAsync("MainMenu");
        }
        //analytics.EmitAnalyticsEvent("start_clicked", gameMode.ToString(), "", "");
        Debug.Log("1");

    }
    public void onBackButtonClicked(string gameMode)
    {
        //analytics.EmitAnalyticsEvent("back_button_clicked", gameMode, "", "");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameMode);
    }
}
