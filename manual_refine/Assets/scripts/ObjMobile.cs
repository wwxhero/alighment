using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RigidTransformation
{
	Matrix4x4 m_forward;
	Matrix4x4 m_inverse;

	static bool IsRigid(ref Matrix4x4 m)
	{
		bool is_rigid = m.ValidTRS();
		if (is_rigid)
		{
			Vector3 scale = m.lossyScale;
			for (int i = 0; i < 3 && is_rigid; i ++)
			{
				float d = scale[i] - 1.0f;
				is_rigid = (d < float.Epsilon && d > -float.Epsilon);
			}
		}
		return is_rigid;
	}

	static void Inverse(ref Matrix4x4 m)
	{
		Matrix4x4 r_inv = m.transpose;
		Vector3 t = new Vector3(  m[0, 3]
								, m[1, 3]
								, m[2, 3]);
		Vector3 t_prime = r_inv * (-t);
		for (int i_r = 0; i_r < 3; i_r ++)
		{
			for (int i_c = 0; i_c < 3; i_c ++)
			{
				m[i_r, i_c] = r_inv[i_r, i_c];
			}
		}
		m[0, 3] = t_prime[0];
		m[1, 3] = t_prime[1];
		m[2, 3] = t_prime[2];
	}

	public RigidTransformation(Matrix4x4 [] delta)
	{
		Debug.Assert(IsRigid(ref delta[0])
					&& IsRigid(ref delta[1]));
		//fixme: tobe complete
		m_forward = delta[1] * delta[0].inverse;
		m_inverse = m_forward;
		Inverse(ref m_inverse);
	}

	public Matrix4x4 forward {
		get
		{
			return m_forward;
		}
	}

	public Matrix4x4 inverse
	{
		get
		{
			return m_inverse;
		}
	}

};

public class ObjMobile : MonoBehaviour {

	// Use this for initialization
	List<RigidTransformation> m_rigids = new List<RigidTransformation>();
	int m_iRT = 0;
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void Register(Matrix4x4 [] l2ws, int n_l2ws)
	{
		m_rigids.Clear();
		Matrix4x4 [] delta = new Matrix4x4[2]
		{
			  transform.localToWorldMatrix
			, l2ws[0]
		};
		m_rigids.Add(new RigidTransformation(delta));
		for (int i_l2w = 1; i_l2w < n_l2ws; i_l2w ++)
		{
			delta[0] = delta[1];
			delta[1] = l2ws[i_l2w];
			m_rigids.Add(new RigidTransformation(delta));
		}
		m_iRT = 0;
	}

	public void Apply()
	{
		Matrix4x4 T = Matrix4x4.identity;
		int n_t = m_rigids.Count;
		for (; m_iRT < n_t; m_iRT ++)
		{
			T = m_rigids[m_iRT].forward * T;
		}
		m_iRT --;
		Matrix4x4 l2w = T * transform.localToWorldMatrix;
		Vector3 x = new Vector3(  l2w[0, 0]
								, l2w[1, 0]
								, l2w[2, 0]);
		Vector3 y = new Vector3(  l2w[0, 1]
								, l2w[1, 1]
								, l2w[2, 1]);
		Vector3 z = new Vector3(  l2w[0, 2]
								, l2w[1, 2]
								, l2w[2, 2]);
		Vector3 t = new Vector3(  l2w[0, 3]
								, l2w[1, 3]
								, l2w[2, 3]);
		transform.up = y;
		transform.forward = z;
		transform.position = t;
	}

	public void Reset()
	{
		Matrix4x4 T = Matrix4x4.identity;
		for (; m_iRT > -1; m_iRT --)
		{
			T = m_rigids[m_iRT].inverse * T;
		}
		m_iRT ++;
		Matrix4x4 l2w = T * transform.localToWorldMatrix;
		Vector3 x = new Vector3(  l2w[0, 0]
								, l2w[1, 0]
								, l2w[2, 0]);
		Vector3 y = new Vector3(  l2w[0, 1]
								, l2w[1, 1]
								, l2w[2, 1]);
		Vector3 z = new Vector3(  l2w[0, 2]
								, l2w[1, 2]
								, l2w[2, 2]);
		Vector3 t = new Vector3(  l2w[0, 3]
								, l2w[1, 3]
								, l2w[2, 3]);
		transform.up = y;
		transform.forward = z;
		transform.position = t;
	}
}
