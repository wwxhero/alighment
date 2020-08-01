using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorCmp : MonoBehaviour {

	const float radian2deg = 180f/3.1416f;
	public ObjMobile [] m_elements;
	public void Report()
	{
		int n_elements = m_elements.Length;
		int n_elementsm = n_elements - 1;
		for (int i = 0; i < n_elementsm; i ++)
		{
			for (int j = i + 1; j < n_elements; j ++)
			{
				ReportError(m_elements[i], m_elements[j]);
			}
		}
	}

	static void ReportError(ObjMobile e1, ObjMobile e2)
	{
		Quaternion q1 = e1.transform.rotation;
		Quaternion q2 = e2.transform.rotation;
		float error_q_cos_half =  q1.w*q2.w
								+ q1.x*q2.x
								+ q1.y*q2.y
								+ q1.z*q2.z;
		float error_q = Mathf.Acos(error_q_cos_half) * 2 * radian2deg;
		Vector3 error_p = e1.transform.position - e2.transform.position;
        float error_d = error_p.magnitude;
		string strError = string.Format("Comparing between {0} and {1}", e1.name_t, e2.name_t);
		strError += string.Format("\n\trotation: {0, 5:#.00} degrees", error_q);
		strError += string.Format("\n\tpostion: {0, 5:#.00} units", error_d);
		Debug.Log(strError);
	}
}
