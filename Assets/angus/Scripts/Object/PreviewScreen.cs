using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PreviewScreen : MonoBehaviour
{
    public VideoPlayer PreviewVideo;

    public void StopPreview()
    {
        PreviewVideo.Stop();
    }
}
