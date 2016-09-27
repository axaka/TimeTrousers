using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
	[Header("Interactable")]
	public bool toggle;
	
	public UnityEvent onReceiveSignal;

	protected virtual void Start()
	{
		if (onReceiveSignal == null)
		{
			onReceiveSignal = new UnityEvent();
		}
	}

	public virtual void ReceiveSignal(int signal)
	{
		onReceiveSignal.Invoke();
	}

	void Update()
	{

	}
}