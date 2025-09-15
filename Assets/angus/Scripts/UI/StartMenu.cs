using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject LevelSelectMenu;
    void OpenLevelSelectUI()
    {
        LevelSelectMenu.SetActive(true);
    }

    void Option()
    {

    }

    void EndGame()
    {
        Application.Quit();
    }
}
