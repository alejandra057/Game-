using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 15f;

    [Header("Salto")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private LayerMask groundMask = ~0;

    [Header("Sprint")]
    [SerializeField] private float sprintMultiplier = 2f;

    [Header("Referencias")]
    [SerializeField] private Transform cameraTransform;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool isSprinting;
    private bool isGrounded;
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Jump.performed += OnJump;
        inputActions.Player.Sprint.performed += OnSprint;
        inputActions.Player.Sprint.canceled += OnSprint;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Jump.performed -= OnJump;
        inputActions.Player.Sprint.performed -= OnSprint;
        inputActions.Player.Sprint.canceled -= OnSprint;
        inputActions.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        jumpPressed = true;
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();
    }

    private void Update()
    {
        Vector3 rayOrigin = transform.position + Vector3.down * 0.9f;

        isGrounded = Physics.Raycast(rayOrigin, Vector3.down,
            groundCheckDistance, groundMask, QueryTriggerInteraction.Ignore);

        Debug.DrawRay(rayOrigin, Vector3.down * groundCheckDistance,
            isGrounded ? Color.green : Color.red);

        if (jumpPressed && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        jumpPressed = false;
    }

    private void FixedUpdate()
    {
        if (cameraTransform == null) return;

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 direction = camForward * moveInput.y + camRight * moveInput.x;

        if (direction.sqrMagnitude > 0.01f)
        {
            direction.Normalize();
            float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;
            Vector3 move = direction * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}