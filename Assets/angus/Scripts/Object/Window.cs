using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Window : InteractableItem
{

    public override void InteractSound()
    {

    }

    public override void Interact(PlayerInteraction player)
    {
        if (CheckRequirements(player, out var inv))
        {
            EventAfterInteract.Invoke();
        }
        else
        {
            ShowPlayerTalk(player, dialogue);
        }
    }
}
