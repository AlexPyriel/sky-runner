using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _maxCurrentVelocity;
    [SerializeField] private float _currentVelocity;
    [Header("References")]
    public LineRenderer lr;
    public Transform gunTip, cam, player;
    public LayerMask whatIsGrappleableRight;
    public LayerMask whatIsGrappleableLeft;

    [Header("Swinging")]
    private float maxSwingDistance = 25;
    private Vector3 swingPoint;
    private SpringJoint joint;
    private bool grappling;

    private Vector3 currentGrapplePosition;

    private PlayerControls _playerControls;
    [SerializeField] private float _predictionSphereRadius;
    [SerializeField] private float _spring = 4.5f;
    [SerializeField] private float _damper = 7f;
    [SerializeField] private float _massScale = 4.5f;
    [SerializeField] private Rigidbody _rigidbody;

    private LayerMask _whatIsGrappleable;
    private float _leftLine = -3f;
    private float _rightLine = 3f;

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

    private void TryDashRight(InputAction.CallbackContext context)
    {
        transform.position = new Vector3(_rightLine, transform.position.y, transform.position.z);
        _whatIsGrappleable = whatIsGrappleableRight;
    }

    private void TryDasheLeft(InputAction.CallbackContext context)
    {
        transform.position = new Vector3(_leftLine, transform.position.y, transform.position.z);
        _whatIsGrappleable = whatIsGrappleableLeft;
    }

    private void TrySwing(InputAction.CallbackContext context)
    {
        StartSwing(_whatIsGrappleable);
    }

    private void StartSwing(LayerMask whatIsGrappleable)
    {
        Debug.Log("Started");
        grappling = true;
        RaycastHit sphereCastHit;
        if (Physics.SphereCast(cam.position, _predictionSphereRadius, cam.forward, out sphereCastHit, maxSwingDistance, whatIsGrappleable))
        // if (Physics.Raycast(cam.position, cam.forward, out hit, maxSwingDistance, whatIsGrappleable))
        {
            swingPoint = sphereCastHit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            // the distance grapple will try to keep from grapple point.
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            // customize values as you like
            joint.spring = _spring;
            joint.damper = _damper;
            joint.massScale = _massScale;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;

            sphereCastHit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        // else
        // {
        //     grapplePoint = cam.position + cam.forward * maxGrappleDistance;

        //     Invoke(nameof(StopGrapple), grappleDelayTime);
        // }
    }

    private void StopSwing(InputAction.CallbackContext context)
    {
        Debug.Log("Stopped");
        grappling = false;
        lr.positionCount = 0;
        Destroy(joint);

        _rigidbody.velocity = Vector3.Scale(_rigidbody.velocity, new Vector3(0, 0.6f, 1));
        _rigidbody.AddForce(new Vector3(0, 0, 10), ForceMode.VelocityChange);

    }

    public Vector3 GetGrapplePoint()
    {
        return swingPoint;
    }

    private void DrawRope()
    {
        if (joint == null) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, swingPoint);
    }

    public bool IsGrappling()
    {
        return grappling;
    }

    private void Start()
    {

    }

    private void Update()
    {
        _currentVelocity = _rigidbody.velocity.z;

        if (_rigidbody.velocity.z > _maxCurrentVelocity)
            _maxCurrentVelocity = _rigidbody.velocity.z;


        if (_rigidbody.velocity.z > _maxVelocity)
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, _maxVelocity);

    }

    private void LateUpdate()
    {
        DrawRope();
    }
}
