using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class InteractableItem : MonoBehaviour, IInteractable
{

    [Header("互動設定")]
    [SerializeField] private ItemData requiredItem;

    [Header("消防員提示")]
    public Hint hint;

    [Header("玩家自白")]
    public DialogueData dialogue;

    [Header("空氣牆")]
    public GameObject airCollider;

    [Header("下一個高亮物件")]
    public GameObject NextHighLightObject;

    [Header("可撿取道具")]
    public ItemData itemToAdd;

    public bool canInteract;

    public bool TriggerOnce;

    protected bool CheckRequirements(PlayerInteraction player, out PlayerItem playerItem)
    {
        playerItem = null;
        if (player == null) return false;
        if (!player.TryGetComponent(out playerItem)) return false;

        if (!playerItem.HasItem(requiredItem))
            return false;
        return true;
    }

    public virtual void Interact(PlayerInteraction player)
    {
        if (canInteract)
        {
            ShowHint(hint);
            ToggleAirCollider(false);
            ShowPlayerTalk(player);
            NextHighLight();
            canInteract = false;
        }
    }
    public void ShowHint(Hint hint)
    {
        if (hint != null)
        {
            HintUI.Instance.ShowHint(hint);
        }
    }
    public void ToggleAirCollider(bool toggle)
    {
        if (airCollider != null)
        {
            airCollider.SetActive(toggle);
        }
    }
    public void ShowPlayerTalk(PlayerInteraction player)
    {
        if (dialogue != null)
        {
            player.GetComponent<PlayerTalk>().Talk(dialogue);
        }
    }
    public void NextHighLight()
    {
        if (NextHighLightObject != null)
        {
            NextHighLightObject.GetComponent<HighLightObject>().HighLight(true);
        }
    }
    public void AddItem(PlayerInteraction player)
    {
        if (player.TryGetComponent(out PlayerItem playerItem))
        {
            playerItem.AddItem(itemToAdd);
        }
        this.gameObject.SetActive(false);
    }

    public void CloseObjectHighLight()
    {
        HighLightObject highLightObject = GetComponent<HighLightObject>();
        if(highLightObject != null)
        {
            highLightObject.HighLight(false);
        }
    }

    public abstract void InteractSound();
}

