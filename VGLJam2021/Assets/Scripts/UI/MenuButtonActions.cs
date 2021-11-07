using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonActions : MonoBehaviour
{
    public void Restart()
    {
        LevelSpawner.instance.ResetLevel();
        MenuSpawner.instance.CloseMenu();
    }
}
