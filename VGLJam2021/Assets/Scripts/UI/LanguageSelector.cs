using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    public string language = "fr";
    public Toggle toggle;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        TranslateService.instance.onLanguageChange += OnLanguageChange;
        OnLanguageChange();
    }

    private void OnDestroy()
    {
        TranslateService.instance.onLanguageChange -= OnLanguageChange;
    }

    private void OnLanguageChange()
    {
        toggle.isOn = TranslateService.instance.currentLanguage == language;
    }

    public void SelectLanguage()
    {
        if(toggle.isOn)
            TranslateService.instance.SelectLanguage(language);
    }

    public void ToggleSelected(bool value)
    {
        if(value)
        {
            TranslateService.instance.SelectLanguage(language);
        }
    }
}
