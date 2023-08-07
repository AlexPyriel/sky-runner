using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _grappleableRight;
    [SerializeField] private LayerMask _grappleableLeft;
    [SerializeField] private GrapplingGun _grapplingGun;
    private PlayerControls _playerControls;

    private LayerMask _whatIsGrappleable;
    private float _targetPosition;
    private float _leftRoutePosition = -3f;
    private float _rightRoutePosition = 3f;
    private float _dashSpeed = 40f;

    public event Action<LayerMask> RouteChanged;

    private void Awake()
    {
        _playerControls = new PlayerControls();

        //To be refactored
        _targetPosition = _rightRoutePosition;
        _whatIsGrappleable = _grappleableRight;
        RouteChanged?.Invoke(_whatIsGrappleable);
        //To be refactored
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Player.DashRight.performed += TryDashRight;
        _playerControls.Player.DashLeft.performed += TryDasheLeft;
        _playerControls.Player.Swing.started += TrySwing;
        _playerControls.Player.Swing.canceled += StopSwing;
    }

    private void OnDisable()
    {
        _playerControls.Player.DashRight.performed -= TryDashRight;
        _playerControls.Player.DashLeft.performed -= TryDasheLeft;
        _playerControls.Player.Swing.started -= TrySwing;
        _playerControls.Player.Swing.canceled -= StopSwing;
        _playerControls.Disable();
    }

    private void TryDashRight(InputAction.CallbackContext context)
    {
        if (_grapplingGun.IsAttached == false)
        {
            // transform.position = new Vector3(_rightRoutePosition, transform.position.y, transform.position.z);
            _targetPosition = _rightRoutePosition;
            _whatIsGrappleable = _grappleableRight;
            RouteChanged?.Invoke(_whatIsGrappleable);
        }
    }

    private void TryDasheLeft(InputAction.CallbackContext context)
    {
        if (_grapplingGun.IsAttached == false)
        {
            // transform.position = new Vector3(_leftRoutePosition, transform.position.y, transform.position.z);
            _targetPosition = _leftRoutePosition;
            _whatIsGrappleable = _grappleableLeft;
            RouteChanged?.Invoke(_whatIsGrappleable);
        }
    }

    private void TrySwing(InputAction.CallbackContext context)
    {
        _grapplingGun.StartSwing();
    }

    private void StopSwing(InputAction.CallbackContext context)
    {
        _grapplingGun.StopSwing();
    }

    private void Dash()
    {
        if (_player.position.x != _targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_targetPosition, transform.position.y, transform.position.z), _dashSpeed * Time.deltaTime);
        }
    }



    private void Update()
    {
        Dash();
    }
}
