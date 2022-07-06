using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    Vector2 moveDirection = Vector2.zero;

    public PlayerInputActions playerControls;
    private InputAction move;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        moveSpeed = moveSpeed * Time.deltaTime;
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
