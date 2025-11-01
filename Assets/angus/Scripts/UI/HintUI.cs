using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{
    public static HintUI Instance;

    [Header("UI")]
    public GameObject HintCanvas;
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

        if (HintCanvas != null) HintCanvas.SetActive(false);
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

        HintCanvas.SetActive(false);
    }

    private IEnumerator HintRoutine(string[] lines)
    {
        HintCanvas.SetActive(true);

        for (int i = 0; i < lines.Length; i++)
        {
            hintText.text = lines[i];

            yield return new WaitForSeconds(nextHintDelayTime);
        }

        HintCanvas.SetActive(false);
        currentCoroutine = null;
    }

    private void OnDisable()
    {
        StopAllHints();
    }
}
