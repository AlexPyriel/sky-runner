using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GrapplingGun _grapplingGun;
    [SerializeField] private LineRenderer _lineRenderer;

    [Header("Settings")]
    [SerializeField] private int _quality = 200; // how many segments the rope will be split up in
    [SerializeField] private float _damper = 14; // this slows the simulation down, so that not the entire rope is affected the same
    [SerializeField] private float _strength = 800; // how hard the simulation tries to get to the target point
    [SerializeField] private float _velocity = 15; // velocity of the animation
    [SerializeField] private float _waveCount = 3; // how many waves are being simulated
    [SerializeField] private float _waveHeight = 1;
    [SerializeField] private AnimationCurve _affectCurve;

    private Spring _spring; // a custom script that returns the values needed for the animation
    private Vector3 _currentGrapplePosition;

    private void Awake()
    {
        _spring = new Spring();
        _spring.SetTarget(0);
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope()
    {
        // if not grappling, don't draw rope
        if (!_grapplingGun.IsAttached)
        {
            _currentGrapplePosition = _grapplingGun.GunTip.position;

            // reset the simulation
            _spring.Reset();

            // reset the positionCount of the lineRenderer
            if (_lineRenderer.positionCount > 0)
                _lineRenderer.positionCount = 0;

            return;
        }

        if (_lineRenderer.positionCount == 0)
        {
            // set the start velocity of the simulation
            _spring.SetVelocity(_velocity);

            // set the positionCount of the lineRenderer depending on the quality of the rope
            _lineRenderer.positionCount = _quality + 1;
        }

        // set the spring simulation
        _spring.SetDamper(_damper);
        _spring.SetStrength(_strength);
        _spring.Update(Time.deltaTime);

        Vector3 grapplePoint = _grapplingGun.AttachPoint;
        Vector3 gunTipPosition = _grapplingGun.GunTip.position;

        // find the upwards direction relative to the rope
        Vector3 up = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized) * Vector3.up;

        // lerp the currentGrapplePositin towards the grapplePoint
        _currentGrapplePosition = Vector3.Lerp(_currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        // loop through all segments of the rope and animate them
        for (int i = 0; i < _quality + 1; i++)
        {
            float delta = i / (float)_quality;
            // calculate the offset of the current rope segment
            Vector3 offset = up * _waveHeight * Mathf.Sin(delta * _waveCount * Mathf.PI) * _spring.Value * _affectCurve.Evaluate(delta);

            // lerp the lineRenderer position towards the currentGrapplePosition + the offset you just calculated
            _lineRenderer.SetPosition(i, Vector3.Lerp(gunTipPosition, _currentGrapplePosition, delta) + offset);
        }
    }
}
