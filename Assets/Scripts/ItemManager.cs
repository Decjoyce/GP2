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

    [SerializeField] Transform[] spawnpoints;

    [SerializeField] GameObject[] itemPrefabs;

    private void Start()
    {
        for (int i = 0; i < spawnpoints.Length; i++)
        {
            int ranInt = Random.Range(0, itemPrefabs.Length);
            Instantiate(itemPrefabs[ranInt], spawnpoints[i].position, spawnpoints[i].rotation, transform);
        }
    }
}
