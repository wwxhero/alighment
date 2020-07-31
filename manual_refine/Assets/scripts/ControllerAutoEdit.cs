#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ControllerAuto))]
public class ControllerAutoEdit : ControllerEdit
{

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		//add edit controls for automatic edit

	}
};
#endif