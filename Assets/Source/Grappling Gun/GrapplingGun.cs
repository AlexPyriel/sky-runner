using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _gunTip;
    [SerializeField] private Transform _player;
    [SerializeField] private AttachPointSensor _sensor;

    public Transform GunTip => _gunTip;

    [Header("Add Force on stop swing")]
    [SerializeField] private float _horizontalThrustForce;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private Rigidbody _rigidbody;

    private bool _isAttached;
    private bool _attachPointDetected;
    private Vector3 _attachPoint;

    public bool IsAttached => _isAttached;
    public Vector3 AttachPoint => _attachPoint;


    [Header("Joint Settings")]
    [SerializeField] private float _spring = 4.5f;
    [SerializeField] private float _damper = 7f;
    [SerializeField] private float _massScale = 4.5f;

    private SpringJoint joint;

    private void OnEnable()
    {
        _sensor.AttachPointDetected += OnAttachPointDetected;
        _sensor.AttachPointLost += OnAttachPointLost;
    }

    private void OnDisable()
    {
        _sensor.AttachPointDetected -= OnAttachPointDetected;
        _sensor.AttachPointLost -= OnAttachPointLost;
    }

    // выяснить сколько раз срабатывают события в пистолете
    private void OnAttachPointDetected()
    {
        if (_isAttached == false)
        {
            _attachPointDetected = true;
        }
    }

    private void OnAttachPointLost()
    {
        if (_isAttached == false)
        {
            _attachPointDetected = false;
        }
    }

    public void StartSwing()
    {
        if (_attachPointDetected)
        {
            _isAttached = true;

            _attachPoint = _sensor.CurrentAttachPoint;
            Debug.Log($"Started attaching to {_attachPoint}");

            joint = _player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = _attachPoint;

            float distanceFromPoint = Vector3.Distance(_player.position, _attachPoint);

            // the distance grapple will try to keep from grapple point.
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // customize values as you like
            joint.spring = _spring;
            joint.damper = _damper;
            joint.massScale = _massScale;
        }
    }

    public void StopSwing()
    {
        if (_isAttached)
        {
            // Debug.Log("Stopped");
            _isAttached = false;
            Destroy(joint);

            _rigidbody.velocity = Vector3.Scale(_rigidbody.velocity, new Vector3(0, 1, 1));

            if (_rigidbody.velocity.z + _horizontalThrustForce < _maxVelocity)
                _rigidbody.AddForce(new Vector3(0, 0, _horizontalThrustForce), ForceMode.VelocityChange);
        }
    }
}
