using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFireUI : MonoBehaviour
{
    public CanvasGroup transitionCanvas;

    public float transitionSpeed;

    public float delayTime;
    void Awake()
    {
        PlayerHealth player = FindFirstObjectByType<PlayerHealth>();
        if (player != null)
        {
            player.OnPlayerTakeDamage += GetFire;
        }
    }
    void OnDisable()
    {
        PlayerHealth player = FindFirstObjectByType<PlayerHealth>();
        if (player != null)
        {
            player.OnPlayerTakeDamage -= GetFire;
        }
    }

    void GetFire(DamageType type)
    {
        if (type != DamageType.Fire)
        {
            return;
        }
        else
        {
            StartCoroutine(GetFireCoroutine());
        }
    }
    IEnumerator GetFireCoroutine()
    {
        yield return FadeCanvas(1f);

        yield return new WaitForSecondsRealtime(delayTime);


        yield return FadeCanvas(0f);
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
