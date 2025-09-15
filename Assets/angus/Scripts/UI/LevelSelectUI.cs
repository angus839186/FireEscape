using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectUI : MonoBehaviour
{
    public GameObject LevelSelectMenu;

    public void CloseLevelSelectUI()
    {
        LevelSelectMenu.SetActive(false);
    }
}
