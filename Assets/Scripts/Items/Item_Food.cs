using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Item", menuName = "Item/Food Item")]
public class Item_Food : Item
{
    public int addAmount;
    public float eatDelay;

    public override void Use(PlayerInventory inv)
    {
        inv.Eat(eatDelay, addAmount);
    }

    public override void AltUse(PlayerInventory inv)
    {
        inv.Eat(eatDelay, addAmount);
    }
}
