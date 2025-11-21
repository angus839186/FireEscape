using System;
using UnityEngine;


public abstract class UsableItem : MonoBehaviour, IUsable, IInteractable
{
    public Hint hintData;
    public ItemData itemData;

    public GameObject airCollider;

    public bool canPickUp = true;

    public virtual void Interact(PlayerInteraction player)
    {
        if (canPickUp)
        {
            player.GetComponent<PlayerItem>().AddItem(this.itemData);
            if (hintData != null)
            {
                HintUI.Instance.ShowHint(this.hintData);
            }
            if(airCollider!= null)
            {
                airCollider.SetActive(false);
            }
            this.gameObject.SetActive(false);
        }
    }
}
