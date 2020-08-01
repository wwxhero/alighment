#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ErrorCmp))]
public class ErrorCmpEdit : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Report", GUILayout.Width(80)))
		{
			ErrorCmp errorCmp = (ErrorCmp)target;
			errorCmp.Report();
		}
		GUILayout.EndHorizontal();

	}
};
#endif