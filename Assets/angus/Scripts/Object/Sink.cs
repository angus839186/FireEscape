using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : InteractableItem
{
    public override void Interact(PlayerInteraction player)
    {
        if(CheckRequirements(player, out var inv))
        {
            player.GetComponent<PlayerAction>().ragisWet = true;
            player.GetComponent<PlayerAction>().rag.SetActive(true);
            canInteract = false;
        }
    }
}
