using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttachPointSensor : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    #region Spherecast

    [Header("SphereCast Test")]
    [SerializeField] private float _sphereRadius;
    [SerializeField] private float _curerntHitDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private Vector3 _origin;

    private LayerMask _layerMask;
    private GameObject _currentHitObject;
    private Vector3 _currentAttachPoint;

    #endregion

    public event Action<Vector3> AttachPointDetected;
    public event Action AttachPointLost;

    private void OnEnable()
    {
        _playerController.RouteChanged += OnRouteChanged;
    }

    private void OnDisable()
    {
        _playerController.RouteChanged -= OnRouteChanged;
    }

    private void OnRouteChanged(LayerMask layerMask)
    {
        _layerMask = layerMask;
    }

    private void ChecWhatIsGrappleable()
    {
        _origin = transform.position;
        _direction = Vector3.forward;

        if (Physics.SphereCast(_origin, _sphereRadius, _direction, out RaycastHit hit, _maxDistance, _layerMask, QueryTriggerInteraction.UseGlobal))
        {
            _currentHitObject = hit.transform.gameObject;
            _currentAttachPoint = hit.point;
            _curerntHitDistance = hit.distance;

            AttachPointDetected?.Invoke(_currentAttachPoint);

            if (_currentHitObject.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
            {
                renderer.material.color = Color.green;
            }
        }
        else
        {
            if (_currentHitObject != null)
            {
                if (_currentHitObject.TryGetComponent<MeshRenderer>(out MeshRenderer renderer))
                {
                    renderer.material.color = Color.red;
                }
            }

            _currentHitObject = null;
            _curerntHitDistance = _maxDistance;

            AttachPointLost?.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(_origin, _origin + _direction * _curerntHitDistance);
        Gizmos.DrawWireSphere(_origin + _direction * _curerntHitDistance, _sphereRadius);
    }

    private void Update()
    {
        ChecWhatIsGrappleable();
    }
}
