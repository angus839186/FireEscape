using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HintTrigger : InteractableItem
{

    public override void InteractSound()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact(PlayerInteraction player)
    {
        base.Interact(player);
    }

    void OnTriggerEnter(Collider other)
    {
        if (canInteract)
        {
            if (other.CompareTag("Player"))
            {
                ShowHint(hint);
                NextHighLight();
                if (TriggerOnce)
                {
                    GetComponent<BoxCollider>().enabled = false;
                    canInteract = false;
                }
            }
        }
    }
}
