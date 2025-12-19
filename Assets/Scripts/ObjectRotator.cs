using UnityEngine;

public class ObjectManipulator : MonoBehaviour
{
	[Header("Rotation")]
	public float rotationSpeed = 200f;
	public float rotationSmoothness = 10f;

	[Header("Movement")]
	public float moveSpeed = 75f;
	public float moveSmoothness = 10f;

	[Header("Movement Limits")]
	public Vector3 minBounds = new Vector3(-20f, -20f, -20f);
	public Vector3 maxBounds = new Vector3(20f, 20f, 20f);

	private static ObjectManipulator activeObject;

	private Vector3 targetRotation;
	private Vector3 targetPosition;
	private Camera cam;
	private bool isLocked = false;

	void Start()
	{
		cam = Camera.main;
		targetRotation = transform.eulerAngles;
		targetPosition = transform.position;
	}

	void Update()
	{
		if(isLocked)
			return;

		HandleSelection();

		if (activeObject != this)
			return;

		HandleManipulation();
	}

	public void Lock()
	{
		isLocked = true;
	}


	void HandleSelection()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform == transform)
				{
					activeObject = this;
					targetRotation = transform.eulerAngles;
					targetPosition = transform.position;
				}
			}
		}

		if (Input.GetMouseButtonUp(0) && activeObject == this)
		{
			activeObject = null;
		}
	}

	void HandleManipulation()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");

		// MOVE (difficulty 3)
		if (Input.GetKey(KeyCode.LeftShift) && GameState.instance.PuzzleIndex > 1)
		{
			targetPosition += new Vector3(mouseX, mouseY, 0f)
							* moveSpeed * Time.deltaTime;

			targetPosition = new Vector3(
				Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x),
				Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y),
				Mathf.Clamp(targetPosition.z, minBounds.z, maxBounds.z)
			);
		}
		// VERTICAL rotation (difficulty 2+)
		else if (Input.GetKey(KeyCode.LeftControl) && GameState.instance.PuzzleIndex > 0)
		{
			targetRotation.x += mouseY * rotationSpeed * Time.deltaTime;
		}
		// HORIZONTAL rotation (difficulty 1+)
		else
		{
			targetRotation.y -= mouseX * rotationSpeed * Time.deltaTime;
		}

		targetRotation.x = Mathf.Repeat(targetRotation.x, 360f);
		targetRotation.y = Mathf.Repeat(targetRotation.y, 360f);

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
