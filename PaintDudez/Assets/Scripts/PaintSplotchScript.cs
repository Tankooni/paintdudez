using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaintSplotchScript : MonoBehaviour
{
	MeshFilter mf;
	Vector3 normal;
	PaintSplotch paint = null;
	
	// Use this for initialization
	void Start()
	{
		mf = GetComponent<MeshFilter>();
		if(paint == null)
			paint = new BlueSplotch(gameObject);
		paint.normal = normal;
	}
	
	void SetSplotch(Type t)
	{
		paint = (PaintSplotch)Activator.CreateInstance(t, new object[]{gameObject});
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(paint != null)
			paint.EnactPaint(col);
	}
	
	void OnTriggerExit(Collider col)
	{
		if(paint != null)
			paint.DeEnactPaint(col);
	}
	
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(collider.transform.position, collider.transform.localScale);
	}
	
	// Update is called once per frame
	void Update()
	{
		if(paint != null)
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
