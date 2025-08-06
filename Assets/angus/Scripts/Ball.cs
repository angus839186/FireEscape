using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("這裡有顆球");
    }
}
