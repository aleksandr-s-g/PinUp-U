using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class GameControllerJourney : MonoBehaviour
{
    public UnityEvent<string> myEvent;
    public GameObject Ball;
    public GameObject HUDJourney;
    public GameObject SaveManager;
    MainController mainController;

    int currentScore;
    int currentCoins;
    int loadedScore;
    SaveManager saveManager;
    HUDJourney hudjourney;


    private void OnEnable()
    {
        Ball.GetComponent<Ball>().onCoinCollected += CoinCollected;
    }
    private void OnDisable()
    {
        //Ball.onCoinCollected-=CoinCollected;
    }
    // Start is called before the first frame update
    void Start()
    {
        mainController = GameObject.FindGameObjectWithTag("MainTag").GetComponent<MainController>();
        Application.targetFrameRate = 240;
        hudjourney = HUDJourney.GetComponent<HUDJourney>();
        
        saveManager = SaveManager.GetComponent<SaveManager>();
        currentScore = 0;
        currentCoins = saveManager.getCoins();
        loadedScore = saveManager.getScores();
        mainController.EmitAnalyticsEvent("journey_started", "", "", "");
    }
    public void CoinCollected()
    {
        currentCoins = saveManager.getCoins();
        currentCoins++;
        saveManager.setCoins(currentCoins);
        mainController.EmitAnalyticsEvent("coin_collected", "journey", "", "");
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Ball.transform.position.y > currentScore)
        {
            currentScore = (int)Ball.transform.position.y;
            saveManager.setScores(currentScore + loadedScore);
        }
        hudjourney.SetScores(currentScore + loadedScore);
        hudjourney.SetCoins(currentCoins);
    }
    public void DoubleTap()
    {
      
    }
}
