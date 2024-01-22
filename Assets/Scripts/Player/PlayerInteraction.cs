using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class PlayerInteraction : MonoBehaviour
{
    public PlayerInventory inventory;
    public PlayerInteraction otherInteraction;
    bool canInteract;

    List<Interactable> availableInteractions = new List<Interactable>();
    Interactable clostestInteractable;

    [SerializeField] TextMeshProUGUI contextTextPersonal, contextTextShared;

    private void Start()
    {
        inventory = GetComponentInParent<PlayerInventory>();
    }

    private void Update()
    {
        if (availableInteractions.Count <= 0)
            canInteract = false;
        else
            canInteract = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            availableInteractions.Add(other.GetComponent<Interactable>());
            SetInteractionText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            RemoveInteraction(other.GetComponent<Interactable>());
        }
    }

    public void RemoveInteraction(Interactable interactable)
    {
        if (availableInteractions.Contains(interactable))
            availableInteractions.Remove(interactable);
        SetInteractionText();
    }

    public Interactable FindClosestInteractable()
    {
        float closestInt = 50f;
        Interactable temp_ClostestInteractable = null;
        foreach (Interactable interaction in availableInteractions)
        {
            float dist = Vector3.Distance(transform.position, interaction.transform.position);
            //Debug.Log(interaction.name + " - " + dist);
            if (dist < closestInt)
            {
                closestInt = dist;
                temp_ClostestInteractable = interaction;
            }
        }
        return temp_ClostestInteractable;
    }

    public void CheckInteraction()
    {
        if (canInteract)
        {
            Interactable closestInteractable = FindClosestInteractable();
            if (FindClosestInteractable().needItems)
            {
                FindClosestInteractable().Interaction(this);   
            }          
            else
            {
                if (inventory.currentItem != null)
                {
                    inventory.DropItem();
                }
                FindClosestInteractable().Interaction(this);

            }
            //For other player
            if (otherInteraction.availableInteractions.Contains(closestInteractable))
                otherInteraction.availableInteractions.Remove(closestInteractable);
            otherInteraction.SetInteractionText();

        }
        else
        {
            if (inventory.currentItem != null)
            {
                inventory.DropItem();
            }
        }
        SetInteractionText();
    }

    //Context Text
    void SetInteractionText()
    {
        if (availableInteractions.Count > 0)
        {
            Interactable closeInt = FindClosestInteractable();
            contextTextPersonal.gameObject.SetActive(true);
            contextTextShared.gameObject.SetActive(true);
            closeInt.CheckRequiredItems(inventory);
            contextTextPersonal.text = closeInt.contextText.GetLocalizedString();
            contextTextShared.text = closeInt.contextText.GetLocalizedString();
        }
        else
        {
            contextTextPersonal.gameObject.SetActive(false);
            contextTextShared.gameObject.SetActive(false);
        }
    }

    string ParseContextText(string _stringToParse)
    {
        if (_stringToParse.Contains("/")) // /-/ -> all text, /2/ back two letters
        {
            string[] parsedString = _stringToParse.Split("/");
            int i;
            if (int.TryParse(parsedString[1], out i))
            {
                string newParsedString = parsedString[0].Substring(0, parsedString[0].Length - i);
                return newParsedString + parsedString[2];
            }
            else
            {
                if (parsedString[1] == "-")
                    return parsedString[2];
                else
                {
                    Debug.LogError("Incorrect Formatting - " + _stringToParse);
                    return "ERROR: Incorrect Formatting - " + _stringToParse;
                }
            }
        }
        else
        {
            return _stringToParse;
        }

    }
}
