using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Ball;
    public GameObject HUD;
    int currentScore;

    HUD hud;

    // Start is called before the first frame update
    void Start()
    {
        hud = HUD.GetComponent<HUD>();
        currentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Ball.transform.position.y > currentScore)currentScore = (int)Ball.transform.position.y;
        hud.SetScores(currentScore);
    }
}
