using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HintTrigger : MonoBehaviour
{
    public bool TriggerOnce;
    public Hint hint;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HintUI.Instance.ShowHint(hint);
            TriggerOnce = false;
            if (TriggerOnce)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
