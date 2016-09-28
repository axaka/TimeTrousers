//using System.Collections;
using UnityEngine;

public class UseTriggerExample : MonoBehaviour
{
	[SerializeField]
	KeyCode useButton;
	Trigger2D triggerVolume;

	void Start ()
	{
		
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.GetComponent<Trigger2D>())
		{
			triggerVolume = col.GetComponent<Trigger2D>();
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.GetComponent<Trigger2D>())
		{
			triggerVolume = null;
		}
	}

	void Update ()
	{
		if (triggerVolume)
		{
			if (Input.GetKeyDown(useButton))
			{
				triggerVolume.FireTargets();
			}
		}
	}
}