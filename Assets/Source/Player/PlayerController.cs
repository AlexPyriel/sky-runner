using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GrapplingGun _grapplingGun;

    private PlayerControls _playerControls;
    private float _targetRoutePosition;
    private float _leftRoutePosition = -3f;
    private float _rightRoutePosition = 3f;
    private float _dashSpeed = 40f;

    public event Action<Game.Routes> RouteChanged;

    private void Awake()
    {
        _playerControls = new PlayerControls();
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

    private void Start()
    {
        DashRight();
    }

    private void Update()
    {
        DashMovement();
    }

    private void DashMovement()
    {
        if (_player.position.x != _targetRoutePosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_targetRoutePosition, transform.position.y, transform.position.z), _dashSpeed * Time.deltaTime);
        }
    }

    private void DashRight()
    {
        _targetRoutePosition = _rightRoutePosition;
        RouteChanged?.Invoke(Game.Routes.Right);
    }

    private void DashLeft()
    {
        _targetRoutePosition = _leftRoutePosition;
        RouteChanged?.Invoke(Game.Routes.Left);
    }

    private void TryDashRight(InputAction.CallbackContext context)
    {
        if (_grapplingGun.IsAttached == false)
            DashRight();
    }

    private void TryDasheLeft(InputAction.CallbackContext context)
    {
        if (_grapplingGun.IsAttached == false)
            DashLeft();
    }

    private void TrySwing(InputAction.CallbackContext context)
    {
        _grapplingGun.StartSwing();
    }

    private void StopSwing(InputAction.CallbackContext context)
    {
        _grapplingGun.StopSwing();
    }
}
