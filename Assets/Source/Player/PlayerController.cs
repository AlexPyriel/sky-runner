using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GrapplingGun _grapplingGun;

    private PlayerControls _playerControls;
    private float _targetRoutePosition;
    private float _leftRoutePosition = -4f;
    private float _middleRoutePosition = 0;
    private float _rightRoutePosition = 4f;
    private float _stopSwingHeight = 16f;
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
        _playerControls.Player.Swing.performed += TrySwing;
    }

    private void OnDisable()
    {
        _playerControls.Player.DashRight.performed -= TryDashRight;
        _playerControls.Player.DashLeft.performed -= TryDasheLeft;
        _playerControls.Player.Swing.performed -= TrySwing;
        _playerControls.Disable();
    }

    private void Update()
    {
        DashMovement();
    }

    private void LateUpdate()
    {
        TryStopSwing();
    }

    private void DashMovement()
    {
        if (_player.position.x != _targetRoutePosition)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_targetRoutePosition, transform.position.y, transform.position.z), _dashSpeed * Time.deltaTime);
    }

    private void DashRight()
    {
        if (_player.transform.position.x < 0)
        {
            _targetRoutePosition = _middleRoutePosition;
            RouteChanged?.Invoke(Game.Routes.Middle);
        }
        else
        {
            _targetRoutePosition = _rightRoutePosition;
            RouteChanged?.Invoke(Game.Routes.Right);
        }
    }

    private void DashLeft()
    {
        if (_player.transform.position.x > 0)
        {
            _targetRoutePosition = _middleRoutePosition;
            RouteChanged?.Invoke(Game.Routes.Middle);
        }
        else
        {
            _targetRoutePosition = _leftRoutePosition;
            RouteChanged?.Invoke(Game.Routes.Left);
        }
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
        if (_grapplingGun.IsAttached == true) return;

        if (Mathf.Round(_player.position.y) < _stopSwingHeight)
            _grapplingGun.StartSwing();
    }

    private void TryStopSwing()
    {
        if (_grapplingGun.IsAttached == false) return;

        if (Mathf.Round(_player.position.y) == _stopSwingHeight)
            _grapplingGun.StopSwing();
    }
}
