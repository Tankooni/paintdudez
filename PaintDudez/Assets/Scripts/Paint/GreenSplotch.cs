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
		if(theCollider.rigidbody && theCollider.name != "Player")
		{
			theCollider.rigidbody.velocity = Vector3.zero;
			theCollider.rigidbody.angularVelocity = Vector3.zero;
			theCollider.rigidbody.useGravity = false;
			//theCollider.transform.parent = myObject.transform;
		}
		
	}
	
	
	
	// Update is called once per frame
	public override void Update()
	{
		//if(myCollide) myCollide.rigidbody.velocity = Vector3.zero;
	}
}
