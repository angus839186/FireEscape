using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableItem
{
    public string requiredKeyID;
    private bool unlocked = false;

    public bool CanInteract(PlayerInteraction player)
    {
        var item = player.GetComponent<PlayerItem>().currentItem as Key;
        return item != null && item.keyID == requiredKeyID;
    }
    public override void Interact(PlayerInteraction player)
    {
        if (CanInteract(player))
        {
            unlocked = true;
            Debug.Log("門已解鎖！");
            // 加入動畫或開門行為
        }
        else
        {
            Debug.Log("這扇門上鎖了。");
        }

    }
}
