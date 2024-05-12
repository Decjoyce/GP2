using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Xml;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
        Debug.Log(filePath);
    }
    #endregion singleton

    public GameData_Gameplay gd_gameplay;
    public GameData_Statistics gd_statistics;

    string filePath;
    const string FILE_NAME_GAMEPLAY = "gameplay_save.json", FILE_NAME_STATISTICS = "statistics_save.json";

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
        if(!startMenu)
        OnLoadGameData_Gameplay.Invoke();
    }

    public void SaveGameData_Gameplay()
    {
        if (canSave_gameplayData)
        {
            if (!startMenu)
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
            if (!startMenu)
            {
                int ranPos_p1 = Random.Range(0, spawnpoints.Length);
                int ranPos_p2 = Random.Range(0, spawnpoints.Length);
                while(ranPos_p2 == ranPos_p1)
                {
                    ranPos_p2 = Random.Range(0, spawnpoints.Length);
                }
                Debug.Log(ranPos_p1 + " " + ranPos_p2);
                player1.GetComponent<Rigidbody>().MovePosition(spawnpoints[ranPos_p1].position);
                player2.GetComponent<Rigidbody>().MovePosition(spawnpoints[ranPos_p2].position);
            }

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

    public void LoadAllData()
    {
        LoadGameData_Gameplay();
        LoadGameData_Statistics();
    }

    public void SaveAllData()
    {
        SaveGameData_Gameplay();
        SaveGameData_Statistics();
    }

    public void ResetAllData()
    {
        ResetGameData_Gameplay();
        ResetGameData_Statistics();
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

    [SerializeField] Transform[] spawnpoints;

    [SerializeField] bool startMenu;

    [SerializeField] GameObject pauseMenu;
    bool gamePaused;

    [SerializeField] EventSystem eventSystem;

    [SerializeField] GameObject overButton;
    private void Start()
    {
        if(cleanRun)
            ResetAllData();

        LoadAllData();
        Invoke(nameof(AllowSounds), 3f);
        Time.timeScale = 1f;
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
        eventSystem.SetSelectedGameObject(overButton);
        CheckTimeAlive();
        gameIsOver = true;
        OnGameOver.Invoke();
        canSave_gameplayData = false;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveGame()
    {
        Application.Quit();
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
        currentPlayTime += Time.deltaTime;

        if (!startMenu && !gameIsOver && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    float lastTimeScale;

    void PauseGame()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            lastTimeScale = Time.timeScale;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = lastTimeScale;
            pauseMenu.SetActive(false);
        }
    }

    public void CheckTimeAlive()
    {
        if (currentPlayTime > gd_statistics.longestTimeAlive)
            gd_statistics.longestTimeAlive = currentPlayTime;

        if (currentPlayTime < gd_statistics.shortestTimeAlive || currentPlayTime > 0)
            gd_statistics.shortestTimeAlive = currentPlayTime;
    }

    public void LoadStatsIntoText(TextMeshProUGUI text)
    {
        string message = "";
        message += "Total Time Played: " + gd_statistics.totalTimePlayed + "\n";
        message += "Longest Time Alive: " + gd_statistics.longestTimeAlive + "\n";
        message += "Shortest Time Alive: " + gd_statistics.shortestTimeAlive + "\n";
        message += "Total Deaths:" + gd_statistics.numDeaths_total + "\n";
        message += "Player 1 Deaths:" + gd_statistics.numDeaths_p1 + "\n";
        message += "Player 2 Deaths:" + gd_statistics.numDeaths_p2 + "\n";
        message += "Items Used: " + gd_statistics.itemsUsed + "\n";
        message += "Items Dropped: " + gd_statistics.itemsDropped + "\n";
        message += "Items Thrown: " + gd_statistics.itemsThrown + "\n";
        message += "Food Eaten: " + gd_statistics.foodEaten + "\n";
        text.text = message;
    }

}
