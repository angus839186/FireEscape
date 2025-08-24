using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    public string keyID;
    public Hint hint;

    public void Interact(PlayerInteraction player)
    {
        
    }
}
