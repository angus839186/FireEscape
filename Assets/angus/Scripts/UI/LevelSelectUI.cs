using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectUI : MonoBehaviour
{
    public GameObject LevelSelectMenu;

    public LevelData[] levels;

    public SceneLoader sceneloader;

    public void CloseLevelSelectUI()
    {
        LevelSelectMenu.SetActive(false);
    }

    public void SelectLevel(int index)
    {
        LevelData level = levels[index];
        sceneloader.LoadLevel(level);
    }
}
