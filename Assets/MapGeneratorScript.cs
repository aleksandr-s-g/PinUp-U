using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;

public class MapGeneratorScript : MonoBehaviour
{
    public GameObject blueBlockPrefub;
    public GameObject redBlockPrefub;
    public GameObject coinPrefub;
    //public LevelDescripton curLvl;
    int currentShift = 0;
    string[] levelQueue;
    //List<FileInfo> levelQueue = new List<FileInfo>();
    public GameObject Ball;

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

    void Shuffle(string[] a)
    {
    // Loops through array
        for (int i = a.Length-1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = Random.Range(0,i);
            // Save the value of the current i, otherwise it'll overright when we swap the values
            string temp = a[i];
            // Swap the new and old values
            a[i] = a[rnd];
            a[rnd] = temp;
        }
    }

    string popLevel()
    {
        string[] levelQueueNew = new string[levelQueue.Length-1];
        //FileInfo[levelQueue.Length-1] levelQueueNew;
        string firstElement = levelQueue[0];
        for(int i = 1;i<levelQueue.Length;i++)
        {
            levelQueueNew[i-1] = levelQueue[i];
        }
        levelQueue = levelQueueNew;
        return firstElement;
    }
    
    void fillQueue()
    {   
        TextAsset asset = Resources.Load<TextAsset>("FileNames");

        string[] line_list = asset.text.Split("\n");
        

        int[] target_level_list_range = new int[2];
        target_level_list_range[0] = -1; //from
        target_level_list_range[1] = -1; //to
        for (int i = 0 ; i<line_list.Length;i++)
        {
            if (line_list[i].IndexOf("[race_levels_list]", 0) == 0)
            {
                target_level_list_range[0] = i+1;
            }
            if ( string.IsNullOrWhiteSpace(line_list[i]) && target_level_list_range[0] != -1 && target_level_list_range[1] == -1)
            {
                target_level_list_range[1] = i-1;
            }
        }
        string[] level_list = new string[target_level_list_range[1]-target_level_list_range[0]+1];
       
        for (int i = 0; i<level_list.Length;i++)
        {
            level_list[i] = line_list[i+target_level_list_range[0]];
        }
        levelQueue = level_list;
        Shuffle(levelQueue);
    }
    // Start is called before the first frame update
    void createLevel(string targetLevel)
    {
        LevelDescripton curLvl;
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
       
            string jsonString = Resources.Load<TextAsset>(targetLevel).text;
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
            }
            foreach (CoinDescripton c in curLvl.coins)
            {
                
                GameObject newCoin = Instantiate(coinPrefub, new Vector3(c.x+0.5f, c.y+0.5f+currentShift, 0), Quaternion.identity);
                newCoin.transform.SetParent(transform);
            }
            currentShift+=20;
    }
    void Start()
    {
        levelQueue = new string[0];
        for(int i = 0; i<10;i++)
        {
            GameObject newBlock = Instantiate(blueBlockPrefub, new Vector3(0.5f+i, -0.5f, 0), Quaternion.identity);
            newBlock.transform.SetParent(transform);
        } 
        fillQueue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Ball.transform.position.y > currentShift-40)
        {
            if(levelQueue.Length == 0) fillQueue();
            createLevel(popLevel());
        }
    }
}

