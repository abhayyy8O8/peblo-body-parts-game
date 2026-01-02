using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string level1SceneName = "Level 1 – Big Body Builders";

    public void OnPlayPressed()
    {
        SceneManager.LoadScene(level1SceneName);
    }
}
