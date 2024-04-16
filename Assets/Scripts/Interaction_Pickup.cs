using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemStatus
{
    public bool hasBeenAltered;
    public Vector3 pos;
    public Quaternion rot;
    public Item item;
    //public float id;
}

public class Interaction_Pickup : Interactable
{
    public ItemStatus itemStatus;
    public Item item;
    public bool unlimited;
    public int amount = 1;
    public string displayName;
    public float id;

    private void Start()
    {

            itemStatus.item = item;
            itemStatus.pos = transform.position;
            itemStatus.rot = transform.rotation;
            itemStatus.hasBeenAltered = true;
        if(id == 0)
            id = Random.Range(1f, 1000000f);
        displayName = item.displayName;
        ItemManager.instance.AddItemToList(this);
    }

    private void Update()
    {
        itemStatus.pos = transform.position;
        itemStatus.rot = transform.rotation;
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
                ItemManager.instance.RemoveItemFromList(this);
                Destroy(gameObject);
            }
        }
    }
}
