using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HintTrigger : MonoBehaviour
{
    public Hint hint;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HintManager.Instance.ShowHint(hint);
        }
    }
}
