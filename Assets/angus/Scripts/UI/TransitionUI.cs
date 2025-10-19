using System.Collections;
using UnityEngine;

public class TransitionUI : MonoBehaviour
{
    public CanvasGroup transitionCanvas;
    public float transitionSpeed = 2f;

    private Bed _bed;
    private Coroutine _running;

    void Awake()
    {
        _bed = FindFirstObjectByType<Bed>();
        if (_bed != null) _bed.playerSleep += TransitionImage;
    }

    void OnDestroy()
    {
        if (_bed != null) _bed.playerSleep -= TransitionImage;
    }

    void TransitionImage(bool toggle)
    {
        if (_running != null) StopCoroutine(_running);
        _running = StartCoroutine(TransitionCoroutine(toggle));
    }

    IEnumerator TransitionCoroutine(bool toggle)
    {
        if (transitionCanvas == null) yield break;

        float target = toggle ? 1f : 0f;


        transitionCanvas.blocksRaycasts = target > transitionCanvas.alpha;

        while (!Mathf.Approximately(transitionCanvas.alpha, target))
        {
            transitionCanvas.alpha = Mathf.MoveTowards(
                transitionCanvas.alpha,
                target,
                transitionSpeed * Time.deltaTime
            );
            yield return null; // 等下一幀
        }

        // 校正到精確目標值
        transitionCanvas.alpha = target;
        transitionCanvas.blocksRaycasts = target > 0f;

        _running = null;
    }
}
