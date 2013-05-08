using UnityEngine;
using System.Collections.Generic;

public class GreenSplotch : PaintSplotch
{	
	public List<GameObject> myHeldObjs = new List<GameObject>();
	public GreenSplotch(GameObject go)
		: base(go)
	{
		go.renderer.material.color = Color.green;
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		base.EnactPaint(theCollider);
		if(theCollider.rigidbody && theCollider.name != "CubePlayer" && theCollider.tag != "Paint" && theCollider.tag != "Bubble")
		{
			Debug.Log(theCollider.tag);
			theCollider.rigidbody.isKinematic = true;
			myHeldObjs.Add(theCollider.gameObject);
			//theCollider.rigidbody.velocity = Vector3.zero;
			//theCollider.rigidbody.angularVelocity = Vector3.zero;
			//theCollider.transform.parent = myObject.transform;
		}
		
	}
	
	public override void DeEnactPaint (Collider theCollider)
	{
		base.DeEnactPaint (theCollider);
		if(theCollider.name != "CubePlayer" && theCollider.tag != "Paint" && theCollider.tag != "Bubble")
		{
			theCollider.rigidbody.isKinematic = false;
			myHeldObjs.Remove(theCollider.gameObject);
		}
	}
	
	// Update is called once per frame
	public override void Update()
	{
		//if(myCollide) myCollide.rigidbody.velocity = Vector3.zero;
	}
	
	public override void Death()
	{
		for(int i = myHeldObjs.Count-1; i >= 0; i--)
		{
			myHeldObjs[i].rigidbody.isKinematic = false;
			myHeldObjs.Remove(myHeldObjs[i]);
		}
	}
}
