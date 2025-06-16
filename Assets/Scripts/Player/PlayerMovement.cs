using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private bool isJumping = false;

    private Rigidbody2D playerRigidBody;
    private Vector2 movementInput;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && !isJumping)
        {
            isJumping = true;
            playerRigidBody.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        playerRigidBody.velocity = new Vector2(movementInput.x * moveSpeed, playerRigidBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
