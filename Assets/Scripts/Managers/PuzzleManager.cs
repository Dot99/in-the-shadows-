using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
	public MultiObjectValidator multiValidator;
	public GameObject successPanel;
	public int puzzleIndex;
	public float validationInterval = 1f;

	private bool solved = false;
	private float timer;
	private bool isSolved = false;
	

	void Start()
	{
		solved = false;
		isSolved = false;
		timer = 0f;

		if (successPanel != null)
			successPanel.SetActive(false);
		GameState.instance.PuzzleIndex = puzzleIndex;
	}

	void Update()
	{
		if (solved)
			return;

		timer += Time.deltaTime;
		if (timer >= validationInterval)
		{
			timer = 0f;
			if (!isSolved && multiValidator != null)
				isSolved = multiValidator.IsPuzzleSolved();

			if (isSolved)
				SolvePuzzle();
		}
	}


	void SolvePuzzle()
	{
		solved = true;
		successPanel.SetActive(true);

		if (!GameState.IsTestMode)
		{
			int unlocked = SaveManager.GetUnlockedPuzzle();
			if (puzzleIndex >= unlocked)
				SaveManager.SetUnlockedPuzzle(puzzleIndex + 1);
		}

		// Trigger animation here if you want
	}
}
