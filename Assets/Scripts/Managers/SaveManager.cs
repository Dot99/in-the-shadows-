using UnityEngine;

public static class SaveManager
{
    private const string PROGRESS_KEY = "UNLOCKED_PUZZLE";

    public static int GetUnlockedPuzzle()
    {
        return PlayerPrefs.GetInt(PROGRESS_KEY, 0);
    }

    public static void SetUnlockedPuzzle(int puzzleIndex)
    {
        PlayerPrefs.SetInt(PROGRESS_KEY, puzzleIndex);
        PlayerPrefs.Save();
    }

    public static bool HasSave()
    {
        return PlayerPrefs.HasKey(PROGRESS_KEY);
    }

    public static void ResetSave()
    {
        PlayerPrefs.DeleteKey(PROGRESS_KEY);
    }
}
