using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sleep Item", menuName = "Item/Sleep Item")]
public class Item_Sleep : Item
{
    public override void Use(PlayerInventory inv)
    {
        GameManager.instance.PlayersSleeping(inv.GetComponent<PlayerController>().player2, false);
        base.Use(inv);
    }

    public override void AltUse(PlayerInventory inv)
    {
        base.Use(inv);
    }
}
