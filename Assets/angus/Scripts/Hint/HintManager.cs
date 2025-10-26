using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance;

    [Header("UI")]
    public GameObject HintUI;
    public Text hintText;

    [Header("Timing")]
    public float nextHintDelayTime;

    private Coroutine currentCoroutine;

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

        if (HintUI != null) HintUI.SetActive(false);
    }


    public void ShowHint(Hint hint)
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
        currentCoroutine = StartCoroutine(HintRoutine(hint.hintText));
    }


    public void StopAllHints()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }

        HintUI.SetActive(false);
    }

    private IEnumerator HintRoutine(string[] lines)
    {
        HintUI.SetActive(true);

        for (int i = 0; i < lines.Length; i++)
        {
            hintText.text = lines[i];

            yield return new WaitForSeconds(nextHintDelayTime);
        }

        HintUI.SetActive(false);
        currentCoroutine = null;
    }

    private void OnDisable()
    {
        StopAllHints();
    }
}
