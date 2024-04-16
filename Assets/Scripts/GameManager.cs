using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static GameManager;

#region game data

[System.Serializable]
public struct GameData_Gameplay
{
    public bool beenInit;

    public bool dead_p1;
    public bool dead_p2;

    public float hunger_p1;
    public float hunger_p2;

    public Vector3 pos_p1;
    public Vector3 pos_p2;

    public string room_p1;
    public string room_p2;

    public Vector3 pos_Monster;

    public void ResetData()
    {
        dead_p1 = false;
        dead_p2 = false;

        hunger_p1 = 100;
        hunger_p2 = 100;

        pos_p1 = Vector3.right * -2;
        pos_p2 = Vector3.right * 2;

        room_p1 = "Room0";
        room_p2 = "Room0";

        pos_Monster = Vector3.forward * 100000f;

        Debug.Log("Reset Gameplay Data");
    }
}

[System.Serializable]
public struct GameData_Statistics
{
    public bool beenInit;

    //GameStuff
    public float totalTimePlayed;
    public float longestTimeAlive;
    public float shortestTimeAlive;

    public int numDeaths_total;
    public int numDeaths_p1;
    public int numDeaths_p2;

    //Interactions
    public int itemsUsed;
    public int itemsDropped;
    public int itemsThrown;
    public int foodEaten;
    public int sticksAddedToFire;
    public int itemsPlacedInBucket;

    public void ResetData()
    {
        totalTimePlayed = 0;
        longestTimeAlive = 0;
        shortestTimeAlive = 0;

        numDeaths_total = 0;
        numDeaths_p1 = 0;
        numDeaths_p2 = 0;

        //Interactions
        itemsUsed = 0;
        itemsDropped = 0;
        itemsThrown = 0;
        foodEaten = 0;
        sticksAddedToFire = 0;
        itemsPlacedInBucket = 0;
    }
}

[System.Serializable]
public struct GameData_Environment
{
    public bool hasBeenInitialised;

    public List<ItemStatus> itemStatus;
    public void ResetData()
    {

    }
}

#endregion

public class GameManager : MonoBehaviour
{
    public delegate void Event_LoadGameData_Gameplay();
    public event Event_LoadGameData_Gameplay OnLoadGameData_Gameplay;

    public delegate void Event_OnLoadGameData_Statistics();
    public event Event_OnLoadGameData_Statistics OnLoadGameData_Statistics;

    public delegate void Event_OnLoadGameData_Environment();
    public event Event_OnLoadGameData_Environment OnLoadGameData_Environment;

    public delegate void Event_SaveGameData_Gameplay();
    public event Event_SaveGameData_Gameplay OnSaveGameData_Gameplay;

    public delegate void Event_OnSaveGameData_Statistics();
    public event Event_OnSaveGameData_Statistics OnSaveGameData_Statistics;

    public delegate void Event_OnSaveGameData_Environment();
    public event Event_OnSaveGameData_Environment OnSaveGameData_Environment;

