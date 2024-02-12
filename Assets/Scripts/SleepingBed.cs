using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingBed : Interactable
{
    bool canSleep = true;
    public bool occupied;
    [SerializeField] Item sleepItem;
    [SerializeField] CampFire campFire;
    [SerializeField] bool debug;
    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        if(!occupied)
        {
            occupied = true;
            player.controller.EnterState("SLEEP");
            GameManager.instance.sleepManager.AssignBed(player.controller.player2, this);
        }
    }
}
