using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

    public Button nextPageButton;
    public Button previousPageButton;
    public int pageCount = 0;
    private int scoreId = -1;

    public int tempScore;
    public string tempUsername;

    IEnumerator Start()
    {
        
        lines = new LeaderboardLine[pageSize];
        for(int i=0; i<pageSize; i++)
        {
            lines[i] = Instantiate(linePrefab, mainContainer);
            lines[i].index = i;
        }
        nextPageButton.onClick.AddListener(ShowNextPage);
        previousPageButton.onClick.AddListener(ShowPreviousPage);
        yield return UpdateDisplay();
    }

    IEnumerator UpdateDisplay()
    {
        loadingScreen.SetActive(true);
        for(int i=0; i<lines.Length; i++)
            lines[i].Clear();
        int scoreId = PlayerPrefs.GetInt("scoreId", -1);
        WWWForm form = new WWWForm();
        form.AddField("pageIndex", pageIndex);
        form.AddField("pageSize", pageSize);
        form.AddField("tempId", scoreId);
        form.AddField("tempScore", tempScore);
        form.AddField("tempUsername", tempUsername);
        UnityWebRequest webRequest = UnityWebRequest.Post("http://webservice.guilloteam.fr/score/page/",  form);
        yield return webRequest.SendWebRequest();
        loadingScreen.SetActive(false);
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                RequestResult<LeaderboardRequestResult> requestResult = JsonUtility.FromJson<RequestResult<LeaderboardRequestResult>>(webRequest.downloadHandler.text);
                
                for(int i=0; i<Mathf.Min(lines.Length, requestResult.data.scores.Length); i++)
                {
                    lines[i].leaderboardEntry = requestResult.data.scores[i];
                    lines[i].highlighted = requestResult.data.scores[i].id == scoreId;
                }
                pageCount = requestResult.data.pageCount;
                previousPageButton.gameObject.SetActive(pageIndex > 0);
                nextPageButton.gameObject.SetActive(pageIndex < pageCount - 1);
                break;
            default:
                Debug.Log("Error: " + webRequest.error);
                break;
        }
    }

    public void ShowNextPage()
    {
        pageIndex++;
        StartCoroutine(UpdateDisplay());
    }

    public void ShowPreviousPage()
    {
        pageIndex--;
        StartCoroutine(UpdateDisplay());
    }
}
