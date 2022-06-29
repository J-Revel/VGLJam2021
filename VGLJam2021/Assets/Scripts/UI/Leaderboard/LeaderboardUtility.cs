using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class LeaderboardUtility
{
    public static string webServiceRoot = "https://webservice.guilloteam.fr/score/";
    public static UnityWebRequest GetLeaderboardRequest(string projectId, bool showTop, int pageSize, int scoreId = -1, int tempScore = 0, string tempUsername = "")
    {
        WWWForm form = new WWWForm();
        form.AddField("pageIndex", 0);
        form.AddField("pageSize", pageSize);
        form.AddField("tempId", scoreId);
        form.AddField("tempScore", tempScore);
        form.AddField("tempUsername", tempUsername);
        form.AddField("project", projectId);
        UnityWebRequest webRequest = UnityWebRequest.Post(webServiceRoot + (showTop ? "page/" : "around/"),  form);
        return webRequest;
    }
    
    public static LeaderboardEntry[] ParseLeaderboardQueryResult(JSONNode root, int scoreId = -1)
    {
        if(root["success"].AsBool)
        {
            JSONArray scoresArrayJson = root["data"]["scores"].AsArray;
            LeaderboardEntry[] result = new LeaderboardEntry[scoresArrayJson.Count];
            for(int i=0; i<result.Length; i++)
                result[i] = new LeaderboardEntry();
            for(int i=0; i<Mathf.Min(result.Length, scoresArrayJson.Count); i++)
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
                result[i] = entry;
            }

            for(int i=Mathf.Min(result.Length, scoresArrayJson.Count); i<result.Length; i++)
            {
                LeaderboardEntry entry = new LeaderboardEntry();
                entry.type = LeaderboardEntryType.Disabled;
                result[i] = entry;
            }
            return result;
        }
        else
        {
            Debug.LogError("Request Error : " + root["error"]);
        }
        return null;
    }
}
