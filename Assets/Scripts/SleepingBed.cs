using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingBed : Interactable
{
    bool canSleep = true;
    bool occupied;
    [SerializeField] CampFire campfire;
    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        if (occupied)
        {
            GameManager.instance.PlayersSleeping(false);
            occupied = false;
        }
        else if(!occupied && canSleep)
        {
            GameManager.instance.PlayersSleeping(true);
            occupied = true;
        }
    }
}
