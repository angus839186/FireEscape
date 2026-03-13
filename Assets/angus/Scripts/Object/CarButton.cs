using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarButton : InteractableItem
{
    public bool triggerOnce;

    public override void Interact(PlayerInteraction player)
    {
        base.Interact(player);
        if (TriggerOnce)
        {
            CloseInteract();
            EventAfterInteract.Invoke();
        }
    }

    public override void InteractSound()
    {
        
    }

}
