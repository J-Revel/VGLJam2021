using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;

[System.Serializable]
public struct RequestResult<T>
{
    public bool success;
    public T data;
}


[System.Serializable]
public struct LeaderboardEntry
{
    public int id;
    public int rank;
    public string username;
    public int score;
    public LeaderboardEntryType type;
}

[System.Serializable]
public struct LeaderboardRequestResult
{
    public LeaderboardEntry[] scores;
    public int pageCount;

}

public class LeaderboardMenu : MonoBehaviour
{
    public LeaderboardLine linePrefab;
    public int pageSize = 10;

    private LeaderboardLine[] lines;
    public int pageIndex;
    public GameObject loadingScreen;
    public Transform mainContainer;
    public int pageCount = 0;
    private int scoreId = -1;

    public int tempScore;
    public string tempUsername;

    public bool showTop = false;

    IEnumerator Start()
    {
        
        lines = new LeaderboardLine[pageSize];
        for(int i=0; i<pageSize; i++)
        {
            lines[i] = Instantiate(linePrefab, mainContainer);
            lines[i].index = i;
        }
        yield return UpdateDisplay();
    }

    IEnumerator UpdateDisplay()
    {
        loadingScreen.SetActive(true);
        for(int i=0; i<lines.Length; i++)
            lines[i].Clear();
        int scoreId = PlayerPrefs.GetInt("scoreId", -1);
        WWWForm form = new WWWForm();
        form.AddField("pageIndex", 0);
        form.AddField("pageSize", pageSize);
        form.AddField("tempId", scoreId);
        form.AddField("tempScore", tempScore);
        form.AddField("tempUsername", tempUsername);
        form.AddField("project", EncryptionService.projectId);
        UnityWebRequest webRequest = UnityWebRequest.Post("https://webservice.guilloteam.fr/score/" + (showTop ? "page/" : "around/"),  form);
        yield return webRequest.SendWebRequest();
        loadingScreen.SetActive(false);
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                JSONNode root = JSON.Parse(webRequest.downloadHandler.text);
                if(root["success"].AsBool)
                {
                    JSONArray scoresArrayJson = root["data"]["scores"].AsArray;
                    for(int i=0; i<Mathf.Min(lines.Length, scoresArrayJson.Count); i++)
                    {
                        LeaderboardEntry entry = new LeaderboardEntry();
                        entry.id = scoresArrayJson[i]["id"].AsInt;
                        entry.rank = scoresArrayJson[i]["rank"].AsInt;
                        entry.score = scoresArrayJson[i]["score"].AsInt;
                        entry.username = scoresArrayJson[i]["username"];
                        if(scoresArrayJson[i]["is_new"])
                            entry.type = LeaderboardEntryType.CurrentScore;
                        else
                        {
                            if(entry.id == scoreId)
                                entry.type = LeaderboardEntryType.BestPlayerScore;
                            else
                                entry.type = LeaderboardEntryType.Basic;
                        } 
                        lines[i].leaderboardEntry = entry;
                    }
                    for(int i=Mathf.Min(lines.Length, scoresArrayJson.Count); i<lines.Length; i++)
                    {
                        LeaderboardEntry entry = new LeaderboardEntry();
                        entry.type = LeaderboardEntryType.Disabled;
                        lines[i].leaderboardEntry = entry;
                    }
                    pageCount = root["data"]["pageCount"].AsInt;
                }
                else
                {
                    Debug.LogError("Request Error : " + root["error"]);
                }
                
                break;
            default:
                Debug.Log("Error: " + webRequest.error);
                break;
        }
    }

    public void ShowTop()
    {
        showTop = true;
        StartCoroutine(UpdateDisplay());
    }

    public void ShowMyScore()
    {
        showTop = false;
        StartCoroutine(UpdateDisplay());
    }
}
