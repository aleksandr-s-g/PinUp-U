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
        public int bestrace;
        public int coins;
        public string uuid;
        public string gameMode;
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
        gameData = JsonUtility.FromJson<GameData>(jsonData);
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
        
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
    public void setBestRace(int br)
    {
        gameData.bestrace = br;
        Save();
    }

    public int getBestRace()
    {
        Load();
        return gameData.bestrace;
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
    public void setGameMode (string gm)
    {
        gameData.gameMode = gm;
        Save();
    }

    public string getGameMode()
    {
        Load();
        return gameData.gameMode;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
