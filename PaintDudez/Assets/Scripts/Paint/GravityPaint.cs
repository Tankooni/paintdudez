using UnityEngine;
using System.Collections.Generic;

public class GravityPaint : PaintSplotch
{
	GameObject myParts;
	List<GameObject> myStuff = new List<GameObject>();
	Ray myDir;
	public GravityPaint(GameObject go)
		: base(go)
	{
		go.renderer.material.color = new Color(0.211f, 0, 0.16f);//Dark Tyrian Purplse
		colliderScale = 30;
		
		myParts = GameObject.Instantiate(WorldGlobal.Prefabs["GravParticleSystem"], go.transform.position, Quaternion.identity) as GameObject;
		//Debug.Log(go.transform.rotation);
		
		myParts.transform.rotation = Quaternion.LookRotation(go.transform.TransformDirection(Vector3.up));
		myParts.transform.localScale = new Vector3(go.transform.localScale.x, go.transform.localScale.z, go.transform.localScale.y);
		myParts.transform.parent = go.transform;
		
		
		
		//myParts = go.AddComponent<ParticleSystem>();
		//myParts.particleEmitter.
		//go.collider.transform.localPosition = new Vector3(go.collider.transform.localPosition.x, go.collider.transform.localPosition.y + 2.0f, go.collider.transform.localPosition.z);
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		myDir = new Ray(myObject.transform.position, normal);
		Debug.Log(myDir);
		Debug.DrawRay(myDir.origin, myDir.direction, Color.red,10000);
		base.EnactPaint(theCollider);
		
		if(theCollider.gameObject.name == "CubePlayer")
		{
			theCollider.gameObject.SendMessage("SetBehavior", new object[]{"GravPaint", normal});
		}
		else if(theCollider.rigidbody && theCollider.gameObject.tag != "Paint")
		{
			theCollider.rigidbody.isKinematic = true;
			theCollider.rigidbody.transform.position = (myDir.GetPoint(Vector3.Distance(theCollider.gameObject.transform.position, myObject.transform.position)));
				//pickObj.rigidbody.MovePosition(pickRay.GetPoint(pickDist));
			// = Vector3.Project(theCollider.gameObject.transform.position, normal.normalized);
			myStuff.Add(theCollider.gameObject);
		}
	}

	public static float DistanceToLine(Ray ray, Vector3 point)
	{
	    return Vector3.Cross(ray.direction, point - ray.origin).magnitude;
	}

	public override void DeEnactPaint (Collider theCollider)
	{
		if(theCollider.rigidbody && theCollider.gameObject.tag != "Paint")
		{
			base.DeEnactPaint(theCollider);
			theCollider.rigidbody.isKinematic = false;
			myStuff.Remove(theCollider.gameObject);
		}
	}
	
	// Update is called once per frame
	public override void Update()
	{
		foreach(GameObject gom in myStuff)
		{
			gom.rigidbody.position = gom.gameObject.transform.position + normal.normalized * Time.fixedDeltaTime;
			//gom.rigidbody.position = myDir.GetPoint(1000);
			//gom.transform.position = ;
			
		}
		//Debug.Log(myParts.transform.rotation);
	}
}
