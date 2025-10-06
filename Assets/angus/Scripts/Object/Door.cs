using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableItem
{
    [SerializeField, Tooltip("需要的鑰匙ID:留空代表無需鑰匙")]
    private string requiredKeyID;

    [SerializeField, Tooltip("是否已解鎖（可由存檔系統寫回）")]
    private bool unlocked = false;

    public Animator anime;

    public bool IsUnlocked => unlocked;
    public string RequiredKeyID => requiredKeyID;

    [SerializeField] private Hint hint;

    public void Awake()
    {
        if (string.IsNullOrEmpty(requiredKeyID))
        {
            unlocked = true;
        }
    }
    public override void Interact(PlayerInteraction player)
    {
        if (unlocked)
        {
            TryOpen();
            return;
        }

        if (player != null && player.TryGetComponent<PlayerItem>(out var playerItem))
        {
            if (!string.IsNullOrEmpty(requiredKeyID) && TryConsumeKey(playerItem, requiredKeyID))
            {
                unlocked = true;
                Debug.Log("門已解鎖");
                TryOpen();
                return;
            }
        }

        if (hint != null)
        {
            HintManager.Instance.ShowHint(hint);
        }

    }

    private bool TryConsumeKey(PlayerItem playerItem, string keyId)
    {
        if (playerItem.CheckItem(out var item) && item is Key key && key.keyID == keyId)
        {
            playerItem.RemoveItem();
            return true;
        }
        return false;
    }
    public void TryOpen()
    {
        if (anime == null)
        {
            Debug.LogWarning($"{name}: 沒有 Animator 無法開門動畫");
            return;
        }
        anime.SetTrigger("DoorTrigger");
    }
}
