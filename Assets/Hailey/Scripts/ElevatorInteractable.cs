using UnityEngine;

// 繼承你的互動基底類別
public class ElevatorInteractable : InteractableItem
{
    [Header("Elevator Settings")]
    public Animator elevatorAnimator;
    private bool isOpen = false;

    public AudioClip moveSound;

    // 實作你系統要求的 Interact 函式
    public override void Interact(PlayerInteraction player)
    {
        ToggleElevator();
    }

    private void ToggleElevator()
    {
        // 檢查是否正在播放動畫中，避免重複點擊
        if (elevatorAnimator.IsInTransition(0)) return;

        isOpen = !isOpen;

        if (isOpen)
        {
            elevatorAnimator.SetTrigger("Open");
        }
        else
        {
            elevatorAnimator.SetTrigger("Close");
        }

        // 播放聲音（如果你有 AudioManager）
        if (moveSound != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(moveSound);
        }
    }
}