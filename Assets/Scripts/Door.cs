//using System.Collections;
using UnityEngine;

public class Door : Mover
{
	[Header("Door")]
	public bool useHeightAsDistance;


	protected override void Start ()
	{
		base.Start();
	}

	void UpdateDistance()
	{
		if (useHeightAsDistance)
		{
			finalDistance = transform.lossyScale.y;
		}
		else
		{
			finalDistance = distance;
		}
	}

	void Update ()
	{
		InvokeRepeating("UpdateDistance", 0f, 1f);
	}
}