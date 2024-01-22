using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class Bucket : Interactable
{
    [SerializeField] LocalizedString contexttext_pickup;
    [SerializeField] LocalizedString contexttext_add;
    [SerializeField] LocalizedString contexttext_bucketfull;

    [SerializeField] Item_Bucket bucketItem;
    [SerializeField] Transform[] itemSpots;

    public int bucketSize = 3;

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        if (player.inventory.currentItem != null)
        {
            if (BucketItems.itemsInBucket.Count < bucketSize)
            {
                BucketItems.itemsInBucket.Add(player.inventory.currentItem);
                Instantiate(player.inventory.currentItem.mesh, itemSpots[BucketItems.itemsInBucket.Count - 1]);
                player.inventory.RemoveItem();
            }
        }
        else
        {
            player.inventory.AddItem(bucketItem, transform.GetChild(0).gameObject);
            player.RemoveInteraction(this);
            bucketItem.ogBucket = gameObject;
            gameObject.SetActive(false);
        }

    }

    public override bool CheckRequiredItems(PlayerInventory inv)
    {
        if (inv.currentItem != null)
        {
            if (BucketItems.itemsInBucket.Count < bucketSize)
                contextText = contexttext_add;
            else
                contextText = contexttext_bucketfull;
        }
        else
            contextText = contexttext_pickup;
        return true;
    }

}
