using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	//[HideInInspector] public bool m_recordingmanul = false;
	//bool m_recordingmanulPrevious = false;
	//Matrix4x4 m_TM = Matrix4x4.identity;        //the transformation for manual operations
	//Matrix4x4 m_MM = Matrix4x4.identity;        //the local to world matrix
	//Matrix4x4 m_MMinv = Matrix4x4.identity;
	//const float c_epsilonR = 0.0001f;
	//const float c_epsilonT = 0.01f;
	// Use this for initialization
	void Start () {
		//m_MM = transform.localToWorldMatrix;
	}
	
	// Update is called once per frame
	void Update () {
		// if (m_recordingmanul)
		// {
		// 	float err_rot, err_tran;
		// 	Matrix4x4 m_prime = transform.localToWorldMatrix;
		// 	Error(ref m_MM, ref m_prime, out err_rot, out err_tran);
		// 	bool moved = (err_rot > c_epsilonR
		// 				|| err_tran > c_epsilonT);
		// 	if (moved)
		// 	{
		// 		Matrix4x4 t = m_prime * m_MMinv;
		// 		m_TM = t * m_TM;
		// 		m_MM = m_prime;
		// 		m_MMinv = transform.worldToLocalMatrix;
		// 	}
		
		// }

		// bool stop_recording = (m_recordingmanulPrevious
		// 					&& !m_recordingmanul);
		
		// if (stop_recording)
		// {
		// 	Matrix4x4 M_A_Prime = m_MM;
		// 	Matrix4x4 M_A = m_TM.inverse * M_A_Prime;

		// 	Debug.LogFormat("{0}: local to world matrix:", name);
		// 	Debug.Log(M_A.ToString());
		// 	DOFs_6(ref M_A, out r_x, out r_y, out r_z, out t_x, out t_y, out t_z);
		// 	Debug.LogFormat("6 DOFs: \n\trotation {0, 7:#.0000} {1, 7:#.0000} {2, 7:#.0000}\n\ttranlation {3, 7:#.0000} {4, 7:#.0000} {5, 7:#.0000}"
		// 					, r_x, r_y, r_z
		// 					, t_x, t_y, t_z);
			
		// 	Debug.LogFormat("{0}': local to world matrix:", name);
		// 	Debug.Log(M_A_Prime.ToString());
		// 	DOFs_6(ref M_A_Prime, out r_x, out r_y, out r_z, out t_x, out t_y, out t_z);
		// 	Debug.LogFormat("6 DOFs: \n\trotation {0, 7:#.0000} {1, 7:#.0000} {2, 7:#.0000}\n\ttranlation {3, 7:#.0000} {4, 7:#.0000} {5, 7:#.0000}"
		// 					, r_x, r_y, r_z
		// 					, t_x, t_y, t_z);
		// }
		
		// m_recordingmanulPrevious = m_recordingmanul;
	}

	//static void Error(ref Matrix4x4 m, ref Matrix4x4 m_prime, out float e_r, out float e_t)
	//{

	//}

	public void Apply()
	{
		Debug.Log("Controller::Apply");
	}

	public void Reset()
	{
		Debug.Log("Controller::Reset");
	}

	public void LogOut()
	{
		Debug.Log("Controller::LogOut");
	}
}
