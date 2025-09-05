using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "HintSettings/HintData")]
public class Hint : ScriptableObject
{
    public string hintText;
    public float HintDuration;
}
