using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGeneratorScript : MonoBehaviour
{
    public GameObject blueBlockPrefub;
    public LevelDescripton curLvl;

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
        FileInfo targetLevel = filesList[0];
        string targetLevelFullFileName = targetLevel.Directory+ "/"+ targetLevel.Name;
        //Debug.Log(targetLevel.Directory+ "/"+ targetLevel.Name);
        string jsonString = File.ReadAllText(targetLevelFullFileName);
        Debug.Log(jsonString);
        curLvl = JsonUtility.FromJson<LevelDescripton>(jsonString);
        foreach (BlockDescripton b in curLvl.blocks)
        {
            Instantiate(blueBlockPrefub, new Vector3(b.x+0.5f, b.y+0.5f, 0), Quaternion.identity);
            Debug.Log(b.colour);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
