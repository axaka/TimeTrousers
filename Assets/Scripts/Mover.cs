using System.Collections;
using UnityEngine;

public class Mover : Interactable
{
	[Header("Mover")]
	public Transform targetTransform;
	
	[HideInInspector]
	public Vector3 direction = Vector3.up;

	public Vector3 offset;

	public bool easeIn;

	[HideInInspector]
	public float distance = 1f;

	public float speed = 1f;

	protected Vector3 startPos;
	protected float finalDistance;
	protected Vector3 nextPos;

	protected override void Start ()
	{
		base.Start();

		startPos = transform.position;
		finalDistance = distance;

		nextPos = GetNextPos();
	}

	IEnumerator GoToTarget()
	{

		OnBeginMove();

		while (Vector3.Distance(transform.position, nextPos) > 0.001f)
		{
			if (targetTransform)
			{
				nextPos = targetTransform.position;
			}

			if (easeIn)
			{
				transform.position = Vector3.Lerp(transform.position, nextPos, Time.deltaTime * speed);
			}
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, nextPos, Time.deltaTime * speed);
			}

			yield return new WaitForEndOfFrame();
		}

		transform.position = nextPos;
		OnReachTarget();
	}

	protected virtual void OnBeginMove()
	{
		nextPos = GetNextPos();
	}

	protected virtual void OnReachTarget()
	{
		
	}

	Vector3 GetNextPos()
	{
		//Mover implementation limited to 2 positions.
		//Use 'Train.cs' for multiple positions
		Vector3 pos = startPos + direction.normalized * finalDistance;
		pos += offset;

		if (Vector3.Distance(transform.position, startPos) > 0.01f)
		{
			pos = startPos;
		}

		return pos;
	}

	public override void ReceiveSignal(int signal)
	{
		base.ReceiveSignal(signal);

		//Signal 0 typically means 'revert', so someone exited a trigger. Fire only if toggle is disabled.
		if (toggle && signal == 0)
		{
			return;
		}
		
		StopAllCoroutines();
		StartCoroutine(GoToTarget());
	}

	void Update ()
	{
		
	}
}