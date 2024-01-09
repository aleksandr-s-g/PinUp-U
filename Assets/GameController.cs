using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public GameObject Ball;
    public GameObject HUD;
    int currentScore;
    int currentCoins;

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
        hud = HUD.GetComponent<HUD>();
        currentScore = 0;
        currentCoins = 0;
    }
    public void CoinCollected()
    {
        Debug.Log("Coin was collected!");
        currentCoins++;
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Ball.transform.position.y > currentScore)currentScore = (int)Ball.transform.position.y;
        hud.SetScores(currentScore);
        hud.SetCoins(currentCoins);
    }
}
