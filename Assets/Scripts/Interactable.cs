using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class Interactable : MonoBehaviour
{
    public LocalizedString contextText;
    public bool needItems;

    public virtual void Interaction(PlayerInteraction player)
    {
        Debug.Log(player.name + "Interacted with " + name);
    }
    
    public virtual bool CheckRequiredItems(PlayerInventory inv)
    {
        return true;
    }

    public virtual void ChangeContextText()
    {
        
    }
}
