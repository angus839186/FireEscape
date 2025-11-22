using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GuideLine : MonoBehaviour
{
    [Header("連線對象")]
    public Transform player;
    public Transform target;

    [Header("顯示設定")]
    public float yOffset = 0.1f;
    public bool flattenToGround = false;



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

        line.enabled = true;

        Vector3 start = player.position;
        Vector3 end = target.position;

        if (flattenToGround)
        {
            start.y = 0f;
            end.y = 0f;
        }

        start.y += yOffset;
        end.y += yOffset;

        line.positionCount = 2;
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        if (lineMatInstance != null)
        {
            float length = Vector3.Distance(start, end);

            float repeatCount = Mathf.Max(minRepeatCount, length / arrowWorldLength);
            Vector2 tiling = baseTiling;
            tiling.x = repeatCount;
            lineMatInstance.mainTextureScale = tiling;

            // 捲動貼圖，製造箭頭往 Target 流動的感覺
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
