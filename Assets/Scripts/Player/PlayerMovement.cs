using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

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

    private void Update()
    {
        playerRigidBody.velocity = new Vector2(movementInput.x * moveSpeed, playerRigidBody.velocity.y);
    }
}
