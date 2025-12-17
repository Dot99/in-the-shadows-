using UnityEngine;

[System.Serializable]
public class PuzzleObject
{
	public Transform playerObject;       // Object the player rotates/moves
	public Transform referenceObject;    // Hidden reference object
	public float positionTolerance = 1f; // How close in units to be “correct”
	public float rotationTolerance = 5f;   // Degrees difference allowed
	[HideInInspector] public bool solved = false; // Lock once solved
}

public class MultiObjectValidator : MonoBehaviour
{
	[Header("Objects in order")]
	public PuzzleObject[] puzzleObjects;

	public bool IsPuzzleSolved()
	{
		for (int i = 0; i < puzzleObjects.Length; i++)
		{
			PuzzleObject obj = puzzleObjects[i];

			if (obj.solved) continue;

			float distance = Vector3.Distance(
				obj.playerObject.position,
				obj.referenceObject.position
			);
			float angle = Quaternion.Angle(
				obj.playerObject.rotation,
				obj.referenceObject.rotation
			);
			if (distance <= obj.positionTolerance && angle <= obj.rotationTolerance)
			{
				obj.solved = true;
				obj.playerObject.position = obj.referenceObject.position;
				obj.playerObject.rotation = obj.referenceObject.rotation;
			}
			else
			{
				return false;
			}
		}
		return true;
	}
}
