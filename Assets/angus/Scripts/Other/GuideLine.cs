using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GuideLine : MonoBehaviour
{
    [Header("連線對象")]
    [SerializeField] public Transform player;
    [SerializeField] public Transform target;

    [Header("地板設定")]
    [SerializeField] public LayerMask groundMask;
    [SerializeField] public float raycastHeight = 2f;
    [SerializeField] private float raycastDistance = 6f;
    [SerializeField] public float groundYOffset = 0.05f;

    [Header("箭頭貼圖設定")]
    [SerializeField] public float arrowWorldLength = 0.5f;
    [SerializeField] public float minRepeatCount = 1f;
    [SerializeField] public float scrollSpeed = 1f;
    [SerializeField] private float minVisibleLength = 0.15f;

    private LineRenderer line;
    private Material lineMatInstance;
    private Vector2 baseTiling = Vector2.one;

    void Awake()
    {
        line = GetComponent<LineRenderer>();

        line.positionCount = 2;
        line.enabled = false;

        if (line.material != null)
        {
            lineMatInstance = new Material(line.material);
            line.material = lineMatInstance;
            baseTiling = lineMatInstance.mainTextureScale;
        }
    }

    private void OnDestroy()
    {
        if (lineMatInstance != null)
        {
            Destroy(lineMatInstance);
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

    private void LateUpdate()
    {
        if (player == null || target == null)
        {
            line.enabled = false;
            return;
        }

        Vector3 startPoint = GetGroundPoint(player.position);
        Vector3 endPoint = GetGroundPoint(target.position);
        float lineLength = Vector3.Distance(startPoint, endPoint);

        if (lineLength < minVisibleLength)
        {
            line.enabled = false;
            return;
        }

        line.enabled = true;
        line.SetPosition(0, startPoint);
        line.SetPosition(1, endPoint);

        UpdateTexture(lineLength);
    }

    private void UpdateTexture(float lineLength)
    {
        if (lineMatInstance == null)
        {
            return;
        }

        float safeArrowLength = Mathf.Max(0.01f, arrowWorldLength);
        float repeatCount = Mathf.Max(minRepeatCount, lineLength / safeArrowLength);

        Vector2 tiling = baseTiling;
        tiling.x = repeatCount;
        lineMatInstance.mainTextureScale = tiling;

        Vector2 offset = lineMatInstance.mainTextureOffset;
        offset.x -= scrollSpeed * Time.deltaTime;
        lineMatInstance.mainTextureOffset = offset;
    }



    private Vector3 GetGroundPoint(Vector3 worldPosition)
    {
        Vector3 rayOrigin = worldPosition + Vector3.up * raycastHeight;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, raycastDistance, groundMask, QueryTriggerInteraction.Ignore))
        {
            return hit.point + Vector3.up * groundYOffset;
        }

        return worldPosition + Vector3.up * groundYOffset;
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
