using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extinguisher : InteractableItem
{
    public override void Interact(PlayerInteraction player)
    {
        base.Interact(player);
        AddItem(player);
        CloseObjectHighLight();
    }

    public override void InteractSound()
    {

    }
}
