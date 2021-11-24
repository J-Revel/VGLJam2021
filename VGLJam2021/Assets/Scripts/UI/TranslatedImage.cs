using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct NamedImage
{
    public string name;
    public Sprite sprite;
}

public class TranslatedImage : MonoBehaviour
{
    private Image image;
    public Sprite sprite {set { _sprite = value; image.sprite = value; }}
    private Sprite _sprite;

    public NamedImage[] images;

    void Start()
    {
        image = GetComponent<Image>();
        foreach(NamedImage namedImage in images)
        {
            if(namedImage.name == TranslateService.instance.currentLanguage)
            {
                image.sprite = namedImage.sprite;
            }
        }
        TranslateService.instance.onLanguageChange += OnLanguageChange;
    }

    private void OnLanguageChange()
    {
        foreach(NamedImage namedImage in images)
        {
            if(namedImage.name == TranslateService.instance.currentLanguage)
            {
                image.sprite = namedImage.sprite;
            }
        }
    }

    void Update()
    {
        
    }
}
