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
		renderer.material.color = Color.blue;
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name == "Player")
		{
			col.gameObject.SendMessage("SetVelocity", normal*30);
		}
		else if(col.rigidbody)
		{
			col.rigidbody.AddForce(normal*500);
			//Debug.DrawRay(transform.position, normal, Color.cyan, 1000);
		}
	}
			
	void OnCollisionEnter(Collision col)
	{
		col.rigidbody.AddForce(normal*1000);
	}
	
	// Update is called once per frame
	void Update()
	{
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
