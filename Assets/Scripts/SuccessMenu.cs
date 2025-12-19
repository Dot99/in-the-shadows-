using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessMenu : MonoBehaviour
{
    [Tooltip("Scene name of the next puzzle")]
    public string nextPuzzleScene;

    public void LoadNextPuzzle()
    {
        if (!string.IsNullOrEmpty(nextPuzzleScene))
        {
            this.gameObject.SetActive(false);
            SceneManager.LoadScene(nextPuzzleScene);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
