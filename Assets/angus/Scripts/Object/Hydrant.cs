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
            HintUI.Instance.ShowHint(hint);
        }
        else
        {
            playerAct.ToggleNozzle(false);
        }
        this.HighLight(false);
    }
}
