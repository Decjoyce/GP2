using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Item currentItem;

    public PlayerStats stats;

    public Transform throwPos;
    public Transform otherDropLocation;
    public GameObject heldObject;

    [SerializeField] LayerMask interactionLayer;

    public void AddItem(Item item, GameObject overridenGraphics = null)
    {
        if(currentItem != null)
        {
            Destroy(heldObject);
            currentItem.Drop(transform);
        }
            
        currentItem = item;
        if(overridenGraphics == null)
        {
            if(item.mesh != null)
                heldObject = Instantiate(item.mesh, throwPos);
        }

        else
            heldObject = Instantiate(overridenGraphics, throwPos);
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

            Destroy(heldObject);
            currentItem = null;
        }
    }

    public void RemoveItem()
    {
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
}
