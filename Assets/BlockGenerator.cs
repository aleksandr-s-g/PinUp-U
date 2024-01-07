using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject blueBlockPrefub;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<10;i++)
        {
            Instantiate(blueBlockPrefub, new Vector3(0.5f+i, -0.5f, 0), Quaternion.identity);
        }
        for(int i = 0; i<10;i++)
        {
            Instantiate(blueBlockPrefub, new Vector3(0.5f+i, 20.5f, 0), Quaternion.identity);
        }
        for(int i = 0; i<20;i++)
        {
            Instantiate(blueBlockPrefub, new Vector3(-0.5f, 0.5f+i, 0), Quaternion.identity);
        }
        for(int i = 0; i<20;i++)
        {
            Instantiate(blueBlockPrefub, new Vector3(10.5f, 0.5f+i, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
