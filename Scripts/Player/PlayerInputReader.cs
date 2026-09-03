using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputReader : IDisposable
{
    private PlayerInputActions _playerInputActions;

    public PlayerInputReader()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Move.performed += OnMovePerformed;
        _playerInputActions.Player.Move.canceled += OnMoveCancelled;
        _playerInputActions.Player.Enable();
    }

    public Vector2 Move { get; private set; }

    public void Dispose()
    {
        _playerInputActions.Dispose();
    }

    private void OnMovePerformed(CallbackContext callbackContext)
    {
        Move = callbackContext.ReadValue<Vector2>();
    }

    private void OnMoveCancelled(CallbackContext callbackContext)
    {
        Move = Vector2.zero;
    }
}
