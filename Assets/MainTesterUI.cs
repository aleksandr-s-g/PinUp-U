using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainTesterUI : MonoBehaviour
{
    public GameObject TesterPanel;
    public GameObject TestModePanel;
    
    public GameObject Minimize;
    public GameObject MainController;
    public GameObject SaveManager;
    SaveManager saveManager;
    MainController mainController;
    public float testerClickInterval = 0.3f;
    public int testerClickCountTarget = 10;
    private int testerClickCount = 0;
    private float testerLastClickTime = 0f;

    public TextMeshProUGUI testHeadText;
    public TextMeshProUGUI testBodyText;
    bool isTesterModeOn = false;


    float fpsTimer = 0f;
    float fpsSecondPerUpdate = 1f;


    public void onTesterButtonClicked()
    {
        float currentTime = Time.time;
        Debug.Log("onTesterButtonClicked");
        if (currentTime - testerLastClickTime < testerClickInterval)
        {
            testerClickCount++;
            if (testerClickCount >= testerClickCountTarget)
            {
                 // Debug.Log("is tester");
                TesterPanel.SetActive(!TesterPanel.activeSelf);
                TestModePanel.SetActive(!TesterPanel.activeSelf);
                testBodyText.text = saveManager.getRawSaveText() + "\n" + mainController.getStringDeviceInfo();
                saveManager.setTester(true);
                isTesterModeOn = TestModePanel.activeSelf;
                mainController.SetTesterModeIsOn(isTesterModeOn);
                testerClickCount = 0;
                if (TesterPanel.activeSelf)
                {
                    mainController.EmitAnalyticsEvent("tester_panel", "on", "", "");

                }
                else
                {
                    mainController.EmitAnalyticsEvent("tester_panel", "off", "", "");

                }

            }
            else
            {
                // Debug.Log(testerClickCount);
            }
        }
        else
        {

            testerClickCount = 1;
            //Debug.Log(testerClickCount);
        }


        testerLastClickTime = currentTime;
    }

    public void onSetCoins1000Clicked()
    {
        saveManager.setCoins(1000);
       // mainController.EmitAnalyticsEvent("tester_set_coins", gameMode.ToString(), "1000", "");
        Debug.Log("coins 1000");
    }
    public void onSetCoins0Clicked()
    {
        saveManager.setCoins(0);
       // mainController.EmitAnalyticsEvent("tester_set_coins", gameMode.ToString(), "0", "");
        //Debug.Log("coins 0");
    }
    public void onApplovinClicked()
    {
        mainController.onApplovinClicked();
    }


    public void onTesterCloseClicked()
    {
        TestModePanel.SetActive(false);
        isTesterModeOn = TestModePanel.activeSelf;
        mainController.SetTesterModeIsOn(isTesterModeOn);
    }
    public void onTesterMinimizeClicked()
    {

        TesterPanel.SetActive(!TesterPanel.activeSelf);

        Minimize.transform.Rotate(0, 0, 180);

    }

    // Start is called before the first frame update
    void Start()
    {
        saveManager = SaveManager.GetComponent<SaveManager>();

        mainController = MainController.GetComponent<MainController>();
        isTesterModeOn = mainController.GetTesterModeIsOn();
        
        TestModePanel.SetActive(isTesterModeOn);
        

    }

    // Update is called once per frame
    void Update()
    {
        if (isTesterModeOn)
        {
            fpsTimer = fpsTimer + Time.deltaTime;
            if (fpsTimer > fpsSecondPerUpdate)
            {
                fpsTimer = 0;
                testHeadText.text = "TEST MODE\r\nFPS: " + mainController.GetBaseDeviceInfo().fps.ToString();
                /* testBodyText.text = "UUID: " + mainController.GetBaseUserInfo().uuid.ToString() +
                     "\r\nIP: " + mainController.GetBaseDeviceInfo().global_ip.ToString() +
                     "\r\nDeviceInfo:" + mainController.GetBaseDeviceInfo().ToString();  */
            }

        }
    }
}
