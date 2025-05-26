using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement 
{
    [Header("Physics")]
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float _jumpForce = 2.0f;

    private Vector3 _dir = new();
    float _speed = 2;
    Transform _transform;
    private Rigidbody _rb;
    public Movement(Transform transform, float speed)
    {
        _transform = transform;

        _speed = speed;

    }
    ~Movement()
    {



    }



    public void Move(float vertical, float horizontal)
    {

        Vector3 moveDir = _transform.forward * _dir.z + _transform.right * _dir.x;
        _rb.MovePosition(_transform.position + moveDir.normalized * currentMoveSpeed * Time.fixedDeltaTime);



    }
    public void Jump()
    {

        _rb.AddForce(_transform.up * _jumpForce, ForceMode.Impulse);

    }


}
