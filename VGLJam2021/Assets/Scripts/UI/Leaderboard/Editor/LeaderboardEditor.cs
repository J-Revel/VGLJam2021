using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.IMGUI;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.Networking;
using System.Runtime.CompilerServices;
using UnityEditor.IMGUI.Controls;


public class LeaderboardEditor : EditorWindow
{
    private TextAsset configAsset;
    private SimpleJSON.JSONNode configRoot;
    private bool initialized = false;

    private string leaderboardUid;
    private string password;

    public string error;
    private LeaderboardEntry[] displayLeaderboardData;
    private LeaderboardEntry editedLeaderboardEntry;
    private Vector2 scrollPos; 
    public MultiColumnHeader columnHeader;
    public MultiColumnHeaderState.Column[] columns;
    public Color darkerBgColor, lighterBgColor;
    private int testValue;
    string[] columnNames = new string[]{"rank", "username", "score", "actions"};
    private Rect windowVisibleRect;
    private int editedIndex = -1;
    
    private enum EditorState
    {

    }

    [MenuItem ("Window/Leaderboard Configurator")]
    public static void ShowWindow () {
        EditorWindow.GetWindow(typeof(LeaderboardEditor));
    }

    void OnEnable()
    {
        AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
        AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
    }

    void OnDisable()
    {
        AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
        AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
    }

    private void OnBeforeAssemblyReload()
    {

    }

    private void OnAfterAssemblyReload()
    {
        LoadConfig();
        Initialize();
    }


    void LoadConfig()
    {
        configAsset = Resources.Load<TextAsset>("config");
        if(configAsset != null)
        {
            try
            {
                configRoot = SimpleJSON.JSON.Parse(configAsset.text);
            }
            catch(System.Exception e)
            {
                error = e.ToString();
            }
        }
    }

    void Initialize()
    {
        
        MultiColumnHeaderState.Column[] columns = new MultiColumnHeaderState.Column[columnNames.Length];
        for(int i=0; i<columns.Length; i++)
        {
            columns[i] = new MultiColumnHeaderState.Column()
            {
                headerContent = new GUIContent(columnNames[i]),
                width = i == 0 ? 50 : 200,
                minWidth = 50,
                maxWidth = 1000,
                autoResize = true,
                headerTextAlignment = TextAlignment.Center
            };
        }
        columnHeader = new MultiColumnHeader(new MultiColumnHeaderState(columns));
        columnHeader.height = 25;
        columnHeader.ResizeToFit();
        LoadConfig();
    }

