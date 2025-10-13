using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : InteractableItem
{
    public override void Interact(PlayerInteraction player)
    {
        if (!CheckRequirements(player, out var inv))
        {
            Debug.Log("需要滅火器才能撲滅火焰。");
        }
        else
        {
            Debug.Log("滅火!!");
        }

    }

    void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            Debug.Log("被火燙到了");
            player.TakeDamage(1);
        }
    }
}
