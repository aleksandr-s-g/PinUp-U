using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameControllerJourney : MonoBehaviour
{
    public GameObject Ball;
    public GameObject HUDJourney;
    public GameObject SaveManager;
    public GameObject Analytics;
    Analytics analytics;

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
        Application.targetFrameRate = 240;
        hudjourney = HUDJourney.GetComponent<HUDJourney>();
        analytics = Analytics.GetComponent<Analytics>();
        saveManager = SaveManager.GetComponent<SaveManager>();
        currentScore = 0;
        currentCoins = saveManager.getCoins(); ;
        loadedScore = saveManager.getScores();
        analytics.EmitAnalyticsEvent("journey_started", "", "", "");
    }
    public void CoinCollected()
    {
        currentCoins++;
        saveManager.setCoins(currentCoins);
        analytics.EmitAnalyticsEvent("coin_collected", "journey", "", "");
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
