using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;
    public int score = 0;
    public int comboIndex = -1;
    public int comboValue = 1;
    public int maxComboValue = 8;
    
    private void Awake()
    {
        instance = this;
    }

    public int AddScore(int value, int comboIndex)
    {
        if(comboIndex != this.comboIndex)
        {
            this.comboIndex = comboIndex;
            comboValue = 1;
        }
        else
        {
            comboValue = Mathf.Min(comboValue+1, maxComboValue);
        }
        score += value * comboValue;
        return value * comboValue;
    }
}
