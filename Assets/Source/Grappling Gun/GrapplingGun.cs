using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _gunTip;
    [SerializeField] private Transform _player;
    [SerializeField] private AttachPointSensor _sensor;

    private bool _isAttached;
    private Vector3 _attachPoint;

    public Transform GunTip => _gunTip;
    public bool IsAttached => _isAttached;
    public Vector3 AttachPoint => _attachPoint;


    [Header("Joint Settings")]
    [SerializeField] private float _spring = 4.5f;
    [SerializeField] private float _damper = 7f;
    [SerializeField] private float _massScale = 4.5f;

    private SpringJoint _joint;

    public void StartSwing()
    {
        if (_sensor.TryGetAttachPoint(out Vector3 attachPoint))
        {
            _isAttached = true;
            _attachPoint = attachPoint;

            _joint = _player.gameObject.AddComponent<SpringJoint>();
            _joint.autoConfigureConnectedAnchor = false;
            _joint.connectedAnchor = _attachPoint;

            float distanceFromPoint = Vector3.Distance(_player.position, _attachPoint);

            // the distance grapple will try to keep from grapple point.
            // _joint.maxDistance = distanceFromPoint * 0.8f;
            _joint.maxDistance = distanceFromPoint * 0.5f;
            _joint.minDistance = distanceFromPoint * 0.25f;

            // customize values as you like
            _joint.spring = _spring;
            _joint.damper = _damper;
            _joint.massScale = _massScale;
        }
    }

    public void StopSwing()
    {
        if (_isAttached)
        {
            _isAttached = false;
            Destroy(_joint);
        }
    }
}
