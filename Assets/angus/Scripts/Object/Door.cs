using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableItem
{
    public Animator anime;
    public override void Interact(PlayerInteraction player)
    {
        anime.SetTrigger("DoorTrigger");
    }
}
