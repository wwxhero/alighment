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

	public static void Inverse(ref Matrix4x4 m)
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

class RigidConfiguration
{
	Quaternion m_q;
	Vector3 m_t;
    Matrix4x4 m_forward;
    Matrix4x4 m_inverse;
	public RigidConfiguration(Quaternion q, Vector3 t)
	{
		m_q = q;
		m_t = t;
        m_forward = Matrix4x4.TRS(t, q, new Vector3(1f, 1f, 1f));
        m_inverse = m_forward;
        RigidTransformation.Inverse(ref m_inverse);
    }

    public void Apply(Transform transform)
    {
    	transform.rotation = m_q;
    	transform.position = m_t;
    }

    public Matrix4x4 localToWorldMatrix
    {
        get
        {
            return m_forward;
        }
    }


};

public class ObjMobile : MonoBehaviour {

	// Use this for initialization
	List<RigidTransformation> m_rigids = new List<RigidTransformation>();
	RigidConfiguration m_confi;
	int m_iRT = 0;

	bool m_registering = false;
	Matrix4x4 m_regDelta0 = Matrix4x4.identity;
	void Start () {
		m_confi = new RigidConfiguration(transform.rotation, transform.position);
	}

	// Update is called once per frame
	void Update () {
		if (!m_registering)
		{
			m_confi.Apply(transform);
		}
	}

	public string name_t {
		get
		{
			string name_suffix = name;
			for (int i = 0; i < m_iRT; i ++)
				name_suffix += "'";
			return name_suffix;
		}
	}
	public void Register(Matrix4x4 [] l2ws, int n_l2ws)
	{
		for (int i = m_rigids.Count - 1; i > m_iRT - 1; i --)
			m_rigids.RemoveAt(i);
		Matrix4x4 [] delta = new Matrix4x4[2]
		{
			  m_confi.localToWorldMatrix
			, l2ws[0]
		};
		m_rigids.Add(new RigidTransformation(delta));
		for (int i_l2w = 1; i_l2w < n_l2ws; i_l2w ++)
		{
			delta[0] = delta[1];
			delta[1] = l2ws[i_l2w];
			m_rigids.Add(new RigidTransformation(delta));
		}
	}

	public void StartRigidTransform()
	{
		m_registering = true;
		m_regDelta0 = m_confi.localToWorldMatrix;
	}

	public void StopRigidTransform()
	{
		m_registering = false;
		m_confi = new RigidConfiguration(transform.rotation, transform.position);
		for (int i = m_rigids.Count - 1; i > m_iRT - 1; i --)
			m_rigids.RemoveAt(i);
		Matrix4x4 [] delta = new Matrix4x4[2]
		{
			  m_regDelta0
			, m_confi.localToWorldMatrix
		};
		m_rigids.Add(new RigidTransformation(delta));
		m_iRT ++;
	}

	public void Apply()
	{
		Matrix4x4 T = Matrix4x4.identity;
		int n_t = m_rigids.Count;
		for (; m_iRT < n_t; m_iRT ++)
		{
			T = m_rigids[m_iRT].forward * T;
		}

		Matrix4x4 l2w = T * m_confi.localToWorldMatrix;
		Vector3 t = new Vector3(  l2w[0, 3]
								, l2w[1, 3]
								, l2w[2, 3]);
		m_confi = new RigidConfiguration(l2w.rotation, t);
		m_confi.Apply(transform);
	}

	public void Reset()
	{
		Matrix4x4 T = Matrix4x4.identity;
		for (m_iRT --; m_iRT > -1; m_iRT --)
		{
			T = m_rigids[m_iRT].inverse * T;
		}
		m_iRT ++;
		Matrix4x4 l2w = T * m_confi.localToWorldMatrix;
		Vector3 t = new Vector3(  l2w[0, 3]
								, l2w[1, 3]
								, l2w[2, 3]);
		m_confi = new RigidConfiguration(l2w.rotation, t);
		m_confi.Apply(transform);
	}

	public void LogOut()
	{
		Matrix4x4 T_i = Matrix4x4.identity;
		int i = m_iRT - 1;
		for (; i > -1; i --)
		{
			T_i = m_rigids[i].inverse * T_i;
		}

		Matrix4x4 l2w_i = T_i * m_confi.localToWorldMatrix;
		PrintOut(ref l2w_i, 0);
		int n_t = m_rigids.Count;
		for (i = 0; i < n_t; i ++)
		{
			l2w_i = m_rigids[i].forward * l2w_i;
			PrintOut(ref l2w_i, i + 1);
		}
	}

	public void Append(ObjMobile mobile)
	{
		var addi = mobile.m_rigids;
		for (int i = 0; i < addi.Count; i ++)
			m_rigids.Add(addi[i]);
	}

	void PrintOut(ref Matrix4x4 l2w, int i_trans)
	{
		string log = name;
		for (int i_suffix = 0; i_suffix < i_trans; i_suffix ++)
			log += "'";
		log += ":";
		log += "\nMatrix:";
		for (int i_r = 0; i_r < 4; i_r ++)
		{
			log += string.Format("\n\t{0,7:#.0000}\t{1,7:#.0000}\t{2,7:#.0000}\t{3,7:#.0000}"
									, l2w[i_r, 0], l2w[i_r, 1], l2w[i_r, 2], l2w[i_r, 3]);

		}
		log += "\nDOFs:";
		Vector3 euler = l2w.rotation.eulerAngles;
		log += string.Format("\n\trotation: {0,7:#.0000}\t{1,7:#.0000}\t{2,7:#.0000}", euler.x, euler.y, euler.z);
		log += string.Format("\n\ttranslation: {0,7:#.0000}\t{1,7:#.0000}\t{2,7:#.0000}", l2w[0, 3], l2w[1, 3], l2w[2, 3]);
		Debug.Log(log);
	}
}
