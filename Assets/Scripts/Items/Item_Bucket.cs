using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bucket", menuName = "Item/Bucket")]
public class Item_Bucket : Item
{
    public GameObject ogBucket;

    public override void Use(PlayerInventory inv)
    {
        if(BucketItems.itemsInBucket.Count > 0)
        {
            int lastIndex = BucketItems.itemsInBucket.Count - 1;
            Item lastItem = BucketItems.itemsInBucket[lastIndex];
            Destroy(inv.heldObject.transform.GetChild(lastIndex).gameObject);
            Destroy(ogBucket.transform.GetChild(0).GetChild(lastIndex).GetChild(0).gameObject);
            lastItem.Drop(inv.throwPos);
            BucketItems.itemsInBucket.RemoveAt(lastIndex);
        }
    }

    public override void AltUse(PlayerInventory inv)
    {
        for(int i = 0; i < BucketItems.itemsInBucket.Count; i++)
        {
            Item item = BucketItems.itemsInBucket[i];
            Destroy(inv.heldObject.transform.GetChild(i).gameObject);
            item.Drop(inv.throwPos);
        }
        BucketItems.itemsInBucket.RemoveRange(0, BucketItems.itemsInBucket.Count);
    }

    public override void Drop(Transform dropLocation)
    {
        ogBucket.transform.position = dropLocation.position;
        ogBucket.SetActive(true);
    }
}
