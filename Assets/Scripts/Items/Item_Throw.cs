using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Throw Item", menuName = "Item/Throw Item")]
public class Item_Throw : Item
{
    public float throwForce = 20;
    public float throwForceUpwards = 20;
    public float throwForceMultiplier = 2;
    public float throwTorque = 0.5f;
    public float throwTorqueMultiplier = 2;
    public override void Use(PlayerInventory inv)
    {
        GameObject thrownItem = Instantiate(droppedItem, inv.throwPos.position, inv.throwPos.rotation);
        thrownItem.GetComponent<Rigidbody>().AddForce(thrownItem.transform.forward * throwForce + thrownItem.transform.up * throwForceUpwards, ForceMode.Impulse);
        thrownItem.GetComponent<Rigidbody>().AddTorque(thrownItem.transform.right * throwTorque, ForceMode.Impulse);
        base.Use(inv);
    }

    public override void AltUse(PlayerInventory inv)
    {
        GameObject thrownItem = Instantiate(droppedItem, inv.throwPos.position, inv.throwPos.rotation);
        thrownItem.GetComponent<Rigidbody>().AddForce((thrownItem.transform.forward * throwForce * throwForceMultiplier) + thrownItem.transform.up * throwForceUpwards, ForceMode.Impulse);
        thrownItem.GetComponent<Rigidbody>().AddTorque(thrownItem.transform.right * throwTorque * throwTorqueMultiplier, ForceMode.Force);
        base.Use(inv);
    }
}
