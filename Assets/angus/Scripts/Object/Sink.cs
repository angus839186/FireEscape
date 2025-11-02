using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : InteractableItem
{
    public AudioClip waterClip;
    public override void Interact(PlayerInteraction player)
    {
        if (canInteract)
        {
            if (CheckRequirements(player, out var inv))
            {
                player.GetComponent<PlayerAction>().ragisWet = true;
                player.GetComponent<PlayerAction>().rag.SetActive(true);
                AudioManager.Instance.PlaySound(waterClip);
                canInteract = false;
            }
        }
    }
}
