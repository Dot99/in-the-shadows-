using UnityEngine;

public class RotationValidator : MonoBehaviour
{
    public Transform playerObject;
    public Transform referenceObject;

    [Tooltip("Degrees of tolerance")]
    public float angleTolerance = 3f;

    public bool IsValid()
    {
        float angle = Quaternion.Angle(
            playerObject.rotation,
            referenceObject.rotation
        );

        return angle <= angleTolerance;
    }
}
