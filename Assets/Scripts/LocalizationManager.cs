using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.Localization.Settings;

public class LocalizationManager : MonoBehaviour
{
    bool active = false;

    int currentLocale;

    public SettingsManagerTest sm;

    public void ChangeLocale()
    {
        if (active == true)
            return;

        if (currentLocale == 1)
            currentLocale = 0;
        else
            currentLocale = 1;
        StartCoroutine(SettingLocale());
    }

    public void SetLocale(int i)
    {
        currentLocale = i;
        StartCoroutine(SettingLocale());
    }

    IEnumerator SettingLocale()
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[currentLocale];
        sm.settingsData.gd_settings.languageIndex = currentLocale;
        active = false;
    }
}
