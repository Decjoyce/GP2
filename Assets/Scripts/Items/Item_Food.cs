using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Item", menuName = "Item/Food Item")]
public class Item_Food : Item
{
    public int addAmount;
    public override void Use(PlayerInventory inv)
    {
        inv.stats.AddHunger(addAmount);
        base.Use(inv);
    }
}
