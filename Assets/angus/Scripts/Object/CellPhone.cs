using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPhone : InteractableItem
{
    public AudioSource phoneSound;

    public DoorTrigger bedroomDoor;
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
            HintUI.Instance.ShowHint(hint);
        }
        phoneSound.Stop();
        bedroomDoor.locked = false;
        canInteract = false;
    }
}
