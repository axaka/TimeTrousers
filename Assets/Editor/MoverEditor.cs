//using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Mover), true)]
public class MoverEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Mover mover = (Mover)target;

		if (!mover.targetTransform)
		{
			mover.direction = EditorGUILayout.Vector3Field("Direction", mover.direction);
			mover.distance = EditorGUILayout.FloatField("Distance", mover.distance);
		}
	}
}