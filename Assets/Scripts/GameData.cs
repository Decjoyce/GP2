using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GameData_Settings
{
    public bool beenInit;
    public int languageIndex;
    public float volume;
}

[CreateAssetMenu(fileName = "gd_settings", menuName = "GameData")]
public class GameData : ScriptableObject
{

    public GameData_Settings gd_settings;

    public void Start()
    {
        LoadGameData();
    }

    public void LoadGameData()
    {
        if (!gd_settings.beenInit)
        {
            gd_settings = new GameData_Settings();

            gd_settings.languageIndex = 0;
            gd_settings.volume = 100;
            gd_settings.beenInit = true;
        }
    }
}
