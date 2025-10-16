using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LevelVideoUI : MonoBehaviour
{
    public LevelSelectUI levelselectUI;
    public VideoPlayer videoPlayer;

    [Header("UI")]
    public Button skipButton;                 // 指到你的「跳過」按鈕

    [Header("Config")]
    [SerializeField] float prepareTimeout = 5f;
    [SerializeField] float endEarlyPadding = 0.1f;

    // 狀態
    bool skipVideo;
    bool sceneStarted;
    Coroutine _playingCoro;

    void Awake()
    {
        levelselectUI = levelselectUI ?? FindFirstObjectByType<LevelSelectUI>();
        if (levelselectUI) levelselectUI.OnSelectLevel += PlayVideo;

        if (skipButton)
        {
            skipButton.onClick.AddListener(OnSkipClicked);
            skipButton.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        if (levelselectUI) levelselectUI.OnSelectLevel -= PlayVideo;
        if (skipButton) skipButton.onClick.RemoveListener(OnSkipClicked);
    }

    void PlayVideo(LevelData level)
    {
        // reset
        skipVideo = false;
        sceneStarted = false;

        if (videoPlayer == null || level.video == null)
        {
            StartScene(level);
            return;
        }

        // 顯示跳過按鈕
        if (skipButton) skipButton.gameObject.SetActive(true);

        // 開始播放協程
        if (_playingCoro != null) StopCoroutine(_playingCoro);
        _playingCoro = StartCoroutine(PlayingVideo(level));
    }

    IEnumerator PlayingVideo(LevelData level)
    {
        videoPlayer.clip = level.video;
        videoPlayer.Prepare();

        // 等待準備或逾時或被跳過
        float t = 0f;
        while (!skipVideo && !videoPlayer.isPrepared && t < prepareTimeout)
        {
            t += Time.unscaledDeltaTime;
            yield return null;
        }

        if (skipVideo || !videoPlayer.isPrepared)
        {
            if (!skipVideo)
                Debug.LogWarning("[LevelVideoUI] Video prepare timeout, skipping video.");

            EndVideo();
            StartScene(level);
            yield break;
        }

        videoPlayer.Play();


        float startWait = 0f;
        while (!skipVideo && !videoPlayer.isPlaying && startWait < 1f)
        {
            startWait += Time.unscaledDeltaTime;
            yield return null;
        }


        while (!skipVideo && videoPlayer.clip != null &&
               videoPlayer.time < videoPlayer.clip.length - endEarlyPadding)
        {
            yield return null;
        }

        EndVideo();
        StartScene(level);
    }

    public void OnSkipClicked()
    {
        if (sceneStarted) return;      // 已經跳走就忽略
        skipVideo = true;          // 通知協程提前結束
    }

    void StartScene(LevelData level)
    {
        if (sceneStarted) return;
        sceneStarted = true;

        if (skipButton) skipButton.gameObject.SetActive(false);

        GameManager.Instance.StartGameScene(level);
    }

    void EndVideo()
    {
        if (videoPlayer != null)
        {
            if (videoPlayer.isPlaying) videoPlayer.Stop();
            videoPlayer.clip = null;
        }
        if (_playingCoro != null)
        {
            StopCoroutine(_playingCoro);
            _playingCoro = null;
        }
    }
}
