using System.Collections;
using UnityEngine;

public class ElevatorInteractable : InteractableItem
{
    [Header("Elevator Settings")]
    public Animator elevatorAnimator;
    public AudioClip moveSound;
    private bool isOpen = false;

    public override void Interact(PlayerInteraction player)
    {
        // 必須符合你的 canInteract 檢查機制
        if (canInteract)
        {
            // 呼叫基底類別的需求檢查 (即使沒有道具要求也要跑，否則系統會報錯)
            if (CheckRequirements(player, out var inv))
            {
                // 執行開關門
                ToggleElevator();
            }
            else
            {
                // 如果檢查沒過，顯示提示文字並觸發高亮 (這就是第 45 行可能在找的東西)
                if (HintUI.Instance != null && hint != null)
                {
                    HintUI.Instance.ShowHint(hint);
                }

                if (NextHighLightObject != null)
                {
                    var interactable = NextHighLightObject.GetComponent<IInteractable>();
                    if (interactable != null) interactable.HighLight(true);
                }
            }
        }
    }

    private void ToggleElevator()
    {
        if (elevatorAnimator == null) return;
        
        // 防止動畫重疊
        if (elevatorAnimator.IsInTransition(0)) return;

        isOpen = !isOpen;
        
        // 播放動畫
        elevatorAnimator.SetTrigger(isOpen ? "Open" : "Close");

        // 播放音效
        if (moveSound != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(moveSound);
        }
        
        Debug.Log("電梯門切換為：" + (isOpen ? "開啟" : "關閉"));
    }
}