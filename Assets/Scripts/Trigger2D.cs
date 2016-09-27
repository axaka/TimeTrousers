using System.Collections;
using UnityEngine;

public class Trigger2D : MonoBehaviour
{
	[SerializeField]
	protected LayerMask triggerLayers;

	[SerializeField]
	protected Interactable target;

	[SerializeField]
	protected bool useButton;

	[SerializeField]
	protected int signal = 1;

	[SerializeField]
	protected bool retriggerBeforeExit;

	[SerializeField]
	protected float triggerRate = 1f;

	protected bool someoneInside;

	float retriggerTimer;

	public bool getUseButton { get { return useButton; } }
	public Interactable getTarget { get { return target; } }

	void Start()
	{

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if ((1 << (col.gameObject.layer) & triggerLayers) == 0 ||
			useButton ||
			!target)
		{
			return;
		}

		SendSignal(target);
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if ((1 << (col.gameObject.layer) & triggerLayers) == 0 ||
			useButton ||
			!target)
		{
			return;
		}

		if (retriggerBeforeExit)
		{
			retriggerTimer += Time.deltaTime;

			if (retriggerTimer >= triggerRate)
			{
				SendSignal(target);
				retriggerTimer = 0f;
			}
		}

		someoneInside = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if ((1 << (col.gameObject.layer) & triggerLayers) == 0 ||
			useButton)
		{
			return;
		}

		someoneInside = false;
	}

	protected virtual void SendSignal(Interactable target, int newSignal = -1)
	{
		if (newSignal == -1)
		{
			newSignal = signal;
		}

		target.ReceiveSignal(newSignal);
	}

	void Update()
	{
		
	}
}