using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EndUI : MonoBehaviour
{
    public TransitionUI Transition;
    public GameObject winUI;
    public GameObject loseUI;
    void Awake()
    {
        GameManager.Instance.OnLevelEnd += ToggleEndUI;
    }
    void OnDisable()
    {
        GameManager.Instance.OnLevelEnd -= ToggleEndUI;
        
    }
    void ToggleEndUI(bool win)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameObject EndScreen = win ? winUI : loseUI;
        Transition.TransitionImage(true);
        EndScreen.SetActive(true);
    }
    public void OnClickRestart()
    {
        GameManager.Instance.RestartLevel();
    }
    public void OnClickBack()
    {
        GameManager.Instance.Back();
    }

}
