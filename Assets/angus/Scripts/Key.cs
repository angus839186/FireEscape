using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : UsableItem
{
    public string keyID; // 用於對應門的編號

    public override void Interact(PlayerInteraction player)
    {
        player.GetComponent<PlayerItem>().GetItem(this);
    }

    public override void Use()
    {

    }
}
