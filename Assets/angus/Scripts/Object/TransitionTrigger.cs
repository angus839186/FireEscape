using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionTrigger : InteractableItem
{
    public AudioSource alarmBell;
    public CellPhone cellPhone;

    public event Action<bool> OnTransition;

    public float wakeUpDelayTime;
    public float warningDelayTime;


    public override void Interact(PlayerInteraction player)
    {
        if (canInteract)
        {
            Transition(player);
            canInteract = false;
        }
    }

    public void Transition(PlayerInteraction player)
    {
        StartCoroutine(TransitionCoroutine(player));
    }

    IEnumerator TransitionCoroutine(PlayerInteraction player)
    {
        OnTransition?.Invoke(true);
        player.GetComponent<PlayerAction>().ToggleFreeze(true);
        yield return new WaitForSeconds(warningDelayTime);
        if (alarmBell != null)
        {
            alarmBell.Play();
        }
        yield return new WaitForSeconds(wakeUpDelayTime);
        OnTransition?.Invoke(false);
        cellPhone.StartRing();
        player.GetComponent<PlayerAction>().ToggleFreeze(false);
    }
}
