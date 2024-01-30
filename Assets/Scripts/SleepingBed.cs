using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingBed : Interactable
{
    bool canSleep = true;
    bool occupied;
    [SerializeField] Item sleepItem;
    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        if(!occupied && canSleep)
        {
            GameManager.instance.PlayersSleeping(player.GetComponentInParent<PlayerController>().player2, true);
            player.inventory.AddItem(sleepItem);
        }

    }
}
