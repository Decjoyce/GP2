using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string displayName;
    public GameObject mesh;
    public GameObject droppedItem;
    public int holdType;
    public int animIndex;

    public virtual void Use(PlayerInventory inv)
    {
        GameManager.instance.gd_statistics.itemsUsed++;
        //inv.RemoveItem();
    }
    
    public virtual void AltUse(PlayerInventory inv)
    {
        Use(inv);
        GameManager.instance.gd_statistics.itemsUsed++;
        //inv.RemoveItem();
    }

    public virtual void Drop(Transform dropLocation)
    {
        //Vector3 newPos = new Vector3(dropLocation.position.x, dropLocation.position.y + 1, dropLocation.)
        if(droppedItem != null)
            Instantiate(droppedItem, dropLocation.position, dropLocation.rotation);

        GameManager.instance.gd_statistics.itemsDropped++;
    }
}
