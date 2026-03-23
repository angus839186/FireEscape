using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : InteractableItem
{
    public AudioSource audioSource;
    public override void Interact(PlayerInteraction player)
    {
        if (canInteract)
        {
            if (CheckRequirements(player, out var inv))
            {
                player.GetComponent<PlayerAction>().canUseRag = true;
                ShowPlayerTalk(player, dialogue);
                InteractSound();
                CloseObjectHighLight();
                canInteract = false;
            }
        }
    }

    public override void InteractSound()
    {
        audioSource.Play();
    }
}
