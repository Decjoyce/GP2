using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManagerTest : MonoBehaviour
{
    public GameData settingsData;

    public AudioMixer mixer;

    public LocalizationManager lm;
    public Slider volSlider;

    // Start is called before the first frame update
    void Start()
    {
        settingsData.Start();
        lm.SetLocale(settingsData.gd_settings.languageIndex);
        volSlider.value = settingsData.gd_settings.volume;
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
        settingsData.gd_settings.volume = volSlider.value; 
    }
}
