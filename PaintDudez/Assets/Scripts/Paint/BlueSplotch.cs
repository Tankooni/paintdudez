using UnityEngine;
using System.Collections;

public class BlueSplotch : PaintSplotch
{
	public BlueSplotch(GameObject go)
		: base(go)
	{
		go.renderer.material.color = Color.blue;
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		if(theCollider.gameObject.name == "CubePlayer")
		{
			//theCollider.gameObject.SendMessage("SetVelocity", normal*30);
			theCollider.gameObject.SendMessage("SetBehavior", "BluePaint", SendMessageOptions.DontRequireReceiver);
			Debug.Log(normal);
		}
		else if(theCollider.rigidbody)
		{
			theCollider.rigidbody.AddForce(normal*500);
			//Debug.DrawRay(transform.position, normal, Color.cyan, 1000);
		}
	}
	
	public override void DeEnactPaint (Collider theCollider)
	{
		
	}
	
	// Update is called once per frame
	public override void Update()
	{
	
	}
}
