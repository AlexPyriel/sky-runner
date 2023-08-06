using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCaster : MonoBehaviour
{
    #region Spherecast

    [Header("SphereCast Test")]
    [SerializeField] private float _sphereRadius;
    [SerializeField] private float _curerntHitDistance;
    [SerializeField] private float _maxDistance;
    [SerializeField] private Vector3 _direction;
    [SerializeField] private Vector3 _origin;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _currentHitObject;

    #endregion

    private void ChecWhatIsGrappleable()
    {
        _origin = transform.position;
        _direction = Vector3.forward;
        RaycastHit hit;

        if (Physics.SphereCast(_origin, _sphereRadius, _direction, out hit, _maxDistance, _layerMask, QueryTriggerInteraction.UseGlobal))
        {
            _currentHitObject = hit.transform.gameObject;
            _curerntHitDistance = hit.distance;
        }
        else
        {
            _currentHitObject = null;
            _curerntHitDistance = _maxDistance;
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
