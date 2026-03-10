using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class DoorHandle : InteractableItem
{
    public DoorTrigger targetDoorTrigger;
    public bool isHot;

    public GameObject doorHandleText;
    public DialogueData hotDialogue;

    public override void Interact(PlayerInteraction player)
    {
        if (!canInteract) return;

        ShowHint(hint);
        ToggleAirCollider(false);

        if (isHot)
        {
            ShowPlayerTalk(player, hotDialogue);
        }
        else
        {
            ShowPlayerTalk(player, dialogue);
        }

        if (targetDoorTrigger != null)
        {
            targetDoorTrigger.UnlockInteraction();
        }

        if(doorHandleText != null)
        {
            doorHandleText.SetActive(false);
        }

        NextHighLight();
        canInteract = false;
        CloseInteract();

    }

    public override void InteractSound()
    {

    }
}
