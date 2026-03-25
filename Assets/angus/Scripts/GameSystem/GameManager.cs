using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static bool ShouldShowLevelSelect = false;

    public SceneLoader sceneLoader;

    LevelData level;

    public event Action<bool> OnLevelEnd;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void StartGameScene(LevelData level)
    {
        this.level = level;
        sceneLoader.LoadLevel(level);
    }

    public void LevelEnd(bool pass)
    {
        PlayerAction player = FindObjectOfType<PlayerAction>();
        player.ToggleFreeze(true);
        OnLevelEnd?.Invoke(pass);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        sceneLoader.RestartScene();
    }

    public void Back()
    {
        Time.timeScale = 1f;
        ShouldShowLevelSelect = true; // 標記：下次進入選單時要開關卡選擇
        sceneLoader.BackToMenu();
    }
}
