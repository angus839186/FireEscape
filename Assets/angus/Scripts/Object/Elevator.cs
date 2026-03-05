using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : InteractableItem
{
    [SerializeField] private Animator anime;
    public override void Interact(PlayerInteraction player)
    {
        anime.SetBool("Toggle", !anime.GetBool("Toggle"));
    }
}