    void OnGUI()
    {

        // Handle editor reload
        if(!initialized || (configAsset != null && configRoot == null))
        {
            initialized = true;
            
            Initialize();
        }
        if(error != "")
        {
            GUILayout.Label(error);
        }
        if(configRoot != null)
        {
            GUILayout.Label("Leaderboard connected : " + configRoot["project_uid"]);
            if(GUILayout.Button("Update leaderboard"))
            {
                RunTask(configRoot["project_uid"]);
            }
        }
        
        GUILayout.FlexibleSpace();
        if(Event.current.type == EventType.Repaint)
        {
            windowVisibleRect = GUILayoutUtility.GetLastRect();
        
            windowVisibleRect.width = position.width;
            windowVisibleRect.height = position.height;
        }
        var headerRect = windowVisibleRect;
        headerRect.height = columnHeader.height;
        float xScroll = 0;
        columnHeader.OnGUI(headerRect, xScroll);

        Rect[] columnRects = new Rect[columnNames.Length];
        for(int i=0; i<columnNames.Length; i++)
        {
            columnRects[i] = columnHeader.GetColumnRect(i);
            float columnHeight = EditorGUIUtility.singleLineHeight;
            Rect columnRect = new Rect();
            columnRect.position = new Vector2(columnRects[i].xMin, headerRect.yMax);
            columnRect.size = new Vector2(columnRects[i].width, position.size.y);
            columnRects[i] = columnRect;
            // EditorGUI.DrawRect(rect: columnRects[i], color: Color.white * (float)(i+1) / columnRects.Length);
        }

        GUIStyle nameFieldGUIStyle = new GUIStyle(GUI.skin.label)
        {
            padding = new RectOffset(left: 10, right: 10, top: 2, bottom: 2),
            alignment = TextAnchor.MiddleCenter
        };
        
        float rowHeight = EditorGUIUtility.singleLineHeight;
        Rect testRect = new Rect();
        if(displayLeaderboardData != null)
        {

            for(int i=0; i<displayLeaderboardData.Length; i++)
            {
                Rect rowRect = new Rect();
                rowRect.position = new Vector2(0, headerRect.yMax + rowHeight * i);
                rowRect.size = new Vector2(position.width, rowHeight);
                EditorGUI.DrawRect(rect: rowRect, color: Color.white * (0.1f + 0.2f * (i%2)));
                
                Rect rankRect = new Rect(rowRect);
                rankRect.width = columnRects[0].width;
                EditorGUI.LabelField(
                    position: rankRect,
                    label: new GUIContent(displayLeaderboardData[i].rank.ToString()),
                    style: nameFieldGUIStyle
                );
                Rect usernameRect = new Rect(rowRect);
                usernameRect.width = columnRects[1].width;
                usernameRect.position = new Vector2(columnRects[1].position.x, usernameRect.position.y);
                if(editedIndex == i)
                {
                    editedLeaderboardEntry.username = EditorGUI.TextField(position: usernameRect,
                        text: editedLeaderboardEntry.username);
                }
                else
                {
                    EditorGUI.LabelField(
                        position: usernameRect,
                        label: new GUIContent(displayLeaderboardData[i].username),
                        style: nameFieldGUIStyle
                    );
                }
                Rect scoreRect = new Rect(rowRect);
                usernameRect.width = columnRects[2].width;
                usernameRect.position = new Vector2(columnRects[2].position.x, usernameRect.position.y);
                if(editedIndex == i)
                {
                    editedLeaderboardEntry.score = EditorGUI.IntField(position: usernameRect, value: editedLeaderboardEntry.score);
                }
                else
                {
                    EditorGUI.LabelField(
                        position: usernameRect,
                        label: new GUIContent(displayLeaderboardData[i].score.ToString()),
                        style: nameFieldGUIStyle
                    );

                }

                Rect actionsRect = new Rect(rowRect);
                actionsRect.width = columnRects[3].width;
                actionsRect.position = new Vector2(columnRects[3].position.x, actionsRect.position.y);
                using (var areaScope = new GUILayout.AreaScope(actionsRect))
                {
                    EditorGUILayout.BeginHorizontal();
                    if(editedIndex == i)
                    {
                        if(GUILayout.Button("Save"))
                        {
                            displayLeaderboardData[editedIndex].score = editedLeaderboardEntry.score;
                            displayLeaderboardData[editedIndex].username = editedLeaderboardEntry.username;
                            editedIndex = -1;
                        }
                        if(GUILayout.Button("Cancel"))
                        {
                            editedIndex = -1;
                        }
                    }
                    else if(editedIndex < 0)
                    {
                        if(GUILayout.Button("Edit"))
                        {
                            editedIndex = i;
                            editedLeaderboardEntry.score = displayLeaderboardData[editedIndex].score;
                            editedLeaderboardEntry.username = displayLeaderboardData[editedIndex].username;
                        }
                    }
                    else GUILayout.FlexibleSpace();
                    if(GUILayout.Button("-", GUILayout.Width(rowHeight)))
                    {
                        if(EditorUtility.DisplayDialog("Remove score", "Remove a score ?", "OK", "Cancel"))
                        {
                            Debug.Log("Remove " + i);
                        }
                    }
                    GUILayout.EndHorizontal();
                }
            }
        }
        
        EditorGUI.DrawRect(rect: testRect, color: new Color(1, 0, 0, 0.5f));
    }

    public async void RunTask(string projectId)
    {
        Task task = _ExecuteTaskAfterDelay(projectId);
        await task;
    }

    private async Task _ExecuteTaskAfterDelay(string projectId)
    {
        UnityWebRequest request = LeaderboardUtility.GetLeaderboardRequest(projectId, true, 50);
        await request.SendWebRequest();
        try {
            SimpleJSON.JSONNode rootNode = SimpleJSON.JSON.Parse(request.downloadHandler.text);
            if(rootNode["success"].AsBool == true)
            {
                displayLeaderboardData = LeaderboardUtility.ParseLeaderboardQueryResult(rootNode);
            }
            else
            {
                error = "Request error";
            }
        }
        catch(System.Exception e)
        {
            error = e.ToString();
        }
    }
}

public class UnityWebRequestAwaiter : INotifyCompletion
{
	private UnityWebRequestAsyncOperation asyncOp;
	private System.Action continuation;

	public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
	{
		this.asyncOp = asyncOp;
		asyncOp.completed += OnRequestCompleted;
	}

	public bool IsCompleted { get { return asyncOp.isDone; } }

	public void GetResult() { }

	public void OnCompleted(System.Action continuation)
	{
		this.continuation = continuation;
	}

	private void OnRequestCompleted(AsyncOperation obj)
	{
		continuation();
	}
}

public static class ExtensionMethods
{
	public static UnityWebRequestAwaiter GetAwaiter(this UnityWebRequestAsyncOperation asyncOp)
	{
		return new UnityWebRequestAwaiter(asyncOp);
	}
}