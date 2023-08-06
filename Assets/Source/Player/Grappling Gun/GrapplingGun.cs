using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _gunTip;
    [SerializeField] private Transform _player;
    // [SerializeField] private PlayerController _playerController;

    public Transform GunTip => _gunTip;

    [Header("Swinging")]
    [SerializeField] private float _horizontalThrustForce;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float maxSwingDistance = 25;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _sphereRadius;

    private bool _isAttached;
    private Vector3 _swingPoint;

    public bool IsAttached => _isAttached;
    public Vector3 SwingPoint => _swingPoint;


    [Header("Joint Settings")]
    [SerializeField] private float _spring = 4.5f;
    [SerializeField] private float _damper = 7f;
    [SerializeField] private float _massScale = 4.5f;

    private SpringJoint joint;

    public void StartSwing(LayerMask whatIsGrappleable)
    {
        RaycastHit sphereCastHit;

        if (Physics.SphereCast(transform.position, _sphereRadius, Vector3.forward, out sphereCastHit, maxSwingDistance, whatIsGrappleable))
        {
            Debug.Log("Started");
            _isAttached = true;
            _swingPoint = sphereCastHit.point;
            joint = _player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = _swingPoint;

            float distanceFromPoint = Vector3.Distance(_player.position, _swingPoint);

            // the distance grapple will try to keep from grapple point.
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // customize values as you like
            joint.spring = _spring;
            joint.damper = _damper;
            joint.massScale = _massScale;

            sphereCastHit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    public void StopSwing()
    {
        if (_isAttached)
        {
            Debug.Log("Stopped");
            _isAttached = false;
            Destroy(joint);

            _rigidbody.velocity = Vector3.Scale(_rigidbody.velocity, new Vector3(0, 1, 1));

            if (_rigidbody.velocity.z + _horizontalThrustForce < _maxVelocity)
                _rigidbody.AddForce(new Vector3(0, 0, _horizontalThrustForce), ForceMode.VelocityChange);
        }
    }



    private void Start()
    {

    }

    private void Update()
    {

    }
}
