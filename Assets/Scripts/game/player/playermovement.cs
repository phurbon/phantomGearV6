using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;  // Increased speed for better movement
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    private Vector2 moveInput;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Rigidbody2D rb;
    private Animator animator;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (moveInput != Vector2.zero)
        {
            bool success = MovePlayer(moveInput);

            if (!success)
            {
                // Try horizontal movement first
                success = MovePlayer(new Vector2(moveInput.x, 0));

                // If horizontal fails, try vertical movement
                if (!success)
                {
                    success = MovePlayer(new Vector2(0, moveInput.y));
                }
            }

            animator.SetBool("isMoving", success);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private bool MovePlayer(Vector2 direction)
    {
        // DEBUG: Print movement direction
        Debug.Log("Trying to move: " + direction);

        // Check for potential collisions
        int count = rb.Cast(
            direction, 
            movementFilter, 
            castCollisions, 
            moveSpeed * Time.fixedDeltaTime + collisionOffset
        );

        if (count == 0)
        {
            // Move the player if no collisions are detected
            rb.MovePosition(rb.position + (direction * moveSpeed * Time.fixedDeltaTime));
            return true;
        }
        else
        {
            Debug.Log("Blocked by: " + castCollisions[0].collider.name);
            return false;
        }
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log("Move Input: " + moveInput); // DEBUG: Check if input is working

        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("XInput", moveInput.x);
            animator.SetFloat("YInput", moveInput.y);
        }
    }

    public void OnFire()
    {
        Debug.Log("Shots fired!");
    }
}
