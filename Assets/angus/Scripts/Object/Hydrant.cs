using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Hydrant : InteractableItem
{
    public override void Interact(PlayerInteraction player)
    {
        AddItem(player);
        EventAfterInteract.Invoke();
        CloseObjectHighLight();
    }

    public override void InteractSound()
    {

    }
}