    #region singleton
    public static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found");
            return;
        }
        instance = this;

        rm = GetComponent<RoomHandler>();

        //GameData
        filePath = Application.persistentDataPath;
        gd_gameplay = new GameData_Gameplay();
        gd_statistics = new GameData_Statistics();
        gd_environment = new GameData_Environment();
        Debug.Log(filePath);
    }
    #endregion singleton

    public GameData_Gameplay gd_gameplay;
    public GameData_Statistics gd_statistics;
    public GameData_Environment gd_environment;

    string filePath;
    const string FILE_NAME_GAMEPLAY = "gameplay_save.json", FILE_NAME_STATISTICS = "statistics_save.json", FILE_NAME_ENVIRONMENT = "environment_save.json";

    #region Game Data Functionality
    public void LoadGameData_Gameplay()
    {
        if (File.Exists(filePath + "/" + FILE_NAME_GAMEPLAY))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME_GAMEPLAY);
            gd_gameplay = JsonUtility.FromJson<GameData_Gameplay>(loadedJson);
            Debug.Log("<b>GAMEPLAY DATA</b> LOADED SUCCESSFULLY");
        }
        else
        {
            ResetGameData_Gameplay();
            Debug.Log("<b>GAMEPLAY DATA</b> not found");
        }
        OnLoadGameData_Gameplay.Invoke();
    }

    public void SaveGameData_Gameplay()
    {
        if (canSave_gameplayData)
        {
            OnSaveGameData_Gameplay.Invoke();

            gd_gameplay.beenInit = true;

            string gameStatusJson = JsonUtility.ToJson(gd_gameplay);

            File.WriteAllText(filePath + "/" + FILE_NAME_GAMEPLAY, gameStatusJson);
            Debug.Log("<b>GAMEPLAY DATA</b> created and saved");
        }

    }

    public void ResetGameData_Gameplay()
    {
        if (File.Exists(filePath + "/" + FILE_NAME_GAMEPLAY))
        {
            File.Delete(filePath + "/" + FILE_NAME_GAMEPLAY);
        }
    }

    public void LoadGameData_Statistics()
    {
        if (File.Exists(filePath + "/" + FILE_NAME_STATISTICS))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME_STATISTICS);
            gd_statistics = JsonUtility.FromJson<GameData_Statistics>(loadedJson);
            Debug.Log("GAMEPLAY STATISTICS LOADED SUCCESSFULLY");
        }
        else
        {
            ResetGameData_Statistics();
            Debug.Log("GAMEPLAY STATISTICS not found");
        }
        //OnLoadGameData_Statistics.Invoke();
    }

    public void SaveGameData_Statistics()
    {
        //OnSaveGameData_Statistics.Invoke();

        gd_statistics.beenInit = true;

        gd_statistics.totalTimePlayed += Time.realtimeSinceStartup;

        string gameStatusJson = JsonUtility.ToJson(gd_statistics);

        File.WriteAllText(filePath + "/" + FILE_NAME_STATISTICS, gameStatusJson);
        Debug.Log("GAMEPLAY STATISTICS created and saved");
    }

    public void ResetGameData_Statistics()
    {
        if (File.Exists(filePath + "/" + FILE_NAME_STATISTICS))
        {
            File.Delete(filePath + "/" + FILE_NAME_STATISTICS);
        }
    }

    public void LoadGameData_Environment()
    {
        if (File.Exists(filePath + "/" + FILE_NAME_ENVIRONMENT))
        {
            string loadedJson = File.ReadAllText(filePath + "/" + FILE_NAME_ENVIRONMENT);
            gd_environment = JsonUtility.FromJson<GameData_Environment>(loadedJson);
            Debug.Log("GAMEPLAY ENVIRONMENT LOADED SUCCESSFULLY");
        }
        else
        {
            ResetGameData_Environment();
            Debug.Log("<b>GAMEPLAY ENVIRONMENT</b> not found");
        }
        OnLoadGameData_Environment.Invoke();
    }

    public void SaveGameData_Environment()
    {
        OnSaveGameData_Environment.Invoke();

        string gameStatusJson = JsonUtility.ToJson(gd_environment);

        File.WriteAllText(filePath + "/" + FILE_NAME_ENVIRONMENT, gameStatusJson);
        Debug.Log("GAMEPLAY ENVIRONMENT created and saved");
    }

    public void ResetGameData_Environment()
    {
        if (File.Exists(filePath + "/" + FILE_NAME_ENVIRONMENT))
        {
            File.Delete(filePath + "/" + FILE_NAME_ENVIRONMENT);
        }
    }

    public void LoadAllData()
    {
        LoadGameData_Gameplay();
        LoadGameData_Statistics();
        LoadGameData_Environment();
    }

    public void SaveAllData()
    {
        SaveGameData_Gameplay();
        SaveGameData_Statistics();
        SaveGameData_Environment();
    }

    public void ResetAllData()
    {
        ResetGameData_Gameplay();
        ResetGameData_Statistics();
        ResetGameData_Environment();
    }

    #endregion

    public UnityEvent OnGameStart, OnGameOver;

    public bool gameIsOver;

    public GameObject player1, player2;
    public RoomHandler rm;
    public readonly GameObject player1Ref, player2Ref;

    public SleepManager sleepManager;

    public bool allowSounds;

    float currentPlayTime;

    public bool cleanRun;

    bool canSave_gameplayData = true;

    private void Start()
    {
        if(cleanRun)
            ResetAllData();

        LoadAllData();
        Invoke(nameof(AllowSounds), 3f);
    }

    void AllowSounds()
    {
        allowSounds = true;
    }

    void StartGame()
    {

    }

    public void GameOver()
    {
        //CheckTimeAlive();
        gameIsOver = true;
        OnGameOver.Invoke();
        canSave_gameplayData = false;
    }

    public GameObject GetOtherPlayer(GameObject player)
    {
        if (player == player1)
            return player2;
        else if (player == player2)
            return player1;
        else
        {
            Debug.Log("NOT VALID PLAYER");
            return null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            SaveAllData();
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            LoadAllData();
        }
    }

    public void CheckTimeAlive(float newTime)
    {
        if (newTime > gd_statistics.longestTimeAlive)
            gd_statistics.longestTimeAlive = newTime;

        if (newTime < gd_statistics.shortestTimeAlive)
            gd_statistics.shortestTimeAlive = newTime;
    }

    private void OnApplicationQuit()
    {
        SaveAllData();
    }

}
