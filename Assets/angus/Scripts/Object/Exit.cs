using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : InteractableItem
{
    [SerializeField] private bool win;

    public override void Interact(PlayerInteraction player)
    {
    }

    public override void InteractSound()
    {
        throw new System.NotImplementedException();
    }

    void OnTriggerEnter(Collider other)
    {
        var Player = other.GetComponent<PlayerController>();
        if(Player != null)
        {
            ShowHint(hint);
            GameManager.Instance.LevelEnd(win);
        }
    }
}
