using UnityEngine;

public class AttachPointSensor : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    private Game.Routes _targetRoute;
    private Vector3 _leftPoint;
    private Vector3 _middlePoint;
    private Vector3 _rightPoint;

    private void OnEnable()
    {
        _playerController.RouteChanged += OnRouteChanged;
    }

    private void OnDisable()
    {
        _playerController.RouteChanged -= OnRouteChanged;
    }

    private void Start()
    {
        _targetRoute = Game.Routes.Middle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<AttachPoint>(out AttachPoint point))
        {
            point.Enable();

            if (point.Route == Game.Routes.Left)
                _leftPoint = point.transform.position;
            else if (point.Route == Game.Routes.Middle)
                _middlePoint = point.transform.position;
            else if (point.Route == Game.Routes.Right)
                _rightPoint = point.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<AttachPoint>(out AttachPoint point))
        {
            point.Disable();

            if (point.Route == Game.Routes.Left)
                _leftPoint = Vector3.zero;
            else if (point.Route == Game.Routes.Middle)
                _middlePoint = Vector3.zero;
            else if (point.Route == Game.Routes.Right)
                _rightPoint = Vector3.zero;
        }
    }

    private void OnRouteChanged(Game.Routes route)
    {
        _targetRoute = route;
    }

    public bool TryGetAttachPoint(out Vector3 attachPoint)
    {
        attachPoint = _targetRoute switch
        {
            Game.Routes.Left => _leftPoint,
            Game.Routes.Middle => _middlePoint,
            Game.Routes.Right => _rightPoint,
            _ => Vector3.zero,
        };

        return attachPoint != Vector3.zero;
    }
}
