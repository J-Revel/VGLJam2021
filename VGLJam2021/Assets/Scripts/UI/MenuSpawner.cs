using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpawner : MonoBehaviour
{
    public static MenuSpawner instance;
    private GameObject currentMenu;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnMenu(Transform menu)
    {
        currentMenu = Instantiate(menu, transform).gameObject;
    }

    public void CloseMenu()
    {
        Destroy(currentMenu);
    }
}
