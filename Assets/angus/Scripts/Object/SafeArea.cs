using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : InteractableItem
{
    [SerializeField] GameObject area;
    [SerializeField] DialogueData ExitAreaDialogue;

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
        PlayerAction playerAction = Player.GetComponent<PlayerAction>();
        PlayerInteraction playerInteraction = Player.GetComponent<PlayerInteraction>();
        if(Player != null)
        {
            ShowPlayerTalk(playerInteraction,dialogue);
            playerAction.canUsePhone = true;
            area.SetActive(false);
            EventAfterInteract.Invoke();
        }
    }
    void OnTriggerExit(Collider other)
    {
        var Player = other.GetComponent<PlayerController>();
        PlayerAction playerAction = Player.GetComponent<PlayerAction>();
        PlayerInteraction playerInteraction = Player.GetComponent<PlayerInteraction>();
        if(Player != null)
        {
            ShowPlayerTalk(playerInteraction,ExitAreaDialogue);
            playerAction.canUsePhone = false;
            area.SetActive(true);
        }
    }
}
