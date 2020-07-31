#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ControllerAuto))]
public class ControllerAutoEdit : Editor
{
	delegate void MTD_CONTROLLER();
    MTD_CONTROLLER[] m_methods;
    string[]		 m_methodsnames;
    void OnEnable()
    {
        Controller controller = (Controller)target;
        m_methods = new MTD_CONTROLLER[]
        {
              new MTD_CONTROLLER(controller.Apply)
            , new MTD_CONTROLLER(controller.Reset)
            , new MTD_CONTROLLER(controller.LogOut)
        };

        m_methodsnames = new string [] 
        {
        	  "Apply"
        	, "Reset"
        	, "Log out"
        };
    }
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
				
		GUILayout.BeginHorizontal();
		for (int i_btn = 0; i_btn < m_methods.Length; i_btn ++)
		{
			const int width_u = 10;
			string capi = m_methodsnames[i_btn];

			if (GUILayout.Button(capi, GUILayout.Width(width_u*capi.Length)))
            {
                m_methods[i_btn]();
            }
		}
		//int nFrame = EditorGUILayout.IntSlider("Frame"
		//						, nFrameProp.intValue
		//						, nFrameMinProp.intValue
		//						, nFrameMaxProp.intValue);
		//if (nFrame != nFrameProp.intValue)
		//{
		//	nFrameProp.intValue = nFrame;
		//	serializedObject.ApplyModifiedProperties();
		//}
		GUILayout.EndHorizontal();
	}
};
#endif