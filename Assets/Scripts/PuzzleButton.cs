using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleButton : MonoBehaviour
{
    public int puzzleIndex;
    public string puzzleSceneName;

    public GameObject lockIcon;
    public Image background;

    public Color lockedColor = Color.gray;
    public Color unlockedColor = Color.white;
    public Color solvedColor = Color.green;

    void Start()
    {
        bool testMode = GameState.IsTestMode;
        int unlocked = testMode ? int.MaxValue : SaveManager.GetUnlockedPuzzle();

        if (puzzleIndex > unlocked)
        {
            lockIcon.SetActive(true);
            background.color = lockedColor;
            GetComponent<Button>().interactable = false;
        }
        else
        {
            lockIcon.SetActive(false);
            GetComponent<Button>().interactable = true;
            background.color = puzzleIndex < unlocked ? solvedColor : unlockedColor;
        }
    }

    public void LoadPuzzle()
    {
        GameState.instance.PuzzleIndex = puzzleIndex;
        SceneManager.LoadScene(puzzleSceneName);
    }
}
