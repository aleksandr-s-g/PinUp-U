using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void onStartClicked()
    {
        SceneManager.LoadScene("GameJourney", LoadSceneMode.Single);
    }
    public void onJourneyClicked()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
