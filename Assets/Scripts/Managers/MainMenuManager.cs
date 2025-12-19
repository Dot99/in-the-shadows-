using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button newGameButton;
    public Button continueButton;
    public Button testModeButton;

    private void Start()
    {
        // Disable Continue if no save exists
        continueButton.interactable = SaveManager.HasSave();
    }

    public void NewGame()
    {
        GameState.IsTestMode = false;
        SaveManager.ResetSave();
        SaveManager.SetUnlockedPuzzle(0);
        SceneManager.LoadScene("PuzzleSelect");
    }

    public void ContinueGame()
    {
        GameState.IsTestMode = false;
        SceneManager.LoadScene("PuzzleSelect");
    }

    public void TestMode()
    {
        GameState.IsTestMode = true;
        SceneManager.LoadScene("PuzzleSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
