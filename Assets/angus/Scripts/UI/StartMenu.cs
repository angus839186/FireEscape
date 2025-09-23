using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public GameObject LevelSelectMenu;
    public void OpenLevelSelectUI()
    {
        LevelSelectMenu.SetActive(true);
    }

    public void Option()
    {

    }

    public void EndGame()
    {
        Application.Quit();
    }
}
