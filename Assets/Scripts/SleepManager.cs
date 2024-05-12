using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SleepManager : MonoBehaviour
{
    public UnityEvent OnPlayer1Sleep, OnPlayer2Sleep, OnPlayersAwake, OnPlayer2Awake;
    bool sleeping_p1, sleeping_p2;

    SleepingBed bed_p1, bed_p2;
    [SerializeField] PlayerStats p1, p2;

    [SerializeField] GameObject water;

    void CheckSleep()
    {
        if((sleeping_p1 || p1.isDead) && (sleeping_p2 || p2.isDead))
        {
            Time.timeScale = 10f;
            water.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            water.SetActive(true);
        }
    }

    public void PlayerSleep(bool player2)
    {
        if (!player2)
        {
            sleeping_p1 = true;
        }
        else
        {
            sleeping_p2 = true;
        }
        CheckSleep();
    }

    public void PlayerAwake(bool player2)
    {
        if (!player2)
        {
            sleeping_p1 = false;
            bed_p1.occupied = false;
            bed_p1 = null;
        }
        else
        {
            sleeping_p2 = false;
            bed_p2.occupied = false;
            bed_p2 = null;
        }
        CheckSleep();
    }

    public void WakeUpPlayers()
    {
        if (sleeping_p1)
        {
            sleeping_p1 = false;
            bed_p1.occupied = false;
            bed_p1 = null;
            GameManager.instance.player1.GetComponent<PlayerController2>().ExitState();
        }
        if (sleeping_p2)
        {
            sleeping_p2 = false;
            bed_p2.occupied = false;
            bed_p2 = null;
            GameManager.instance.player2.GetComponent<PlayerController2>().ExitState();
        }
        CheckSleep();
    }

    public void AssignBed(bool player2, SleepingBed bed)
    {
        if (!player2)
            bed_p1 = bed;
        else
            bed_p2 = bed;
    }

}
