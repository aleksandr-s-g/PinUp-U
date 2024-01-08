using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGeneratorScript : MonoBehaviour
{
    public GameObject blueBlockPrefub;
    public GameObject redBlockPrefub;
    public LevelDescripton curLvl;
    int currentShift = 0;

    [System.Serializable]
    public class BlockDescripton
    {
        public string colour;
        public int x;
        public int y;
    }

    [System.Serializable]
    public class CoinDescripton
    {
        public int x;
        public int y;
    }

    [System.Serializable]
    public class LevelDescripton
    {
        public BlockDescripton[] blocks;
        public CoinDescripton[] coins;
        //public int in; //не работает, судя по всему, плотому что in и out служебные слова, но это не точно 
        //public int out;
    }

    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/Levels/Journey");
        FileInfo[] filesList = dir.GetFiles("*.json");
        //FileInfo targetLevel = filesList[0];
        for(int i = 0; i<10;i++)
        {
            GameObject newBlock = Instantiate(blueBlockPrefub, new Vector3(0.5f+i, -0.5f, 0), Quaternion.identity);
            newBlock.transform.SetParent(transform);
        }
        foreach(FileInfo targetLevel in filesList)
        {
            for(int i = 0; i<20;i++)
            {
                GameObject newBlock = Instantiate(blueBlockPrefub, new Vector3(-0.5f, 0.5f+i+currentShift, 0), Quaternion.identity);
                newBlock.transform.SetParent(transform);
            }
            for(int i = 0; i<20;i++)
            {
                GameObject newBlock = Instantiate(blueBlockPrefub, new Vector3(10.5f, 0.5f+i+currentShift, 0), Quaternion.identity);
                newBlock.transform.SetParent(transform);
            }
            string targetLevelFullFileName = targetLevel.Directory+ "/"+ targetLevel.Name;
            //Debug.Log(targetLevel.Directory+ "/"+ targetLevel.Name);
            string jsonString = File.ReadAllText(targetLevelFullFileName);
            Debug.Log(jsonString);
            curLvl = JsonUtility.FromJson<LevelDescripton>(jsonString);
            foreach (BlockDescripton b in curLvl.blocks)
            {
                if (b.colour == "blue"){
                    GameObject newBlock = Instantiate(blueBlockPrefub, new Vector3(b.x+0.5f, b.y+0.5f+currentShift, 0), Quaternion.identity);
                    newBlock.transform.SetParent(transform);
                }
                if (b.colour == "red"){
                    GameObject newBlock = Instantiate(redBlockPrefub, new Vector3(b.x+0.5f, b.y+0.5f+currentShift, 0), Quaternion.identity);
                    newBlock.transform.SetParent(transform);
                }

                //Debug.Log(b.colour);
            }
            currentShift+=20;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
