using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 15f;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    Vector2 moveDirection = Vector2.zero;
    

    public PlayerInputActions playerControls;
    private InputAction move;
    private InputAction jump;

    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.05f;
    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private LayerMask groundMask;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        move = playerControls.Player.Move;
        jump = playerControls.Player.Jump;
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    private void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed));
        }
        

        if (jump.IsPressed())
        {
            if (IsGrounded())
            {
                Debug.Log("On the ground");
                rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
            }
            else Debug.Log("NOT On the ground");
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);
    }
    
}
