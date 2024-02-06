using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameControllerRace : MonoBehaviour
{
    public GameObject Ball;
    public GameObject HUDRace;
    public GameObject SaveManager;
    public GameObject LosePanel1;
    public GameObject LosePanel2;
    public GameObject Camera;
    
    public float loseDistance = 30f;
    public int resetDistance = 3;
    int resetTargetY = 0;
    int currentScore;
    int currentBest;
    int currentCoins;
    bool isLoosing = false;
    bool isTimerExpired1 = false;
    bool isTimerExpired2 = false;
    public float looseTimer = 5f;
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
        LosePanel1.SetActive(false);
        LosePanel2.SetActive(false);
        saveManager = SaveManager.GetComponent<SaveManager>();
        
        currentScore = 0;
        currentCoins = saveManager.getCoins();
        currentBest = saveManager.getBestRace();
        hudrace.SetBestRace(currentBest);
        hudrace.SetButtonInteractable(true);
        isLoosing = false;
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
        
        if (currentScore > currentBest)
        {
            currentBest = currentScore;
            hudrace.SetBestRace(currentBest);
            saveManager.setBestRace(currentBest);
        }
         hudrace.SetScores(currentScore);
         hudrace.SetCoins(currentCoins);
        if (!isLoosing) 
        {
            if (Ball.transform.position.y < Camera.transform.position.y - loseDistance)
            {
                LosePanel1.SetActive(true);
                isLoosing = true;
                hudrace.SetTimer(looseTimer);
            }
        }
        
        if (isLoosing) 
        {
            if(!isTimerExpired1 || !isTimerExpired2)
            {
                looseTimer = looseTimer - Time.deltaTime;
                hudrace.SetTimer(looseTimer);
                if (looseTimer < 0)
                {
                    if (!isTimerExpired1)
                    {
                        isTimerExpired1 = true;
                        looseTimer = 5f;
                        LosePanel1.SetActive(false);
                        LosePanel2.SetActive(true);
                        if (currentCoins <= 10) hudrace.SetButtonInteractable(false);
                    }
                    else
                    {
                        isTimerExpired2 = true;
                        looseTimer = 0f;
                        hudrace.SetButtonInteractable(false);
                    }
                }
            }
            if (Ball.transform.position.y > Camera.transform.position.y - (loseDistance))
            {
                LosePanel1.SetActive(false);
                LosePanel2.SetActive(false);
                isLoosing = false;
                looseTimer = 5f;
                isTimerExpired1 = false;
                isTimerExpired2 = false;
                hudrace.SetButtonInteractable(true);
            }
        }
       // Debug.Log(currentScore);
       // Debug.Log(currentCoins);
    }
    public void DoubleTap()
    {
        //Debug.Log("Double tap detected!");
        if (isTimerExpired1 && !isTimerExpired2)
        {
            if (currentCoins >= 10)
            {
                currentCoins = currentCoins - 10;
                resetTargetY = (Mathf.FloorToInt(Camera.transform.position.y / 20) + resetDistance) * 20;
                Ball.GetComponent<Ball>().ResetBall(resetTargetY);
            }
        }
        if (!isTimerExpired1 && !isTimerExpired2)
        {
            if (currentCoins >= 5)
            {
                currentCoins = currentCoins - 5;
                resetTargetY = (Mathf.FloorToInt(Camera.transform.position.y / 20) + resetDistance) * 20;
                Ball.GetComponent<Ball>().ResetBall(resetTargetY);
            }
        }
        
    }
   // public int getCurretnScore()
   // {
   //     return currentScore;
   // }
}
