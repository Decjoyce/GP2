using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region singleton
    public static ItemManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ItemManager found");
            return;
        }
        instance = this;
    }
    #endregion singleton

    public GameManager gm;
   //public List<ItemStatus> listOfItemStatus = new List<ItemStatus>();
   public List<Interaction_Pickup> listOfItems = new List<Interaction_Pickup>();

    int ids;

    private void OnEnable()
    {
        gm.OnLoadGameData_Environment += LoadItems;
        gm.OnSaveGameData_Environment += SaveItems;
    }

    private void OnDisable()
    {
        gm.OnLoadGameData_Environment -= LoadItems;
        gm.OnSaveGameData_Environment -= SaveItems;
    }



    public void LoadItems()
    {
        for(int i = 0; i < listOfItems.Count; i++)
        {
            listOfItems[i].item = gm.gd_environment.itemStatus[i].item;
            listOfItems[i].transform.rotation = gm.gd_environment.itemStatus[i].rot;
            listOfItems[i].transform.rotation = gm.gd_environment.itemStatus[i].rot;
        }

    }

    public void SaveItems()
    {
        Debug.Log("Shush");
        foreach(Interaction_Pickup item in listOfItems)
        {
            gm.gd_environment.itemStatus.Add(item.itemStatus);
        }

    }

    public void AddItemToList(Interaction_Pickup item)
    {
        if (!listOfItems.Contains(item))
        {
            listOfItems.Add(item);
        }
    }

    public void RemoveItemFromList(Interaction_Pickup item)
    {
        listOfItems.Remove(listOfItems.Find(x => x.id == item.id));
        //Debug.Log(listOfItems.Find(x => x.id == item.id).id);
    }
}
