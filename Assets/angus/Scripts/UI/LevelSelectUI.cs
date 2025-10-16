using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LevelSelectUI : MonoBehaviour
{
    public GameObject LevelSelectMenu;

    public LevelData[] levels;

    public event Action<LevelData> OnSelectLevel;

    public void CloseLevelSelectUI()
    {
        LevelSelectMenu.SetActive(false);
    }

    public void SelectLevel(int index)
    {
        LevelData level = levels[index];
        OnSelectLevel?.Invoke(level);
    }
}
