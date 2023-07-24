using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        _playerControls.Player.Jump.performed -= Jump;
        _playerControls.Disable();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
    }
}
