using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHint", menuName = "Game/Hint")]
public class Hint : ScriptableObject
{
    public string hintText;
    public float HintDuration;
}
