using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardLine : MonoBehaviour
{
    public LeaderboardEntry leaderboardEntry { 
        set { 
            usernameText.text = value.username;
            rankText.text = value.rank.ToString();
            scoreText.text = value.score.ToString();
        }
    }
    public TMPro.TextMeshProUGUI usernameText;
    public TMPro.TextMeshProUGUI rankText;
    public TMPro.TextMeshProUGUI scoreText;
}
