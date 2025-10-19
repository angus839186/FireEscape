using UnityEngine;


public abstract class UsableItem : MonoBehaviour, IUsable, IInteractable
{
    public ItemData itemData;

    public virtual void Interact(PlayerInteraction player)
    {
        player.GetComponent<PlayerItem>().AddItem(this);
        this.gameObject.SetActive(false);
    }
}
