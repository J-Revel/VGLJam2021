using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonActions : MonoBehaviour
{
    public GameObject mainMenu;
    public void Restart()
    {
        LevelSpawner.instance.ResetLevel();
        MenuSpawner.instance.CloseMenu();
    }

    public void Play()
    {
        LevelSpawner.instance.StartLevel();
        MenuSpawner.instance.CloseMenu();
    }

    public void ReturnToMenu()
    {
        LevelSpawner.instance.StopLevel();
        MenuSpawner.instance.CloseMenu();
        MenuSpawner.instance.SpawnMenu(mainMenu);
    }
}
