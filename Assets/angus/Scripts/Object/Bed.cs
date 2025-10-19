using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : InteractableItem
{
    public AudioSource alarmBell;

    public event Action<bool> playerSleep;

    public float sleepDelayTime;

    public bool interactOnce;
    public override void Interact(PlayerInteraction player)
    {
        if(interactOnce)
        {
            SleepAndWakeUp(player);
            interactOnce = false;
        }
    }

    public void SleepAndWakeUp(PlayerInteraction player)
    {
        playerSleep?.Invoke(true);
        player.GetComponent<PlayerInteractAction>().ToggleInteracting(true);
        StartCoroutine(SleepAndWakeUpCoroutine(player));
    }
    
    IEnumerator SleepAndWakeUpCoroutine(PlayerInteraction player)
    {
        yield return new WaitForSeconds(sleepDelayTime);
        playerSleep?.Invoke(false);
        yield return new WaitForSeconds(1f);
        player.GetComponent<PlayerInteractAction>().ToggleInteracting(false);
    }
}
