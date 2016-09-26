using System.Collections;
using UnityEngine;

public class Raycaster2D : MonoBehaviour
{
	[SerializeField]
	private LayerMask raycastLayers;

	[SerializeField]
	float yOffset;

	[SerializeField]
	float verticalRayLength = 0.2f;

	[SerializeField]
	private int groundRays = 3;

	[SerializeField]
	private bool debugDrawRays;

	private bool isHitting;
	private RaycastHit2D rayHit;

	public bool IsHitting { get { return isHitting; } }
	public RaycastHit2D RayHit { get { return rayHit; } }
	
	void Start()
	{

	}
	
	void Update()
	{
		float xScale = transform.localScale.x;
		float yScale = transform.localScale.y;

		float xDivision = xScale / (groundRays - 1);

		float leftEdge = transform.position.x - (xScale / 2);
		float startPosY = transform.position.y - (yScale / 2) + yOffset;

		if (groundRays == 1)
		{
			xDivision = xScale;
			leftEdge = transform.position.x;
		}

		if (groundRays > 0)
		{
			for (int i = 0; i < groundRays; i++)
			{
				float startPosX = leftEdge + (i * xDivision);

				Vector3 startPos = new Vector3(startPosX, startPosY);

				rayHit = Physics2D.Raycast(startPos, Vector2.down, verticalRayLength, raycastLayers);
				
				if (rayHit.transform)
				{
					isHitting = true;
				}
				else
				{
					isHitting = false;
				}

				if (debugDrawRays)
				{
					Debug.DrawRay(startPos, Vector3.down * verticalRayLength);
				}
			}
		}
	}
}