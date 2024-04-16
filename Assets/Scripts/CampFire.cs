using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

public class CampFire : Interactable
{
    [SerializeField] LocalizedString contexttext_noitems;
    [SerializeField] LocalizedString contexttext_haveitems;

    public Item[] requireItem;
    [SerializeField] Light fireLight;
    [SerializeField] float maxHealth;
    public float currentHealth;

    bool isLit;

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0)
            fireLight.gameObject.SetActive(true);
        else
            fireLight.gameObject.SetActive(false);

        fireLight.intensity = ExtensionMethods.Map(currentHealth, 0, maxHealth, 0, 25f);

        if (currentHealth > 0)
        {
            currentHealth -= Time.deltaTime * 2;
            isLit = true;
        }
        else if (isLit)
        {
            currentHealth = 0;
            isLit = false;
            GameManager.instance.sleepManager.WakeUpPlayers();
        }

    }

    public override void Interaction(PlayerInteraction player)
    {
        base.Interaction(player);
        if (CheckRequiredItems(player.inventory))
        {
            player.inventory.RemoveItem();
            IncreaseHealth(50);
            GameManager.instance.gd_statistics.sticksAddedToFire++;
        }
    }

    public override bool CheckRequiredItems(PlayerInventory inv)
    {
        if (inv.currentItem == null)
        {
            contextText = contexttext_noitems;
            return false;
        }
        else
        {
            foreach (Item item in requireItem)
            {
                if (item == inv.currentItem)
                {
                    contextText = contexttext_haveitems;
                   return true;
                }
     
            }
        }
        contextText = contexttext_noitems;
        return false;

    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
    }

}
