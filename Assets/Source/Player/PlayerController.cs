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

        //To be refactored
        _targetRoutePosition = _rightRoutePosition;
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
            _targetRoutePosition = _rightRoutePosition;
            RouteChanged?.Invoke(Game.Routes.Right);
        }
    }

    private void TryDasheLeft(InputAction.CallbackContext context)
    {
        if (_grapplingGun.IsAttached == false)
        {
            _targetRoutePosition = _leftRoutePosition;
            RouteChanged?.Invoke(Game.Routes.Left);
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
        if (_player.position.x != _targetRoutePosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_targetRoutePosition, transform.position.y, transform.position.z), _dashSpeed * Time.deltaTime);
        }
    }

    private void Start()
    {
        //To be refactored
        RouteChanged?.Invoke(Game.Routes.Right);
        //To be refactored

    }

    private void Update()
    {
        Dash();
    }
}
