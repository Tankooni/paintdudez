using UnityEngine;
using System.Collections;

public class PaintSplotchScript : MonoBehaviour
{
	MeshFilter mf;
	Vector3 normal;
	PaintSplotch paint;
	
	// Use this for initialization
	void Start()
	{
		mf = GetComponent<MeshFilter>();
		paint = new BlueSplotch(gameObject);
		paint.normal = normal;
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(paint != null)
			paint.EnactPaint(col);
	}
	
	void OnTriggerExit(Collider col)
	{
		paint.DeEnactPaint(col);
	}
	
	// Update is called once per frame
	void Update()
	{
		paint.Update();
//		foreach(Vector3 poin in mf.mesh.vertices)
//		{
//			
//			Debug.DrawLine(transform.localToWorldMatrix.MultiplyPoint3x4(poin), poin+Vector3.one, Color.blue);
//		}
	}
	
	void SetNormal(Vector3 vec)
	{
		normal = vec;
	}
}
