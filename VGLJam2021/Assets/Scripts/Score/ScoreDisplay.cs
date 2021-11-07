using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    private float currentScore;
    public float scoreIncreaseSpeed = 50;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void Update()
    {
        if(currentScore < ScoreSystem.instance.score)
            currentScore += Time.deltaTime * scoreIncreaseSpeed;
        text.text = ((int)currentScore).ToString("00000");
    }
}
