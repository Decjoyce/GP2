using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Lighting Presets")]
public class LightingPreset : ScriptableObject
{
    public Gradient ambientColor;
    public Gradient directionalColor;
    public Gradient fogColor;
}
