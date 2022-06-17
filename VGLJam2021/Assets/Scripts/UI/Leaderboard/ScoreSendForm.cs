using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreSendForm : MonoBehaviour
{
    public TMPro.TMP_InputField inputField;
    public TMPro.TextMeshProUGUI scoreText;
    public LeaderboardMenu leaderboardMenuPrefab;
    
    void Start()
    {
        
    }

    private void Update()
    {
        if(scoreText != null)
            scoreText.text = "" + ScoreSystem.instance.score;
    }

    public void SendScore()
    {
        StartCoroutine(SendScoreCoroutine());
    }

    public IEnumerator SendScoreCoroutine()
    {
        int scoreId = PlayerPrefs.GetInt("scoreId", -1);
        WWWForm form = new WWWForm();
        string username = inputField.text;
        form.AddField("username", username);
        form.AddField("id", scoreId);
        form.AddField("score", ScoreSystem.instance.score);
        string requestPath = "http://webservice.guilloteam.fr/score/add/";
        if(scoreId >= 0)
        {
            requestPath = "http://webservice.guilloteam.fr/score/update/";
        }
        UnityWebRequest webRequest = UnityWebRequest.Post(requestPath,  form);
        
        yield return webRequest.SendWebRequest();
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                SimpleJSON.JSONNode rootNode = SimpleJSON.JSON.Parse(webRequest.downloadHandler.text);
                int rank = rootNode["data"]["rank"];
                int id = rootNode["data"]["id"];
                if(scoreId < 0)
                    PlayerPrefs.SetInt("scoreId", id);
                MenuSpawner.instance.CloseMenu();
                LeaderboardMenu spawnedMenu = MenuSpawner.instance.SpawnMenu(leaderboardMenuPrefab.gameObject).GetComponent<LeaderboardMenu>();
                spawnedMenu.pageIndex = rank / spawnedMenu.pageSize;
                spawnedMenu.tempScore = ScoreSystem.instance.score;
                spawnedMenu.tempUsername = username;
                break;
        }
    }
}