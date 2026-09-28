using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Escenas")]
    [SerializeField] private string gameSceneName = "Level_01_Street";

    public void OnPlayClicked()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnOptionsClicked()
    {
        Debug.Log("Opciones - pendiente de implementar");
    }

    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}