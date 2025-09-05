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


    public void ShowHint(Hint hint)
    {
        StartCoroutine(HintAnime(hint));
    }
    public IEnumerator HintAnime(Hint hint)
    {
        HintUI.SetActive(true);
        hintText.text = hint.hintText;
        yield return new WaitForSeconds(hint.HintDuration);
        HintUI.SetActive(false);
        hintText.text = string.Empty;
    }

}
