using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    public UnityEvent eventToTrigger;

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerInteraction>();
        if (player != null)
        {
            InvokeEvent();
        }
    }

    public void InvokeEvent()
    {
        eventToTrigger.Invoke();
    }
}
