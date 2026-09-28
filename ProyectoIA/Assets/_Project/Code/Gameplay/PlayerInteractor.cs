using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Detección")]
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private LayerMask interactableMask = ~0;
    [SerializeField] private Transform rayOrigin;

    [Header("UI")]
    [SerializeField] private GameHUDController hud;

    private InputSystem_Actions inputActions;
    private IInteractable currentInteractable;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();

        if (rayOrigin == null)
            rayOrigin = transform;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnInteract;
        inputActions.Player.Disable();
    }

    private void Update()
    {
        DetectInteractable();
        UpdatePrompt();
    }

    private void DetectInteractable()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactableMask, QueryTriggerInteraction.Collide))
        {
            currentInteractable = hit.collider.GetComponentInParent<IInteractable>();
        }
        else
        {
            currentInteractable = null;
        }
    }

    private void UpdatePrompt()
    {
        if (hud == null) return;

        if (currentInteractable != null && currentInteractable.CanInteract())
        {
            hud.ShowInteractionPrompt(true, currentInteractable.GetInteractionPrompt());
        }
        else
        {
            hud.ShowInteractionPrompt(false);
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (currentInteractable != null && currentInteractable.CanInteract())
        {
            currentInteractable.Interact(gameObject);
        }
    }
}