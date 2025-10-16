using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Game/Level")]
public class LevelData : ScriptableObject
{
    public string sceneName;

    public VideoClip video;
    public Vector3 defaultSpawnPos;
    public Vector3 defaultSpawnEuler;
}
