#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ControllerManual))]
public class ControllerManualEdit : ControllerEdit
{
	SerializedProperty registering;

	protected new void OnEnable()
	{
		base.OnEnable();
		registering = serializedObject.FindProperty ("m_registering");
	}
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		GUILayout.BeginHorizontal();
		string [] btns = {"Start Registering", "Stop Registering"};
		int i_btn = registering.boolValue ? 1 : 0;
		if (GUILayout.Button(btns[i_btn], GUILayout.Width(160)))
		{
			registering.boolValue = !registering.boolValue;
			serializedObject.ApplyModifiedProperties ();
		}
		GUILayout.EndHorizontal();

	}
};
#endif