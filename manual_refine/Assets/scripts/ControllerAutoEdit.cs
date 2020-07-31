#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ControllerAuto))]
public class ControllerAutoEdit : ControllerEdit
{
	SerializedProperty registerProp;
	new void OnEnable()
	{
		base.OnEnable();
		registerProp = serializedObject.FindProperty ("m_registered");
	}
	public override void OnInspectorGUI()
	{
		if (registerProp.boolValue)
			base.OnInspectorGUI();
		else
		{
			OnInspectorGUI_Editor();
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("Register", GUILayout.Width(80)))
			{
				ControllerAuto controller = (ControllerAuto)target;
				controller.Register();
			}
			GUILayout.EndHorizontal();
		}
	}
};
#endif