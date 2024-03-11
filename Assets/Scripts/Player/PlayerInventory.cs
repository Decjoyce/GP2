using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Item currentItem;

    public PlayerStats stats;
    public PlayerController2 controller;

    public Transform[] holdPos;
    public Transform throwPos;
    public Transform otherDropLocation;
    public GameObject heldObject;

    bool isUsing;

    [SerializeField] LayerMask interactionLayer;

    private void Start()
    {
        controller = GetComponent<PlayerController2>();
    }

    public void AddItem(Item item, GameObject overridenGraphics = null)
    {
        if(currentItem != null)
        {
            Destroy(heldObject);
            currentItem.Drop(transform);
        }

        currentItem = item;

        controller.anim.SetLayerWeight(1, 0);
        controller.anim.SetLayerWeight(2, 0);
        controller.anim.SetLayerWeight(currentItem.animIndex, 1);

        if (overridenGraphics == null)
        {
            if(item.mesh != null)
                heldObject = Instantiate(item.mesh, holdPos[item.holdType]);
        }

        else
            heldObject = Instantiate(overridenGraphics, holdPos[item.holdType]);
    }

    public void DropItem()
    {
        if (currentItem != null)
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position + (Vector3.up * 1.25f), transform.forward, out hit, 2, interactionLayer))
                currentItem.Drop(throwPos);
            else
            {
                Debug.Log(hit.transform.name);
                currentItem.Drop(otherDropLocation);
            }
            controller.anim.SetLayerWeight(currentItem.animIndex, 0);
            Destroy(heldObject);
            currentItem = null;
        }
    }

    public void RemoveItem()
    {
        controller.anim.SetLayerWeight(currentItem.animIndex, 0);
        Destroy(heldObject);
        currentItem = null;
    }

    public void CheckUse(bool altUse)
    {
        if (currentItem != null)
        {
            if(altUse)
                currentItem.AltUse(this);
            else
                currentItem.Use(this);
        }
    }

    public void Throw(float delay, GameObject item2Throw, float throwForce, float throwForceUpwards, float throwTorque)
    {
        if (!isUsing)
        {
            StartCoroutine(ThrowDelay(delay, item2Throw, throwForce, throwForceUpwards, throwTorque));
        }

    }

    IEnumerator ThrowDelay(float delay, GameObject item2Throw, float throwForce, float throwForceUpwards, float throwTorque)
    {
        isUsing = true;
        controller.anim.SetBool("isThrowing", true);
        yield return new WaitForSeconds(0.1f);
        GameObject thrownItem = Instantiate(item2Throw, throwPos.position, throwPos.rotation);
        thrownItem.GetComponent<Rigidbody>().AddForce(thrownItem.transform.forward * throwForce + thrownItem.transform.up * throwForceUpwards, ForceMode.Impulse);
        thrownItem.GetComponent<Rigidbody>().AddTorque(thrownItem.transform.right * throwTorque, ForceMode.Impulse);
        Destroy(heldObject);
        yield return new WaitForSeconds(0.3f);
        controller.anim.SetLayerWeight(currentItem.animIndex, 0);
        currentItem = null;
        controller.anim.SetBool("isThrowing", false);
        isUsing = false;
    }

    public void Eat(float delay, int amount)
    {
        if(!isUsing)
            StartCoroutine(EatDelay(delay, amount));
    }

    IEnumerator EatDelay(float delay, int amount)
    {
        isUsing = true;
        controller.anim.SetBool("isEating", true);
        yield return new WaitForSeconds(delay);
        stats.AddHunger(amount);
        controller.anim.SetBool("isEating", false);
        RemoveItem();
        isUsing = false;
    }
}
