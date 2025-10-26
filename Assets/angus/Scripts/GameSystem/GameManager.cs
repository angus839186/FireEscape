using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

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
        OnLevelEnd?.Invoke(pass);
    }

    public void RestartLevel()
    {
        sceneLoader.RestartScene();
    }

    public void Back()
    {
        sceneLoader.BackToMenu();
    }
}
