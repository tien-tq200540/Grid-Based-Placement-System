using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.PlaceThings.canceled += PlaceThings_canceled;
    }

    private void PlaceThings_canceled(InputAction.CallbackContext obj)
    {
        Vector3 mouseScreenPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseScreenPos.z = 0f;
        PlacementCtrl.Instance.Spawn(mouseScreenPos);
    }

    private void OnDisable()
    {
        inputActions.Player.PlaceThings.canceled -= PlaceThings_canceled;
        inputActions.Disable();
    }
}
