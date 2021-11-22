using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslatedText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    public string key {set { _key = value; text.text = TranslateService.instance.Translate(value); }}
    private string _key;

    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        _key = text.text;
        text.text = TranslateService.instance.Translate(_key);
        TranslateService.instance.onLanguageChange += OnLanguageChange;
    }

    private void OnLanguageChange()
    {
        text.text = TranslateService.instance.Translate(_key);
    }

    void Update()
    {
        
    }
}
