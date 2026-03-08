using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Hydrant : InteractableItem
{
    public override void Interact(PlayerInteraction player)
    {
        PlayerAction playerAct = player.GetComponent<PlayerAction>();
        if(!playerAct.holdNozzle)
        {
            playerAct.ToggleNozzle(true);
            ShowHint(hint);
        }
        else
        {
            playerAct.ToggleNozzle(false);
        }
        CloseObjectHighLight();
    }

    public override void InteractSound()
    {
        
    }
}
