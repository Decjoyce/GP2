using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Pickup : Interactable
{
    public Item item;
    public bool unlimited;
    public int amount = 1;
    public string displayName;

    private void Start()
    {
        displayName = item.displayName;
    }

    private void Update()
    {
    }

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        player.inventory.AddItem(item);
        if(!unlimited)
        {
            amount--;
            if (amount <= 0)
            {
                player.RemoveInteraction(this);
                Destroy(gameObject);
            }
        }
    }
}
