using UnityEngine;

public class ElevatorControl : MonoBehaviour
{
    public float interactDistance = 4.0f; 
    public Animator elevatorAnimator;     
    private bool isOpen = false;          

    void Update()
    {
        // 偵測滑鼠左鍵點擊
        if (Input.GetMouseButtonDown(0))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        // 從相機正中心射出射線
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            // 確保射線打到的是掛載此腳本的電梯物件
            if (hit.collider.gameObject == this.gameObject)
            {
                ToggleDoor();
            }
        }
    }

    void ToggleDoor()
    {
        // 檢查當前是否正在播放過渡動畫，防止連續點擊出錯
        if (elevatorAnimator.IsInTransition(0)) return;

        if (!isOpen)
        {
            elevatorAnimator.SetTrigger("Open");
            isOpen = true;
            Debug.Log("門開啟 - 維持狀態中");
        }
        else
        {
            elevatorAnimator.SetTrigger("Close");
            isOpen = false;
            Debug.Log("門關閉 - 維持狀態中");
        }
    }
}