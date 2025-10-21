using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPhone : InteractableItem
{
    public AudioSource phoneSound;

    public Door bedroomDoor;

    public event Action GetCallAction;
    public override void Interact(PlayerInteraction player)
    {
        if (canInteract)
        {
            GetCall();
        }
    }
    public void StartRing()
    {
        phoneSound.Play();
        canInteract = true;
    }

    public void GetCall()
    {
        if (hint != null)
        {
            HintManager.Instance.ShowHint(hint);
        }
        phoneSound.Stop();
        bedroomDoor.locked = false;
        canInteract = false;
    }
}
