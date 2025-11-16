using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPhone : InteractableItem
{
    public AudioSource phoneSound;

    public DoorTrigger Door;

    public GameObject airCollider;

    public Light alarmLight;
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
        if (Door != null)
        {
            Door.locked = false;
        }
        if (airCollider != null)
        {
            airCollider.SetActive(false);
        }
        if(alarmLight != null)
        {
            alarmLight.enabled = true;
        }
        canInteract = false;
    }
}
