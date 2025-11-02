using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractableHint : InteractableItem
{
    public bool TriggerOnce;

    public override void Interact(PlayerInteraction player)
    {
        HintUI.Instance.ShowHint(hint);
        if (TriggerOnce)
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
