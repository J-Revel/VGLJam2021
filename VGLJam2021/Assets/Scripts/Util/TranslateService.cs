using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class TranslateService : MonoBehaviour
{
    public TextAsset textAsset;
    private JSONNode[] keys;
    private string[] langs;
    public int langIndex = 0;

    public static TranslateService instance;
    public System.Action onLanguageChange;

    private void Awake()
    {
        instance = this;
    }

    public string currentLanguage { get { return langs[langIndex]; } }
    
    void Start()
    {
        JSONNode root = JSONNode.Parse(textAsset.text);
        langs = new string[root.Count];
        keys = new JSONNode[root.Count];
        int cursor = 0;
        foreach(string key in root.Keys)
        {
            langs[cursor] = key;
            keys[cursor] = root[key];
            cursor++;
        }
    }

    public string Translate(string key)
    {
        JSONNode node = keys[langIndex];
        if(!node.HasKey(key))
            return "Missing key " + key + " in wording";
        return node[key];
    }

    public void SelectLanguage(string language)
    {
        for(int i=0; i<langs.Length; i++)
        {
            if(langs[i] == language)
            {
                langIndex = i;
                onLanguageChange?.Invoke();
                return;
            }
        }
        Debug.LogError("Selected missing language : " + language);
        return;
    }
}
