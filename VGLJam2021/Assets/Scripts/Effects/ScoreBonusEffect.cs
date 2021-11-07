using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBonusEffect : MonoBehaviour
{
    public TMPro.TextMeshPro text;
    private float time;
    public float fadeDuration = 1;
    public float verticalSpeed;

    public Color color;
    public float score;

    void Start()
    {
        text.text = "+" + score;
    }

    void Update()
    {
        time += Time.deltaTime;
        transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
        text.color = new Color(color.r, color.g, color.b, (1 - time * time / fadeDuration / fadeDuration));
    }
}
