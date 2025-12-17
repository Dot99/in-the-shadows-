using UnityEngine;

public class ObjectManipulator : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 200f;
    public float rotationSmoothness = 10f;

    [Header("Movement")]
    public float moveSpeed = 0.5f;
    public float moveSmoothness = 10f;

    private Vector3 targetRotation;
    private Vector3 targetPosition;

    void Start()
    {
        targetRotation = transform.eulerAngles;
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!Input.GetMouseButton(0))
            return;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //MOVE object
        if (Input.GetKey(KeyCode.LeftShift) && SaveManager.GetUnlockedPuzzle() > 2)
        {
            targetPosition += new Vector3(
                mouseX * moveSpeed,
                mouseY * moveSpeed,
                0f
            );
        }
        //VERTICAL rotation (X axis)
        else if (Input.GetKey(KeyCode.LeftControl) && SaveManager.GetUnlockedPuzzle() > 1)
        {
            targetRotation.x += mouseY * rotationSpeed * Time.deltaTime;
        }
        //HORIZONTAL rotation (Y axis)
        else
        {
            targetRotation.y -= mouseX * rotationSpeed * Time.deltaTime;
        }
        targetRotation.x = Mathf.Repeat(targetRotation.x, 360f);
        targetRotation.y = Mathf.Repeat(targetRotation.y, 360f);
        targetRotation.z = Mathf.Repeat(targetRotation.z, 360f);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.Euler(targetRotation),
            Time.deltaTime * rotationSmoothness
        );
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * moveSmoothness
        );
    }
}
