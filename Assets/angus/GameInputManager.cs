using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    private GameInput gameInput;
    public event Action<Vector2> moveInput;

    private void OnEnable()
    {
        gameInput = new GameInput();
        gameInput.Player.Enable();

        gameInput.Player.Movement.performed += OnMovePerformed;
        gameInput.Player.Movement.canceled += OnMoveCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 moveVector = ctx.ReadValue<Vector2>();
        Debug.Log("AA");
        moveInput?.Invoke(moveVector);
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput?.Invoke(Vector2.zero);
    }
}
