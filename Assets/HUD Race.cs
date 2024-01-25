using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDRace : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onBackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        
    }
    public void onRestartClickedRace()
    {
        SceneManager.LoadScene("GameRace", LoadSceneMode.Single);
    }
}
