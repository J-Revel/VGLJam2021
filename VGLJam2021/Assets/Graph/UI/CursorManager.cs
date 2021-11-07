using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CursorManager : MonoBehaviour
{
    public Texture2D cursorTex;
    Vector2 hotSpot = new Vector2(50, 0);
   
    void Awake() { Cursor.SetCursor(cursorTex, hotSpot, CursorMode.ForceSoftware); }

  
}