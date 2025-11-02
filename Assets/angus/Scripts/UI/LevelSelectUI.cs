using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LevelSelectUI : MonoBehaviour
{
    public GameObject LevelSelectMenu;
    public GameObject GameMenu;

    public LevelData[] levels;

    public void CloseLevelSelectUI()
    {
        GameMenu.SetActive(true);
        LevelSelectMenu.SetActive(false);
    }

    public void SelectLevel(int index)
    {
        LevelData level = levels[index];
        GameManager.Instance.StartGameScene(level);
    }
}
