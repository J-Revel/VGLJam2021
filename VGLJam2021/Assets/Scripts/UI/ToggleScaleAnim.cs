using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScaleAnim : MonoBehaviour
{
    private Toggle toggle;
    public float selectedScale = 1.2f;
    private Image image;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        image = GetComponent<Image>();
        toggle.onValueChanged.AddListener(OnValueChanged);
        transform.localScale = toggle.isOn ? Vector3.one * selectedScale : Vector3.one;
        image.enabled = toggle.isOn;
    }

    void OnValueChanged(bool value)
    {
        transform.localScale = value ? Vector3.one * selectedScale : Vector3.one;
        image.enabled = value;
    }
}
