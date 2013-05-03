using UnityEngine;
using System.Collections;

public class GreenSplotch : PaintSplotch
{	
	public GreenSplotch(GameObject go)
		: base(go)
	{
		go.renderer.material.color = Color.green;
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		base.EnactPaint(theCollider);
		if(theCollider.rigidbody && theCollider.name != "CubePlayer" && theCollider.tag != "Paint")
		{
			Debug.Log(theCollider);
			theCollider.rigidbody.isKinematic = true;
			//theCollider.rigidbody.velocity = Vector3.zero;
			//theCollider.rigidbody.angularVelocity = Vector3.zero;
			//theCollider.transform.parent = myObject.transform;
		}
		
	}
	
	public override void DeEnactPaint (Collider theCollider)
	{
		base.DeEnactPaint (theCollider);
		if(theCollider.name != "CubePlayer" && theCollider.tag != "Paint")
			theCollider.rigidbody.isKinematic = false;
		base.DeEnactPaint (theCollider);
	}
	
	// Update is called once per frame
	public override void Update()
	{
		//if(myCollide) myCollide.rigidbody.velocity = Vector3.zero;
	}
}
