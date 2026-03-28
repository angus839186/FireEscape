using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Window : InteractableItem
{
    public bool windowbreak;

    private bool isTransitioning;
    public Transform OutsidePoint;
    public override void InteractSound()
    {

    }

    public override void Interact(PlayerInteraction player)
    {
        if (!player.TryGetComponent(out PlayerItem playerItem)) return;
        if (!player.TryGetComponent(out PlayerAction playerAction)) return;
        ItemData heldItem = playerItem.HeldItem;
        if (isTransitioning) return;
        if (windowbreak)
        {
            StartCoroutine(WindowTransitionCoroutine(player));
        }
        else
        {
            switch (heldItem.actionType)
            {
                case ItemActionType.WindowBreaker:
                    playerAction.TryUseWindowBreaker();
                    windowbreak = true;
                    break;

                default:
                    ShowPlayerTalk(player, dialogue);
                    break;
            }
        }
    }
    private IEnumerator WindowTransitionCoroutine(PlayerInteraction player)
    {
        isTransitioning = true;
        TransitionUI transitionUI = FindFirstObjectByType<TransitionUI>();
        PlayerAction playerAction = player.GetComponent<PlayerAction>();
        PlayerController playerController = player.GetComponent<PlayerController>();

        if (transitionUI != null)
        {
            transitionUI.TransitionImage(true);
        }

        if (playerAction != null)
        {
            playerAction.ToggleFreeze(true);
        }

        yield return new WaitForSeconds(transitionDelayTime);

        if (playerController != null)
        {
            playerController.TeleportTo(OutsidePoint);
        }

        if (playerAction != null)
        {
            playerAction.ToggleInCar(false);
        }

        if (transitionUI != null)
        {
            transitionUI.TransitionImage(false);
        }

        if (playerAction != null)
        {
            playerAction.ToggleFreeze(false);
        }
        EventAfterInteract.Invoke();
        NextHighLight();
        isTransitioning = false;
    }
}
