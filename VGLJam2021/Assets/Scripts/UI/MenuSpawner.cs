using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawner : MonoBehaviour
{
    public static MenuSpawner instance;
    private GameObject currentMenu;
    public GameObject startMenu;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SpawnMenu(startMenu);
    }

    public void SpawnMenu(GameObject menu)
    {
        currentMenu = Instantiate(menu, transform);
    }

    public void CloseMenu()
    {
        Destroy(currentMenu);
    }
}
