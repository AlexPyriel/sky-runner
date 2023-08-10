using System;
using UnityEngine;

public class AttachPointSensor : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    private Game.Routes _targetRoute;

    private Vector3 _leftPoint;
    private Vector3 _rightPoint;

    public Vector3 CurrentAttachPoint => _targetRoute == Game.Routes.Left ? _leftPoint : _rightPoint;

    public event Action AttachPointDetected;
    public event Action AttachPointLost;

    private void OnEnable()
    {
        _playerController.RouteChanged += OnRouteChanged;
    }

    private void OnDisable()
    {
        _playerController.RouteChanged -= OnRouteChanged;
    }

    private void OnRouteChanged(Game.Routes route)
    {
        _targetRoute = route;
        Debug.Log($"Route changed to {route}");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<AttachPoint>(out AttachPoint point))
        {
            Debug.Log("enter");
            point.Enable();

            if (point.Route == Game.Routes.Left)
            {
                _leftPoint = point.transform.position;
                Debug.Log($"Left point {_leftPoint}");
            }
            else if (point.Route == Game.Routes.Right)
            {
                _rightPoint = point.transform.position;
                Debug.Log($"Right point {_rightPoint}");
            }
        }

        if (_leftPoint != null || _rightPoint != null)
            AttachPointDetected?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<AttachPoint>(out AttachPoint point))
        {
            Debug.Log("Lost point");
            point.Disable();
            AttachPointLost?.Invoke();
        }
    }

    // public Vector3 GetCurrentAttachPoint()
    // {
    //     if (_leftPoint != null || _rightPoint != null)
    //         return _targetRoute == Game.Routes.Left ? _leftPoint : _rightPoint;
    // }
}
