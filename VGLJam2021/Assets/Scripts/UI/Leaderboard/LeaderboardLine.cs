using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardLine : MonoBehaviour
{
    public LeaderboardEntry leaderboardEntry { 
        set {
            backgroundImage.color = backgroundColors[index % backgroundColors.Length];
            usernameText.text = value.username;
            rankText.text = value.rank.ToString();
            scoreText.text = value.score.ToString();
        }
    }

    public bool highlighted {
        set {
            highlightElement.SetActive(value);
        }
    }

    public void Clear()
    {
        usernameText.text = "";
        rankText.text = "";
        scoreText.text = "";
        backgroundImage.color = new Color(0, 0, 0, 0);
    }

    public int index;
    public TMPro.TextMeshProUGUI usernameText;
    public TMPro.TextMeshProUGUI rankText;
    public TMPro.TextMeshProUGUI scoreText;
    public Color[] backgroundColors;
    public Image backgroundImage;
    public GameObject highlightElement;
}
