using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarButton : InteractableItem
{
    public bool triggerOnce;

    public override void Interact(PlayerInteraction player)
    {
        throw new System.NotImplementedException();
    }

    public override void InteractSound()
    {
        throw new System.NotImplementedException();
    }

    void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerTalk>();
        if (player != null)
        {
            if (dialogue != null)
            {
                player.Talk(dialogue);
            }
            if (triggerOnce)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
