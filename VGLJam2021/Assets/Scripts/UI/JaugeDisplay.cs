using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JaugeDisplay : MonoBehaviour
{
    public Sprite[] fixSprites;
    public Sprite[] dynamicSprites;
    public Color[] jaugeColors;
    public Image fixImage;
    public Image dynamicImage;
    public Animator animator;
    public Image jaugeImage;

    public int currentComboIndex;
    public int currentComboValue;
    public float displayComboSpeed = 3;
    private float displayComboValue;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool("Switch", ScoreSystem.instance.comboIndex != currentComboIndex);
        if(ScoreSystem.instance.comboValue > displayComboValue)
        {
            displayComboValue = Mathf.Min(displayComboValue + Time.deltaTime * displayComboSpeed, ScoreSystem.instance.comboValue);
        }
        else if(ScoreSystem.instance.comboValue > displayComboValue)
        {
            displayComboValue = Mathf.Max(displayComboValue - Time.deltaTime * displayComboSpeed, ScoreSystem.instance.comboValue);
        }
        jaugeImage.fillAmount = displayComboValue / ScoreSystem.instance.maxComboValue;
    }

    public void UpdateEnemySprite()
    {
        fixImage.sprite = fixSprites[ScoreSystem.instance.comboIndex + 1];
        dynamicImage.sprite = dynamicSprites[ScoreSystem.instance.comboIndex + 1];
        currentComboIndex = ScoreSystem.instance.comboIndex;
        jaugeImage.color = jaugeColors[ScoreSystem.instance.comboIndex + 1];
        displayComboValue = ScoreSystem.instance.comboValue;
    }
}
