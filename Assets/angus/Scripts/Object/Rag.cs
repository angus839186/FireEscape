using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rag : InteractableItem
{
    public override void Interact(PlayerInteraction player)
    {
        base.Interact(player);
        AddItem(player);
    }

    public override void InteractSound()
    {

    }
}
