using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameControllerRace : MonoBehaviour
{
    public GameObject Ball;
    public GameObject HUDRace;
    public GameObject SaveManager;
    public GameObject LosePanel;
    public GameObject Camera;
    public float loseDistance = 30f;
    int currentScore;
    int currentCoins;    
    SaveManager saveManager;
    HUDRace hudrace;

    private void OnEnable()
    {
        Ball.GetComponent<Ball>().onCoinCollected+=CoinCollected;
    }
    private void OnDisable()
    {
        //Ball.onCoinCollected-=CoinCollected;
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 240;
        hudrace = HUDRace.GetComponent<HUDRace>();
        LosePanel.SetActive(false);
        saveManager = SaveManager.GetComponent<SaveManager>();
        currentScore = 0;
        currentCoins = saveManager.getCoins();
        
    }
    public void CoinCollected()
    {
        currentCoins++;
        saveManager.setCoins(currentCoins);
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Ball.transform.position.y > currentScore)
        {
            currentScore = (int)Ball.transform.position.y;            
        }
        if (Ball.transform.position.y < Camera.transform.position.y-loseDistance)
        {
            LosePanel.SetActive(true);
        }
         hudrace.SetScores(currentScore);
         hudrace.SetCoins(currentCoins);
       // Debug.Log(currentScore);
       // Debug.Log(currentCoins);
    }
}
