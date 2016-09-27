using System.Collections;
using UnityEngine;

public class Trigger2D : MonoBehaviour
{
	[SerializeField]
	protected LayerMask triggerLayers;
	
	public Interactable[] targets = new Interactable[1];

	[Tooltip("Triggers when someone enters, without waiting to be called.")]
	public bool autoTrigger;

	[SerializeField]
	protected int signal = 1;

	[Tooltip("Keeps triggering while someone is inside the volume.")]
	public bool retriggerOnStay;
	
	[HideInInspector, Tooltip("Time in seconds before triggering again.")]
	public float triggerRate = 1f;

	protected bool someoneInside;

	protected float retriggerTimer;

	void Start()
	{
		if (GetComponent<Collider2D>())
		{
			GetComponent<Collider2D>().isTrigger = true;
		}
		else
		{
			Debug.Log("No collider volume found on " + name);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (targets.Length == 0 ||
			!autoTrigger ||
			(1 << (col.gameObject.layer) & triggerLayers) == 0)
		{
			return;
		}

		foreach (Interactable target in targets)
		{
			if (target)
			{
				SendSignal(target, signal);
			}
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (targets.Length == 0 ||
			!autoTrigger ||
			(1 << (col.gameObject.layer) & triggerLayers) == 0)
		{
			return;
		}

		if (retriggerOnStay)
		{
			retriggerTimer += Time.deltaTime;

			if (retriggerTimer >= triggerRate)
			{
				foreach (Interactable target in targets)
				{
					if (target)
					{
						SendSignal(target, signal);
					}
				}

				retriggerTimer = 0f;
			}
		}

		someoneInside = true;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (!autoTrigger ||
			(1 << (col.gameObject.layer) & triggerLayers) == 0)
		{
			return;
		}

		foreach (Interactable target in targets)
		{
			SendSignal(target, 0);
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