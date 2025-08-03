using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance;
    public GameObject HintUI;
    public Text hintText;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    public void ShowHint(string text)
    {
        HintUI.SetActive(true);
        hintText.text = text;
    }

    public void CloseHint()
    {
        HintUI.SetActive(false);
        hintText.text = string.Empty;
    }

}
