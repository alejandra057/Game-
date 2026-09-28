using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Texto que se muestra en el prompt cuando el jugador está cerca.
    /// Ejemplo: "[E] Abrir terminal"
    /// </summary>
    string GetInteractionPrompt();

    /// <summary>
    /// Acción que se ejecuta al pulsar E.
    /// </summary>
    void Interact(GameObject interactor);

    /// <summary>
    /// Si el objeto se puede interactuar en este momento.
    /// Útil para bloquear interacciones hasta cumplir requisitos.
    /// </summary>
    bool CanInteract();
}