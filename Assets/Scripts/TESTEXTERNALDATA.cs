using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct GameStatus
{
    public string playerName;
    public int currentLevel;
    public string spawnPoint;
    public int health;
    public int coins;
}

public class TESTEXTERNALDATA : MonoBehaviour
{
    GameStatus gameStatus;
    string filePath;
    const string FILE_NAME = "SaveStatus.json";
    [SerializeField] TextMeshProUGUI helpMe;

    public void RandomiseGameStatus()
    {
        gameStatus.coins += (int)Mathf.Floor(UnityEngine.Random.Range(20f, 100f));

        if(gameStatus.coins > 100)
        {
            gameStatus.currentLevel++;
            gameStatus.health += 10;
            gameStatus.coins = 0;
        }
    }

    void ShowStatus()
    {
        //building the formatted string to be shown to the user
        string message = "";
        message += "Player Name: " + gameStatus.playerName + "\n";
        message += "Current Level: " + gameStatus.currentLevel + "\n";
        message += "Spawn Point: " + gameStatus.spawnPoint + "\n";
        message += "Health: " + gameStatus.health + "\n";
        message += "Coins: " + gameStatus.coins + "\n";
        helpMe.text = message;
    }

    public void LoadGameStatus()
    {
        if(File.Exists(filePath + "/" + FILE_NAME))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME);
            gameStatus = JsonUtility.FromJson<GameStatus>(loadedJson);
            Debug.Log("FILE LOADED SUCCESSFULLY");
        }
        else
        {
            gameStatus.playerName = "Keith";
            gameStatus.currentLevel = 1;
            gameStatus.spawnPoint = "Beginning";
            gameStatus.health = 100;
            gameStatus.coins = 0;
            Debug.Log("File not found");
        }
    }

    public void SaveGameStatus()
    {
        string gameStatusJson = JsonUtility.ToJson(gameStatus);

        File.WriteAllText(filePath + "/" + FILE_NAME, gameStatusJson);
        Debug.Log("File created and saved");
    }

    private void Start()
    {
        filePath = Application.persistentDataPath;
        gameStatus = new GameStatus();
        Debug.Log(filePath);
        LoadGameStatus();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SaveGameStatus();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RandomiseGameStatus();
        }
        if (Input.GetKeyDown(KeyCode.Delete))
            //ResetGameStatus();

        ShowStatus();
    }

    private void OnApplicationQuit()
    {
        SaveGameStatus();
    }
}
