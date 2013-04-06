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
		if(theCollider.gameObject.name == "Player")
		{
			theCollider.gameObject.SendMessage("SetVelocity", normal*30);
			Debug.Log(normal);
		}
		else if(theCollider.rigidbody)
		{
			theCollider.rigidbody.AddForce(normal*500);
			//Debug.DrawRay(transform.position, normal, Color.cyan, 1000);
		}
	}
	
	// Update is called once per frame
	public override void Update()
	{
	
	}
}
