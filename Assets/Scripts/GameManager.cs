using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
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
    }
    #endregion singleton

    public UnityEvent OnGameStart, OnGameOver;

    public bool gameIsOver;

    [SerializeField] GameObject player1, player2;
    public RoomHandler rm;
    public readonly GameObject player1Ref, player2Ref;

    byte playersDead;

    bool player1Sleeping, player2Sleeping;
    bool sleeping;

    [SerializeField] CampFire campFire;

    public void PlayersSleeping(bool player2, bool sleep)
    {
        if (player2)
            player2Sleeping = sleep;
        else
            player2Sleeping = sleep;

        Debug.Log("Player is Sleeping");

        if (player1Sleeping && player2Sleeping)
        {
            Debug.Log("Both players are sleeping");
            Time.timeScale = 10f;
            sleeping = true;
        }
    }

    private void Update()
    {
        if(sleeping && Input.GetKeyDown(KeyCode.Space))
        {
            sleeping = false;
            player1Sleeping = false;
            player2Sleeping = false;
            Time.timeScale = 1f;
        }
    }

    public void PlayerHasDied(bool isPlayer2)
    {
        if (isPlayer2 && playersDead < 1)
        {
            player1.GetComponent<PlayerStats>().lastAlive = true;
            rm.SetUpDeath(true);
        }
        else if(playersDead < 1)
        {
            player2.GetComponent<PlayerStats>().lastAlive = true;
            rm.SetUpDeath(false);
        }
        playersDead++;
        if (playersDead == 2)
        {
            GameOver();
        }

    }

    void GameOver()
    {
        gameIsOver = true;
        OnGameOver.Invoke();
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

}
