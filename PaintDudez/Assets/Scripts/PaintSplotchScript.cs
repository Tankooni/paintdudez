using UnityEngine;
using System.Collections;

public class PaintSplotchScript : MonoBehaviour
{
	MeshFilter mf;
	// Use this for initialization
	void Start()
	{
		mf = GetComponent<MeshFilter>();
	}
	
	void OnTriggerEnter(Collider col)
	{
		
	}
	
	// Update is called once per frame
	void Update()
	{
		foreach(Vector3 poin in mf.mesh.vertices)
		{
			
			Debug.DrawLine(transform.localToWorldMatrix.MultiplyPoint3x4(poin), poin+Vector3.one, Color.blue);
		}
	}
}
