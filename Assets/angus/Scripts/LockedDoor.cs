using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : InteractableItem
{
    public string requiredKeyID;
    public bool unlocked = false;

    public Animator anime;

    public Hint hint;

    public bool CanInteract(PlayerInteraction player)
    {
        var item = player.GetComponent<PlayerItem>().currentItem as Key;
        return item != null && item.keyID == requiredKeyID;
    }

    public void Unlock(PlayerInteraction player)
    {
        unlocked = true;
        player.GetComponent<PlayerItem>().RemoveItem();
        Debug.Log("門已解鎖！");
    }
    public override void Interact(PlayerInteraction player)
    {
        if (CanInteract(player))
        {
            Unlock(player);
            return;
        }
        else
        {
            HintManager.Instance.ShowHint(hint);
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
