using UnityEngine;

public class EnnemyMovement1 : MonoBehaviour
{
    [SerializedField]
    private float _speed;

    [SerializedField]
    private float _rotationSpeed;
    private RigidBody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _target
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private oid Awake(){
        _rigidbody = GetComponent<RigidBody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection(){


    }

    private void RotateTowardsTarget(){


    }
    private void SetVelocity()
    {

    }
}
