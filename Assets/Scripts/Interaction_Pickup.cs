using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Pickup : Interactable
{
    public Item item;
    public bool unlimited;
    public string displayName;

    private void Start()
    {
        displayName = item.displayName;
    }

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        player.inventory.AddItem(item);
        player.RemoveInteraction(this);
        if (!unlimited)
        {
            Destroy(gameObject);
        }
    }
}
