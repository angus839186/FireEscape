using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController character;
    public PlayerLook playerLook;

    [Header("Move / Look")]
    public Vector3 move;
    public Vector2 rotate;
    public float speed = 5f;
    public bool interacting;

    [Header("Gravity")]
    public float gravity = -9.81f;
    public float verticalVelocity = 0f;

    [Header("Crouch")]
    public float standingHeight = 2f;
    public float crouchHeight = 1f;


    [SerializeField] private Transform cameraRoot;

    // 眼睛相對膝蓋/腳底的偏移（會隨身高比例縮放）
    [SerializeField] private float eyeHeightRatio = 0.9f;

    // 蹲下/起身 平滑時間（越小越快）
    [SerializeField] private float crouchSmoothTime = 0.12f;



    // 蹲下時速度可選擇降低
    [SerializeField] private float crouchSpeedMultiplier = 0.7f;

    // 內部狀態
    public bool wantsCrouch = false;
    private float currentHeight;
    private float targetHeight;
    private float heightVel;       // SmoothDamp 用
    private float camYVel;         // SmoothDamp 用
    private float baseSpeed;

    void Start()
    {
        baseSpeed = speed;

        // 初始高度與目標高度
        currentHeight = character.height > 0 ? character.height : standingHeight;
        targetHeight = currentHeight;

        character.height = currentHeight;
        Vector3 c = character.center;
        c.y = currentHeight * 0.5f;
        character.center = c;

        // 相機起始高度（對齊眼睛高度）
        if (cameraRoot != null)
        {
            var lp = cameraRoot.localPosition;
            lp.y = GetEyeHeight(currentHeight);
            cameraRoot.localPosition = lp;
        }

        if (GameInputManager.Instance != null)
        {
            GameInputManager.Instance.moveInput += HandleMoveInput;
            GameInputManager.Instance.rotateInput += HandleRotateInput;
            GameInputManager.Instance.crouchInput += HandleCrouchInput;
        }
    }

    void OnDisable()
    {
        if (GameInputManager.Instance != null)
        {
            GameInputManager.Instance.moveInput -= HandleMoveInput;
            GameInputManager.Instance.rotateInput -= HandleRotateInput;
            GameInputManager.Instance.crouchInput -= HandleCrouchInput;
        }
    }

    void Update()
    {
        ApplyGravity();
        UpdateCrouch(Time.deltaTime);

        if (!interacting)
        {
            Move();
        }
    }

    void LateUpdate()
    {
        if (!interacting)
        {
            playerLook.playerRotate(rotate);
        }
    }

    public void HandleMoveInput(Vector2 _moveInput)
    {
        move = new Vector3(_moveInput.x, 0, _moveInput.y);
    }

    public void HandleRotateInput(Vector2 rotateInput)
    {
        rotate = rotateInput;
    }


    public void HandleCrouchInput(bool isPressed)
    {
        wantsCrouch = isPressed;
    }

    private void Move()
    {
        // 依蹲下狀態調整速度
        float targetSpeed = wantsCrouch ? baseSpeed * crouchSpeedMultiplier : baseSpeed;
        speed = Mathf.Lerp(speed, targetSpeed, 10f * Time.deltaTime);

        Vector3 velocity = move * speed + Vector3.up * verticalVelocity;
        character.Move(transform.TransformDirection(velocity) * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (character.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f; // 貼地避免浮動

        verticalVelocity += gravity * Time.deltaTime;
    }

    private void UpdateCrouch(float dt)
    {
        // 目標高度：想蹲下則用 crouchHeight；想站起則先做頭頂檢查
        if (wantsCrouch)
        {
            targetHeight = crouchHeight;
        }
        else
        {
            targetHeight = standingHeight;
        }

        // 平滑更新膝上高度
        currentHeight = Mathf.SmoothDamp(currentHeight, targetHeight, ref heightVel, crouchSmoothTime);

        // 保持腳底不動：center = (0, height/2, 0)
        character.height = currentHeight;
        var c = character.center;
        c.y = currentHeight * 0.5f;
        character.center = c;

        // 相機跟著滑動到眼睛高度
        if (cameraRoot != null)
        {
            float camTargetY = GetEyeHeight(currentHeight);
            var lp = cameraRoot.localPosition;
            lp.y = Mathf.SmoothDamp(lp.y, camTargetY, ref camYVel, crouchSmoothTime);
            cameraRoot.localPosition = lp;
        }
    }

    private float GetEyeHeight(float h)
    {
        return h * eyeHeightRatio;
    }
}
