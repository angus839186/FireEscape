using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CellPhone : InteractableItem
{

    public override void InteractSound()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact(PlayerInteraction player)
    {
        base.Interact(player);
        AddItem(player);
        CloseObjectHighLight();
        EventAfterInteract.Invoke();
    }
}
