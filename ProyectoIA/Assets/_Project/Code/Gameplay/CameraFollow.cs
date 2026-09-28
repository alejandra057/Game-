using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo")]
    [SerializeField] private Transform target;

    [Header("Posición")]
    [SerializeField] private float distance = 4f;
    [SerializeField] private float height = 1.5f;
    [SerializeField] private float lookAtHeight = 1f;

    [Header("Rotación")]
    [SerializeField] private float mouseSensitivity = 0.15f;
    [SerializeField] private float minPitch = -20f;
    [SerializeField] private float maxPitch = 40f;
    [SerializeField] private float initialPitch = 5f;

    [Header("Suavizado")]
    [SerializeField] private float followSmoothTime = 0.08f;

    [Header("Colisión")]
    [SerializeField] private LayerMask collisionMask = ~0;
    [SerializeField] private float collisionRadius = 0.2f;
    [SerializeField] private float minDistance = 0.5f;

    private float yaw;
    private float pitch;
    private Vector3 velocity;
    private InputSystem_Actions inputActions;
    private Vector2 lookInput;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        pitch = initialPitch;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Look.performed += OnLook;
        inputActions.Player.Look.canceled += OnLook;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        inputActions.Player.Look.performed -= OnLook;
        inputActions.Player.Look.canceled -= OnLook;
        inputActions.Player.Disable();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // 1. Actualizar rotación
        yaw += lookInput.x * mouseSensitivity;
        pitch -= lookInput.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // 2. Punto al que apunta la cámara (a la altura de los ojos del jugador)
        Vector3 lookAtPoint = target.position + Vector3.up * lookAtHeight;

        // 3. Dirección deseada (hacia atrás y arriba desde el lookAtPoint)
        Vector3 direction = rotation * Vector3.back;
        Vector3 desiredOffset = direction * distance + Vector3.up * height;
        Vector3 desiredPosition = lookAtPoint + desiredOffset;

        // 4. Colisión: lanzar un rayo desde lookAtPoint hacia desiredPosition
        Vector3 rayDirection = desiredPosition - lookAtPoint;
        float desiredDistance = rayDirection.magnitude;

        if (Physics.SphereCast(lookAtPoint, collisionRadius, rayDirection.normalized,
            out RaycastHit hit, desiredDistance, collisionMask, QueryTriggerInteraction.Ignore))
        {
            // Colocar la cámara un poco antes del impacto
            float safeDistance = Mathf.Max(hit.distance - collisionRadius, minDistance);
            desiredPosition = lookAtPoint + rayDirection.normalized * safeDistance;
        }

        // 5. Suavizar
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, followSmoothTime);
        transform.rotation = Quaternion.LookRotation(lookAtPoint - transform.position);
    }
}