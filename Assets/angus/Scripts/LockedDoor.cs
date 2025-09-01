using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : InteractableItem
{
    public string requiredKeyID;
    public bool unlocked = false;

    public Animator anime;

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
        }

        if (unlocked)
        {
            anime.SetTrigger("DoorTrigger");
        }
        else
        {
            Debug.Log("門鎖住了");
        }

    }
}
