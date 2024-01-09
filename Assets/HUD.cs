using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI scoresLabel;
    // Start is called before the first frame update
    void Start()
    {
        scoresLabel.text = "777";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScores (int scores)
    {
        scoresLabel.text = scores.ToString();
    }
}