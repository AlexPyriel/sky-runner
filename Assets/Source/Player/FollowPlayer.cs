using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _player.position;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(0, 26f, _player.position.z) + _offset;
    }
}
