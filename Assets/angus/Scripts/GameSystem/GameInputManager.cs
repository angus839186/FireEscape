using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Instance;
    private GameInput gameInput;
    public event Action<Vector2> moveInput;
    public event Action<Vector2> rotateInput;

    public event Action<bool> crouchInput;

    public event Action interactInput;

    public event Action<bool> useItemInput;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        gameInput = new GameInput();
        gameInput.Player.Enable();

        gameInput.Player.Movement.performed += OnMovePerformed;
        gameInput.Player.Movement.canceled += OnMoveCanceled;

        gameInput.Player.Look.performed += OnLookPerformed;
        gameInput.Player.Look.canceled += OnLookCanceled;

        gameInput.Player.Crouch.performed += ctx => crouchInput?.Invoke(true);
        gameInput.Player.Crouch.canceled += ctx => crouchInput?.Invoke(false);

        gameInput.Player.Interact.performed += ctx => interactInput?.Invoke();

        gameInput.Player.UseItem.performed += ctx => useItemInput?.Invoke(true);
        gameInput.Player.UseItem.canceled += ctx => useItemInput?.Invoke(false);
    }

    private void OnDisable()
    {
        gameInput.Player.Movement.performed -= OnMovePerformed;
        gameInput.Player.Movement.canceled -= OnMoveCanceled;

        gameInput.Player.Look.performed -= OnLookPerformed;
        gameInput.Player.Look.canceled -= OnLookCanceled;

        gameInput.Player.Crouch.performed -= ctx => crouchInput?.Invoke(true);
        gameInput.Player.Crouch.canceled -= ctx => crouchInput?.Invoke(false);

        gameInput.Player.Interact.performed -= ctx => interactInput?.Invoke();

        gameInput.Player.UseItem.performed -= ctx => useItemInput?.Invoke(true);
        gameInput.Player.UseItem.canceled -= ctx => useItemInput?.Invoke(false);

        gameInput.Player.Disable();
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 moveVector = ctx.ReadValue<Vector2>();
        moveInput?.Invoke(moveVector);
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput?.Invoke(Vector2.zero);
    }
    private void OnLookPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 lookVector = ctx.ReadValue<Vector2>();
        rotateInput?.Invoke(lookVector);
    }
    private void OnLookCanceled(InputAction.CallbackContext ctx)
    {
        rotateInput?.Invoke(Vector2.zero);
    }
}
