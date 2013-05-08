using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaintSplotchScript : MonoBehaviour
{
	MeshFilter mf;
	Vector3 normal;
	PaintSplotch paint = null;
	GameObject myCollider;
	GameObject myColScalar;
	
	// Use this for initialization
	void Start()
	{
		mf = GetComponent<MeshFilter>();
		myColScalar = transform.Find("Bananas").gameObject;
		myCollider = myColScalar.transform.FindChild("Cooler").gameObject;
		if(paint == null)
			paint = new BlueSplotch(gameObject);
		paint.normal = normal;
		//myCollider.transform.localScale = gameObject.transform.localScale;
		myColScalar.transform.localScale = new Vector3(1, .5f, 1);
		myCollider.transform.localPosition = new Vector3(0,gameObject.transform.localScale.y/2,0);
		
//		myCollider.transform.localRotation = gameObject.transform.localRotation;
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
		//Gizmos.DrawWireCube(myCollider.transform.position, myCollider.transform.localScale);
	}
	
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
