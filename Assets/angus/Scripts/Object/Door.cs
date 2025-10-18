using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableItem
{
    public Animator anime;

    public override void Interact(PlayerInteraction player)
    {
        TryOpen();
    }
    public void TryOpen()
    {
        if (anime == null)
        {
            Debug.LogWarning($"{name}: 沒有 Animator 無法開門動畫");
            return;
        }
        anime.SetTrigger("DoorTrigger");
    }
}
