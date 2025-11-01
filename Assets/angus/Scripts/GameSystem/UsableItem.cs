using UnityEngine;


public abstract class UsableItem : MonoBehaviour, IUsable, IInteractable
{
    public Hint hintData;
    public ItemData itemData;

    public virtual void Interact(PlayerInteraction player)
    {
        player.GetComponent<PlayerItem>().AddItem(this.itemData);
        if(hintData != null)
        {
            HintUI.Instance.ShowHint(this.hintData);
        }
        this.gameObject.SetActive(false);
    }
}
