using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
}

public class LeaderboardMenu : MonoBehaviour
{
    public LeaderboardLine linePrefab;
    public int pageSize = 10;

    private LeaderboardLine[] lines;
    IEnumerator Start()
    {
        lines = new LeaderboardLine[pageSize];
        for(int i=0; i<pageSize; i++)
        {
            lines[i] = Instantiate(linePrefab, transform);
        }

        UnityWebRequest webRequest = UnityWebRequest.Get("http://webservice.guilloteam.fr/score/page/0/" + pageSize);
        yield return webRequest.SendWebRequest();
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                Debug.Log(":\nReceived: " + webRequest.downloadHandler.text);
                RequestResult<List<LeaderboardEntry>> entries = JsonUtility.FromJson<RequestResult<List<LeaderboardEntry>>>(webRequest.downloadHandler.text);
                Debug.Log(entries.data.Count);
                for(int i=0; i<pageSize; i++)
                {
                    lines[i].leaderboardEntry = entries.data[i];
                }
                break;
            default:
                Debug.Log("Error: " + webRequest.error);
                break;
        }
        
    }

    void Update()
    {
        
    }
}
