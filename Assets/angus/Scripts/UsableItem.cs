using UnityEngine;

public abstract class UsableItem : MonoBehaviour, Iusable, IInteractable
{
    public ItemData itemData;

    public abstract void Interact(PlayerInteraction player);

    public abstract void Use();
}
