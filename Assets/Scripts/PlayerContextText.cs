using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class PlayerContextText : MonoBehaviour
{
    [SerializeField] KeyCode inputKey;
    [SerializeField] LocalizedString inputText;
      
    [SerializeField]TextMeshProUGUI tmp;

    private void OnEnable()
    {
        inputText.Arguments = new object[] { inputKey, ""};
        inputText.StringChanged += UpdateText;
    }

    private void OnDisable()
    {
        inputText.StringChanged -= UpdateText;
    }

    void UpdateText(string newText)
    {
        tmp.text = newText;
    }

    public void ChangeText(LocalizedString contextText)
    {
        inputText.Arguments[1] = contextText.GetLocalizedString();
        inputText.RefreshString();
    }
}
