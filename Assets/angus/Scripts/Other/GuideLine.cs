using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GuideLine : MonoBehaviour
{
    [Header("連線對象")]
    public Transform player;
    public Transform target;

    [Header("地板設定")]
    public LayerMask groundMask;          // 指定地板的 Layer
    public float raycastHeight = 2f;      // 往下打 Ray 的起點高度（相對 target）
    public float groundYOffset = 0.05f;   // 讓線稍微浮在地面上

    [Header("箭頭貼圖設定")]
    public float arrowWorldLength = 0.5f;
    public float minRepeatCount = 1f;
    public float scrollSpeed = 1f;

    private LineRenderer line;
    private Material lineMatInstance;
    private Vector2 baseTiling = Vector2.one;

    void Awake()
    {
        line = GetComponent<LineRenderer>();

        if (line.material != null)
        {
            lineMatInstance = new Material(line.material);
            line.material = lineMatInstance;
            baseTiling = lineMatInstance.mainTextureScale;
        }
    }

    void Update()
    {
        if (player == null || target == null)
        {
            line.enabled = false;
            return;
        }

        // --------- 計算 start / end 在地面上的位置 ---------
        Vector3 start = player.position;          // 你說這個不用額外計算
        Vector3 targetWorldPos = target.position; // 物件實際位置（可能在桌上）

        // 從 target 上方往下打 Ray 找地板
        Vector3 rayOrigin = targetWorldPos + Vector3.up * raycastHeight;
        Vector3 endOnGround = targetWorldPos;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, raycastHeight * 3f, groundMask))
        {
            endOnGround = hit.point;
        }

        // 讓線「貼在地面上」：把 start / end 的 y 都拉到同一個地板高度
        float groundY = endOnGround.y + groundYOffset;
        start.y = groundY;
        endOnGround.y = groundY;

        // --------- 畫線 ---------
        line.enabled = true;
        line.positionCount = 2;
        line.SetPosition(0, start);       // 起點：玩家在地面上的位置
        line.SetPosition(1, endOnGround); // 終點：Target 在地面上的投影位置

        // --------- 箭頭貼圖 Tiling + 滾動 ---------
        if (lineMatInstance != null)
        {
            float length = Vector3.Distance(start, endOnGround);

            float repeatCount = Mathf.Max(minRepeatCount, length / arrowWorldLength);
            Vector2 tiling = baseTiling;
            tiling.x = repeatCount;
            lineMatInstance.mainTextureScale = tiling;

            Vector2 offset = lineMatInstance.mainTextureOffset;
            offset.x -= scrollSpeed * Time.deltaTime;
            lineMatInstance.mainTextureOffset = offset;
        }
    }

    public void StopGuide()
    {
        target = null;
        line.enabled = false;
    }

    public void StartGuide(Transform _target)
    {
        target = _target;
        line.enabled = true;
    }
}
