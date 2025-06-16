using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float rayDistance = 1f;
    public float interactImpulse = 5f;

    public Transform rayCastStart;
    public GameObject VFX;

    private bool isJumping = false;

    private Rigidbody2D playerRigidBody;
    private Vector2 movementInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            playerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RayCast();
        }
    }

    private void FixedUpdate()
    {
        playerRigidBody.velocity = new Vector2(movementInput.x * moveSpeed, playerRigidBody.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(movementInput.x));

        if(movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void RayCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayCastStart.position, Vector2.right, rayDistance);
        Debug.DrawRay(rayCastStart.position, Vector2.right * rayDistance, Color.red, 2f);

        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            playerRigidBody.AddForce(new Vector2(interactImpulse, 0), ForceMode2D.Impulse);
            Instantiate(VFX, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }
}
