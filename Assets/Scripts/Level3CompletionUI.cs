using UnityEngine;

public class Level3CompletionUI : MonoBehaviour
{
    [SerializeField] private GameObject completionPanel;

    void Start()
    {
        if (completionPanel != null)
            completionPanel.SetActive(false);
    }

    public void ShowCompletion()
    {
        if (completionPanel != null)
            completionPanel.SetActive(true);
    }

    // Optional replay
    public void ReplayLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
