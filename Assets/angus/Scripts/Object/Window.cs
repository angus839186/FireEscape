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
        if (isTransitioning) return;
        if (windowbreak)
        {
            StartCoroutine(WindowTransitionCoroutine(player));
            Debug.Log("get off the car");
        }
        else
        {
            if (CheckRequirements(player, out var inv))
            {
                windowbreak = true;
            }
            else
            {
                ShowPlayerTalk(player, dialogue);
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
