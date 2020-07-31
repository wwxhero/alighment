using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	protected ObjMobile m_thisObj;

	protected void Start () {
		m_thisObj = GetComponent<ObjMobile>();
	}



	public void Apply()
	{
		m_thisObj.Apply();
	}

	public void Reset()
	{
		m_thisObj.Reset();
	}

	public void LogOut()
	{
		Debug.Log("Controller::LogOut");
	}
}
