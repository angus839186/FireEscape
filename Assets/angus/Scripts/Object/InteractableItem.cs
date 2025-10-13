using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InteractableItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData requiredItem;
    protected bool CheckRequirements(PlayerInteraction player, out PlayerItem playerItem)
    {
        playerItem = null;
        if (player == null) return false;
        if (!player.TryGetComponent(out playerItem)) return false;


        if (!playerItem.HasItem(requiredItem))
            return false;
        return true;
    }
    public abstract void Interact(PlayerInteraction player);
}
