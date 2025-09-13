using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Game/Level")]
public class LevelData : ScriptableObject
{
    public string sceneName;
    public Vector3 defaultSpawnPos;
    public Vector3 defaultSpawnEuler;
}
