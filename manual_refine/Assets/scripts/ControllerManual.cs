using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManual : Controller {

	[HideInInspector] public bool m_registering = false;
	bool m_registeringm = false;

	void Update()
	{
		bool startRC = (m_registering && !m_registeringm);
		bool stopRC = (!m_registering && m_registeringm);
		if (startRC)
			m_thisObj.StartRigidTransform();
		else if(stopRC)
			m_thisObj.StopRigidTransform();
		m_registeringm = m_registering;
	}
}
