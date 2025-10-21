using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : InteractableItem
{
    public AudioSource alarmBell;
    public CellPhone cellPhone;

    public event Action<bool> playerSleep;

    public float wakeUpDelayTime;
    public float warningDelayTime;


    public override void Interact(PlayerInteraction player)
    {
        if(canInteract)
        {
            SleepAndWakeUp(player);
            canInteract = false;
        }
    }

    public void SleepAndWakeUp(PlayerInteraction player)
    {
        StartCoroutine(SleepAndWakeUpCoroutine(player));
    }
    
    IEnumerator SleepAndWakeUpCoroutine(PlayerInteraction player)
    {
        playerSleep?.Invoke(true);
        player.GetComponent<PlayerAction>().ToggleInteracting(true);
        yield return new WaitForSeconds(warningDelayTime);
        alarmBell.Play();
        yield return new WaitForSeconds(wakeUpDelayTime);
        playerSleep?.Invoke(false);
        cellPhone.StartRing();
        player.GetComponent<PlayerAction>().ToggleInteracting(false);
    }
}
