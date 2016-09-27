//using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Trigger2D))]
public class Trigger2DEditor : Editor
{
	float triggerRate;

	void OnSceneGUI()
	{
		Trigger2D trigger = (Trigger2D)target;

		if (trigger.targets.Length > 0)
		{
			foreach (Interactable triggerTarget in trigger.targets)
			{
				if (triggerTarget)
				{
					Debug.DrawLine(trigger.transform.position, triggerTarget.transform.position, Color.red);
				}
			}
		}
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Trigger2D trigger = (Trigger2D)target;

		if (trigger.retriggerOnStay)
		{
			triggerRate = EditorGUILayout.FloatField("Trigger rate", triggerRate);

			if (triggerRate < 0)
			{
				triggerRate = 0;
			}

			trigger.triggerRate = triggerRate;
		}
	}
}