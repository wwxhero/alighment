using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAuto : Controller {
	public Transform [] m_registrations;
	[HideInInspector] public bool m_registered;
	new void Start()
	{
		base.Start();
		m_registered = false;
	}

	public void Register()
	{
		if (!m_registered)
		{
			int n_ts = m_registrations.Length;
			if (n_ts > 0)
			{
				Matrix4x4[] l2ws = new Matrix4x4[n_ts];
				for (int i_ts = 0; i_ts < n_ts; i_ts++)
					l2ws[i_ts] = m_registrations[i_ts].localToWorldMatrix;
				m_thisObj.Register(l2ws, n_ts);
				m_registered = true;
			}
		}
	}
}
