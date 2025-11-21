using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBox : InteractableItem
{
    public override void Interact(PlayerInteraction player)
    {
        if (canInteract)
        {
            if (airCollider != null)
            {
                airCollider.SetActive(false);
            }
            if (dialogue != null)
            {
                player.GetComponent<PlayerTalk>().Talk(dialogue);
            }
            canInteract = false;
        }
    }
}
