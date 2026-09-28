using UnityEngine;
using TMPro;

public class GameHUDController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private GameObject interactionPrompt;

    public void SetStatus(string message)
    {
        if (statusText != null)
            statusText.text = $"Estado: {message}";
    }

    public void ShowInteractionPrompt(bool show)
    {
        if (interactionPrompt != null)
            interactionPrompt.SetActive(show);
    }
}