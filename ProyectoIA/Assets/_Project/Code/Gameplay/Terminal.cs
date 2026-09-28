using UnityEngine;

public class Terminal : MonoBehaviour, IInteractable
{
    [SerializeField] private string terminalName = "Terminal";

    public string GetInteractionPrompt()
    {
        return $"[E] Abrir {terminalName}";
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact(GameObject interactor)
    {
        Debug.Log($"[Terminal] {interactor.name} interactuó con {terminalName}");
        // Aquí irá la lógica del desafío (FASE 8)
    }
}
