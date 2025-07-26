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

    public Vector3 move;

    public float gravity = -9.81f;
    private float verticalVelocity = 0f;


    public float speed = 5f;

    void OnEnable()
    {
        inputManager = FindAnyObjectByType<GameInputManager>();

        inputManager.moveInput += HandleMoveInput;
    }

    void OnDisable()
    {
        if (inputManager != null)
        {
            inputManager.moveInput -= HandleMoveInput;
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

    public void HandleMoveInput(Vector2 _moveInput)
    {
        move = new Vector3(_moveInput.x, 0, _moveInput.y);
    }

    public void Move()
    {
        Vector3 velocity = move * speed + Vector3.up * verticalVelocity;

        character.Move(velocity * Time.deltaTime);
    }
    
    private void ApplyGravity()
    {
        if (character.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // 貼地但避免浮動
        }
        verticalVelocity += gravity * Time.deltaTime;
    }


    
}
