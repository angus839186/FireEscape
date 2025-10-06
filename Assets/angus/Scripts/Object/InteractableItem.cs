using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableItem : MonoBehaviour, IInteractable
{
    public abstract void Interact(PlayerInteraction player);
}
