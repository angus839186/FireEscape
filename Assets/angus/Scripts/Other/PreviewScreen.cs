using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PreviewScreen : MonoBehaviour
{
    public VideoPlayer PreviewVideo;
    public GameObject ScreenCanvas;

    public TvCamera tvCam;

    public event Action StopPreviewVideo;

    public void StopPreview()
    {
        PreviewVideo.Stop();
        ScreenCanvas.SetActive(false);
        tvCam.gameObject.SetActive(false);

        StopPreviewVideo?.Invoke();
    }
}
