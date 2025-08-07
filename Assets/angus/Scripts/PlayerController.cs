using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameInputManager inputManager;
    [SerializeField]
    private CharacterController character;

    public PlayerLook playerLook;

    public Vector3 move;

    public Vector2 rotate;

    public float gravity = -9.81f;


    public float verticalVelocity = 0f;

    public float crouchHeight = 1f;
    public float standingHeight = 2f;
    public Vector3 standingCenter = new Vector3(0, 1f, 0);
    public Vector3 crouchCenter = new Vector3(0, 0.5f, 0);
    public bool isCrouching = false;


    public float speed = 5f;

    void OnEnable()
    {
        inputManager = FindAnyObjectByType<GameInputManager>();

        inputManager.moveInput += HandleMoveInput;
        inputManager.rotateInput += HandleRotateInput;
    }

    void OnDisable()
    {
        if (inputManager != null)
        {
            inputManager.moveInput -= HandleMoveInput;
            inputManager.rotateInput -= HandleRotateInput;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        ApplyGravity();
        Move();
    }

    void LateUpdate()
    {
        playerLook.playerRotate(rotate);
    }

    public void HandleMoveInput(Vector2 _moveInput)
    {
        move = new Vector3(_moveInput.x, 0, _moveInput.y);
    }

    public void Move()
    {
        Vector3 velocity = move * speed + Vector3.up * verticalVelocity;

        character.Move(transform.TransformDirection(velocity) * Time.deltaTime);
    }
    public void HandleRotateInput(Vector2 rotateInput)
    {
        rotate = rotateInput;
    }

    private void ApplyGravity()
    {
        if (character.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // 貼地但避免浮動
        }
        verticalVelocity += gravity * Time.deltaTime;
    }

    public void HandleCrouchInput(bool isPressed)
    {
        isCrouching = isPressed;

        if (isCrouching)
        {
            character.height = crouchHeight;
            character.center = crouchCenter;
        }
        else
        {
            character.height = standingHeight;
            character.center = standingCenter;
        }
    }



}
