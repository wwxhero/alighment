using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class RigidTransformation
{
    Matrix4x4 m_forward;
    public RigidTransformation(Matrix4x4 [] delta)
	{
        //fixme: tobe complete
        m_forward = delta[1] * delta[0].inverse;
	}

	public Matrix4x4 forward {
		get
        {
            return m_forward;
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
