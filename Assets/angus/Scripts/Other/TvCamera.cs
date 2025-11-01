using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvCamera : MonoBehaviour
{
    public Transform target;
    public float stopDistance = 1.0f;
    public float moveSpeed = 2.0f;

    public bool useTargetForward = true;

    [Header("Start")]
    public float startDelay = 0f;
    public bool autoStart = true;

    Vector3 desiredPos;
    bool running;

    void Start()
    {
        if (autoStart) BeginZoomIn();
    }

    public void BeginZoomIn()
    {
        if (target == null) { Debug.LogWarning("[CameraZoomInApproach] Target is null."); return; }
        StopAllCoroutines();
        StartCoroutine(ZoomInRoutine());
    }

    IEnumerator ZoomInRoutine()
    {
        if (startDelay > 0f) yield return new WaitForSeconds(startDelay);
        running = true;
        desiredPos = useTargetForward
        ? target.position - target.forward * stopDistance
        : target.position - (target.position - transform.position).normalized * stopDistance;

        while (running)
        {
            // 位移
            transform.position = Vector3.MoveTowards(
                transform.position,
                desiredPos,
                moveSpeed * Time.deltaTime
            );

            // 到達誤差閾值即停止
            if (Vector3.Distance(transform.position, desiredPos) < 0.01f)
                running = false;

            yield return null;
        }

        yield return null;
    }
}
