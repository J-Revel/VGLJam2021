using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContainer : MonoBehaviour
{
    public static LevelContainer instance;
    void Awake()
    {
        instance = this;
    }
}
