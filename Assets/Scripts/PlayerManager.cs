using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerController2 player1, player2;
    public bool p1_dead, p2_dead;

    #region singleton
    public static PlayerManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerManager found");
            return;
        }
        instance = this;
    }
    #endregion singleton

    private void OnEnable()
    {
        GameManager.instance.OnLoadGameData_Gameplay += LoadPlayers;
        GameManager.instance.OnSaveGameData_Gameplay += SavePlayers;
    }

    private void OnDisable()
    {
        GameManager.instance.OnLoadGameData_Gameplay -= LoadPlayers;
        GameManager.instance.OnSaveGameData_Gameplay -= SavePlayers;
    }

    public void LoadPlayers()
    {
        if (GameManager.instance.gd_gameplay.beenInit)
        {
            p1_dead = GameManager.instance.gd_gameplay.dead_p1;
            player1.transform.position = GameManager.instance.gd_gameplay.pos_p1;
            player1.stats.currentHunger = GameManager.instance.gd_gameplay.hunger_p1;
            RoomHandler.instance.SetUpRooms(GameManager.instance.gd_gameplay.room_p1, false);

            p2_dead = GameManager.instance.gd_gameplay.dead_p2;
            player2.transform.position = GameManager.instance.gd_gameplay.pos_p2;
            player2.stats.currentHunger = GameManager.instance.gd_gameplay.hunger_p2;
            RoomHandler.instance.SetUpRooms(GameManager.instance.gd_gameplay.room_p2, true);

            if (p2_dead)
                player2.stats.Die();

            if (p1_dead)
                player1.stats.Die();
        }
    }

    public void SavePlayers()
    {
        GameManager.instance.gd_gameplay.dead_p1 = p1_dead;
        GameManager.instance.gd_gameplay.hunger_p1 = player1.stats.currentHunger;
        GameManager.instance.gd_gameplay.pos_p1 = player1.transform.position;
        GameManager.instance.gd_gameplay.room_p1 = RoomHandler.instance.roomPlayer1;

        GameManager.instance.gd_gameplay.dead_p2 = p2_dead;
        GameManager.instance.gd_gameplay.hunger_p2 = player2.stats.currentHunger;
        GameManager.instance.gd_gameplay.pos_p2 = player2.transform.position;
        GameManager.instance.gd_gameplay.room_p2 = RoomHandler.instance.roomPlayer2;
    }

    public void PlayerHasDied(bool isPlayer2)
    {
        
        if (isPlayer2)
        {
            p2_dead = true;
        }
        else
        {
            p1_dead = true;
        }

        if (p1_dead && p2_dead)
        {
            GameManager.instance.GameOver();
        }
        else
            RoomHandler.instance.SetUpDeath(isPlayer2);

    }
}
