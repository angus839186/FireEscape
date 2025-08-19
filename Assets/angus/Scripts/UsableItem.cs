using UnityEngine;

public abstract class UsableItem : MonoBehaviour, Iusable, IInteractable
{
    public Item itemData;

    public void Interact(PlayerInteraction player)
    {
        player.GetComponent<PlayerItem>().GetItem(this);
    }

    public abstract void Use();
}
