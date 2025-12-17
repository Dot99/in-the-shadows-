using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessMenu : MonoBehaviour
{
    public void LoadNextPuzzle()
    {
        SceneManager.LoadScene(SaveManager.GetUnlockedPuzzle() + 1);
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
