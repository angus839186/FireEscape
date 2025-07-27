using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    private GameInput gameInput;
    public event Action<Vector2> moveInput;
    public event Action<Vector2> rotateInput;

    private void OnEnable()
    {
        gameInput = new GameInput();
        gameInput.Player.Enable();

        gameInput.Player.Movement.performed += OnMovePerformed;
        gameInput.Player.Movement.canceled += OnMoveCanceled;

        gameInput.Player.Look.performed += ctx => rotateInput?.Invoke(ctx.ReadValue<Vector2>());
        gameInput.Player.Look.canceled += ctx => rotateInput?.Invoke(Vector2.zero);
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 moveVector = ctx.ReadValue<Vector2>();
        Debug.Log("aaa");
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
