using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    public bool paused;
    public GameObject pauseMenu;
    void Awake()
    {
        if (GameInputManager.Instance != null)
        {
            GameInputManager.Instance.OnPauseInput += TogglePauseMenu;
        }
    }

    void OnDisable()
    {
        if (GameInputManager.Instance != null)
        {
            GameInputManager.Instance.OnPauseInput -= TogglePauseMenu;
        }
    }

    public void TogglePauseMenu()
    {
        paused = !paused;
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused ? true : false;
        pauseMenu.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
    }

    public void OnClickContinue()
    {
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(paused);
        Time.timeScale = 1f;
    }
    public void OnClickBack()
    {
        GameManager.Instance.Back();
    }
}
