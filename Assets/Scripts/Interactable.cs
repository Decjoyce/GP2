using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class Interactable : MonoBehaviour
{
    public LocalizedString contextText;
    public bool needItems;

    public string nextState = "NEUTRAL";
    public AnimationClip animationClip;
    public string animParam;

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
