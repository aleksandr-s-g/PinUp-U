using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    [System.Serializable]
    private class GameData
    {
        public int scores;
        public int coins;
        public string uuid;
    }

    private string filePath = "gameSave.json";
    private GameData gameData = new GameData();

    private void Save()
    {
        string jsonData = JsonUtility.ToJson(gameData);
        File.WriteAllText(Application.persistentDataPath+"/"+filePath, jsonData);
    }

    private void Load()
    {
        string jsonData = File.ReadAllText(Application.persistentDataPath + "/" + filePath);
        Debug.Log(jsonData);
        gameData = JsonUtility.FromJson<GameData>(jsonData);
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
        //Debug.Log(Application.persistentDataPath);
    }

    public void setScores(int s)
    {
        gameData.scores = s;
        Save();
    }

    public int getScores()
    {
        Load();
        return gameData.scores;
    }

    public void setCoins(int c)
    {
        gameData.coins = c;
        Save();
    }

    public int getCoins()
    {
        Load();
        return gameData.coins;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
