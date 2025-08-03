using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HintTrigger : MonoBehaviour
{
    public Hint hint;   
    void OnTriggerEnter(Collider other)
    {
        HintManager.Instance.ShowHint(hint.hintText);
    }
}
