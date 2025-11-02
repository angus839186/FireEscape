using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("Fade")]
    public CanvasGroup transitionCanvas;
    [Tooltip("數值越大，淡入/淡出越快")]
    public float transitionSpeed = 2f;

    [Header("Dialogue")]
    public Text dialogueText;
    [SerializeField] private float dialogueDelayTime = 1.2f;

    private Coroutine dialogueCoroutine;

    void Awake()
    {
        PlayerTalk playertalk = FindFirstObjectByType<PlayerTalk>();
        if (playertalk != null)
        {
            playertalk.OnPlayerTalk += StartNewDialogue;
        }
    }
    
    void OnDisable()
    {
        PlayerTalk playertalk = FindFirstObjectByType<PlayerTalk>();
        if(playertalk != null)
        {
            playertalk.OnPlayerTalk -= StartNewDialogue;
        }
    }


    public void StartNewDialogue(DialogueData newDialogue)
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
        }
        dialogueCoroutine = StartCoroutine(DialogueFlow(newDialogue.DialogueText));
    }


    private IEnumerator DialogueFlow(string[] sentences)
    {

        transitionCanvas.alpha = 0f;
        dialogueText.text = string.Empty;

        StartCoroutine(FadeCanvas(1f));


        for (int i = 0; i < sentences.Length; i++)
        {
            dialogueText.text = sentences[i];
            yield return new WaitForSeconds(dialogueDelayTime);
        }


        yield return new WaitForSeconds(dialogueDelayTime);


        yield return FadeCanvas(0f);


        dialogueText.text = string.Empty;

        dialogueCoroutine = null;
    }

    private IEnumerator FadeCanvas(float targetAlpha)
    {

        const float EPS = 0.001f;
        float start = transitionCanvas.alpha;

        // 若已經接近目標，就直接設值
        if (Mathf.Abs(start - targetAlpha) <= EPS)
        {
            transitionCanvas.alpha = targetAlpha;
            yield break;
        }


        while (Mathf.Abs(transitionCanvas.alpha - targetAlpha) > EPS)
        {
            transitionCanvas.alpha = Mathf.MoveTowards(
                transitionCanvas.alpha,
                targetAlpha,
                transitionSpeed * Time.deltaTime
            );
            yield return null;
        }

        transitionCanvas.alpha = targetAlpha;
    }
}
