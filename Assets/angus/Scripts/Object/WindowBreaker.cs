using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WindowBreaker : InteractableItem
{

    public override void InteractSound()
    {

    }

    public override void Interact(PlayerInteraction player)
    {
        if(canInteract)
        {
            AddItem(player);
            ShowPlayerTalk(player, dialogue);
        }
    }
}
