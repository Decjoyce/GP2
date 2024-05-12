using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DayNightCycle : MonoBehaviour
{
    public UnityEvent OnNightTime, OnMorningTime, OnDayTime, OnAfternoonTime;

    [SerializeField] Light theSun;
    [SerializeField] LightingPreset preset;
    [SerializeField] float lengthOfDay;
    [SerializeField, Range(0,240f)] private float timeOfDay;

    public PeriodOfDay periodOfDay;

    public enum PeriodOfDay
    {
        night,
        morning,        
        day,
        afternoon,
    }



    private void Update()
    {
        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime;
            timeOfDay %= lengthOfDay;
            UpdateLighting(timeOfDay / lengthOfDay);
            if (timeOfDay >= 60f && timeOfDay < 80f)
            {
                if (periodOfDay != PeriodOfDay.morning)
                {
                    theSun.intensity = 2;
                    OnMorningTime.Invoke();
                }
                periodOfDay = PeriodOfDay.morning;
            }
            if (timeOfDay >= 80f && timeOfDay < 160f)
            {
                if (periodOfDay != PeriodOfDay.day)
                {
                    OnDayTime.Invoke();
                    theSun.intensity = 2;
                }
                periodOfDay = PeriodOfDay.day;
            }
            if (timeOfDay >= 160f && timeOfDay < 200f)
            {                
                if(periodOfDay != PeriodOfDay.afternoon)
                {
                    OnAfternoonTime.Invoke();
                    theSun.intensity = 2;
                }
                periodOfDay = PeriodOfDay.afternoon;
            }
            if (timeOfDay >= 200f && timeOfDay <= 240f || timeOfDay >= 0f && timeOfDay < 60f)
            {
                if (periodOfDay != PeriodOfDay.night)
                {
                    //theSun.intensity = 0;
                    OnNightTime.Invoke();
                }
                periodOfDay = PeriodOfDay.night;
            }
        }
    }   

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor= preset.fogColor.Evaluate(timePercent);

        theSun.color = preset.directionalColor.Evaluate(timePercent);
        theSun.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
    }
}
