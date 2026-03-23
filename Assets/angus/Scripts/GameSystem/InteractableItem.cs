using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class InteractableItem : MonoBehaviour, IInteractable
{

    [Header("互動設定")]
    [SerializeField] private ItemData requiredItem;

    [Header("預設提示")]
    public Hint hint;

    [Header("預設自白")]
    public DialogueData dialogue;

    [Header("空氣牆")]
    public GameObject airCollider;

    [Header("下一個高亮物件")]
    public GameObject NextHighLightObject;

    [Header("可撿取道具")]
    public ItemData itemToAdd;

    [Header("轉場過渡秒數")]
    public float transitionDelayTime;

    [Header("互動後事件")]
    public UnityEvent EventAfterInteract;

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
            ShowPlayerTalk(player, dialogue);
            NextHighLight();
            EventAfterInteract.Invoke();
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
    public void ShowPlayerTalk(PlayerInteraction player, DialogueData customDialogue)
    {
        if (customDialogue != null)
        {
            player.GetComponent<PlayerTalk>().Talk(customDialogue);
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
    }

    public void CloseObjectHighLight()
    {
        HighLightObject highLightObject = GetComponent<HighLightObject>();
        if (highLightObject != null)
        {
            highLightObject.HighLight(false);
        }
    }

    public void CloseInteract()
    {
        GetComponent<Collider>().enabled = false;
    }

    public void TriggerTransition(PlayerInteraction player)
    {
        StartCoroutine(TransitionCoroutine(player));
    }

    IEnumerator TransitionCoroutine(PlayerInteraction player)
    {
        TransitionUI transitionUI = FindFirstObjectByType<TransitionUI>();
        if(transitionUI != null)
        {
            transitionUI.TransitionImage(true);
        }
        player.GetComponent<PlayerAction>().ToggleFreeze(true);
        yield return new WaitForSeconds(transitionDelayTime);
        if(transitionUI != null)
        {
            transitionUI.TransitionImage(false);
        }
        player.GetComponent<PlayerAction>().ToggleFreeze(false);
    }

    public abstract void InteractSound();
}

