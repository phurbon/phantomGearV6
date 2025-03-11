using UnityEngine;
using UnityEngine.InputSystem;

public class playermovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    [SerializeField]
    private float _speed;
    private Animator _animator;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _movementInputSmoothVelocity,0.1f);
        _rigidBody.linearVelocity = _smoothedMovementInput * _speed;
        SetAnimation();
        
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();


    }

    private void SetAnimation()
    {
        bool isMoving = _movementInput != Vector2.zero;
        _animator.SetBool("IsMoving", isMoving);

    }
}
