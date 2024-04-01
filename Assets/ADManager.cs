using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) => {
            Debug.Log("AppLovin SDK is initialized, start loading ads");
            MaxSdk.ShowMediationDebugger();
    // AppLovin SDK is initialized, start loading ads
        }       ;

        //MaxSdk.SetSdkKey("rKJKJNGINemT8rUU6mCedTQVI9FEqSvqsOusK0i28brptIb5szmTrn4GcZEOyCcOzWhI8I7SQrWdnF1GdxeCUw");
        //MaxSdk.SetSdkKey("sdgndnetyjehtyjhetyjetyjnetyjnetyjhetyj");
        //MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
