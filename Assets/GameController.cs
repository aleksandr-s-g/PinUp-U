using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public GameObject Ball;
    public GameObject HUD;
    public GameObject SaveManager;
    public GameObject LosePanel;
    public GameObject Camera;
    public float loseDistance = 30f;
    int currentScore;
    int currentCoins;
    int loadedScore;
    SaveManager saveManager;
    HUD hud;
   

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
      //  hud = HUD.GetComponent<HUD>();
        LosePanel.SetActive(false);
        saveManager = SaveManager.GetComponent<SaveManager>();
        currentScore = 0;
        currentCoins = saveManager.getCoins();;
        loadedScore = saveManager.getScores();
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
            saveManager.setScores(currentScore + loadedScore);
        }
        if (Ball.transform.position.y < Camera.transform.position.y-loseDistance)
        {
            LosePanel.SetActive(true);
        }
        //hud.SetScores(currentScore + loadedScore);
       // hud.SetCoins(currentCoins);
    }
}
