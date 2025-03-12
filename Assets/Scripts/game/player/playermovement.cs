using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class playermovement : MonoBehaviour
{
   /* private Rigidbody2D _rigidBody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    [SerializeField]
    private float _speed;
    private Animator _animator;
    private Vector2 _lastNonZeroMovement = Vector2.down; // Default facing down



    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(_smoothedMovementInput, _movementInput, ref _movementInputSmoothVelocity, 0.1f);
        _rigidBody.velocity = _smoothedMovementInput * _speed;
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

        if (isMoving)
    {
        _animator.SetFloat("MoveX", _movementInput.x);
        _animator.SetFloat("MoveY", _movementInput.y);

        if (_movementInput.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (_movementInput.x > 0)
            GetComponent<SpriteRenderer>().flipX = false;
    }

    } 

*/
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
 
    private Vector2 moveInput;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Rigidbody2D rb;
    private Animator animator;
 
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
 
    public void FixedUpdate()
    {
        
        // rb.MovePosition(rb.position + (moveInput * moveSpeed * Time.fixedDeltaTime));
        
        if(moveInput != Vector2.zero){
            // Try to move player in input direction, followed by left right and up down input if failed
            bool success = MovePlayer(moveInput);
            
            if(!success)
            {
                // Try Left / Right
                success = MovePlayer(new Vector2(moveInput.x, 0));
 
                if(!success)
                {
                    success = MovePlayer(new Vector2(0, moveInput.y));
                }
            }
 
            animator.SetBool("isMoving", success);
        } 
        else{
            animator.SetBool("isMoving", false);
        }
        
 
    }
 
    // Tries to move the player in a direction by casting in that direction by the amount
    // moved plus an offset. If no collisions are found, it moves the players
    // Returns true or false depending on if a move was executed
    public bool MovePlayer(Vector2 direction)
    {
        // Check for potential collisions
        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset
 
        if (count == 0)
        {
            Vector2 moveVector = direction * moveSpeed * Time.fixedDeltaTime;
 
            // No collisions
            rb.MovePosition(rb.position + moveVector);
            return true;
        }
        else
        {
            // Print collisions
            foreach (RaycastHit2D hit in castCollisions)
            {
                print(hit.ToString());
            }
 
            return false;
        }
    }
    
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
 
        // Only set the animation direction if the player is trying to move
        if(moveInput != Vector2.zero) {
            animator.SetFloat("XInput", moveInput.x);
            animator.SetFloat("YInput", moveInput.y);
        }
    }
 
    public void OnFire()
    {
        print("Shots fired");
    }
   
}
