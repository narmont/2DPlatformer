using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private Vector3 _targetPosition;

    private void Start()
    {
        _targetPosition = transform.position;
    }
}
