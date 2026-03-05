using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : InteractableItem
{
    [SerializeField] private bool win;

    public override void Interact(PlayerInteraction player)
    {
    }

    void OnTriggerEnter(Collider other)
    {
        var Player = other.GetComponent<PlayerController>();
        if(Player != null)
        {
            GameManager.Instance.LevelEnd(win);
        }
    }
}
