using UnityEngine;
using TMPro;

public class GameHUDController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private TextMeshProUGUI interactionPromptText;

    public void SetStatus(string message)
    {
        if (statusText != null)
            statusText.text = $"Estado: {message}";
    }

    public void ShowInteractionPrompt(bool show, string customText = null)
    {
        if (interactionPrompt == null) return;

        interactionPrompt.SetActive(show);

        if (show && !string.IsNullOrEmpty(customText) && interactionPromptText != null)
        {
            interactionPromptText.text = customText;
        }
    }
}