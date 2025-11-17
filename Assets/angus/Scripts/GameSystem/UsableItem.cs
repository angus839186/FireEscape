using System;
using UnityEngine;


public abstract class UsableItem : MonoBehaviour, IUsable, IInteractable
{
    public Hint hintData;
    public ItemData itemData;

    public bool canPickUp = true;

    public event Action OnItemPicked;

    public virtual void Interact(PlayerInteraction player)
    {
        if (canPickUp)
        {
            player.GetComponent<PlayerItem>().AddItem(this.itemData);
            if (hintData != null)
            {
                HintUI.Instance.ShowHint(this.hintData);
            }
            this.gameObject.SetActive(false);
            OnItemPicked?.Invoke();
        }
    }
}
