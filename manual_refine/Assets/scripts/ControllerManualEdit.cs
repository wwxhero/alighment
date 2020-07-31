#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ControllerManual))]
public class ControllerManualEdit : Editor
{
	SerializedProperty m_recordingManulProp;
	SerializedProperty nFrameMaxProp;
	SerializedProperty nFrameMinProp;
	SerializedProperty nFrameProp;

	void OnEnable()
	{
		// Setup the SerializedProperties.
		m_recordingManulProp = serializedObject.FindProperty ("m_recordingmanul");
        Debug.Assert(null != m_recordingManulProp);
		//nFrameMinProp = serializedObject.FindProperty("c_nFrameBase");
		//nFrameMaxProp = serializedObject.FindProperty("c_nFrameMax");
		//nFrameProp = serializedObject.FindProperty("m_nFrame");
	}
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		GUILayout.BeginHorizontal();
		string [] btns = {"Start recording", "Stop recording"};
		int i_btn = m_recordingManulProp.boolValue ? 1 : 0;
		if (GUILayout.Button(btns[i_btn], GUILayout.Width(120)))
		{
			m_recordingManulProp.boolValue = !m_recordingManulProp.boolValue;
			serializedObject.ApplyModifiedProperties ();
		}
		GUILayout.EndHorizontal();
		//GUILayout.BeginHorizontal();
		//int nFrame = EditorGUILayout.IntSlider("Frame"
		//						, nFrameProp.intValue
		//						, nFrameMinProp.intValue
		//						, nFrameMaxProp.intValue);
		//if (nFrame != nFrameProp.intValue)
		//{
		//	nFrameProp.intValue = nFrame;
		//	serializedObject.ApplyModifiedProperties();
		//}
		//GUILayout.EndHorizontal();
	}
};
#endif