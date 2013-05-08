using UnityEngine;
using System.Collections;
using MainGameComponents;

public class BananaSplotch : PaintSplotch
{	
	public BananaSplotch(GameObject go)
		: base(go)
	{
		go.renderer.material.color = Color.yellow;
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		base.EnactPaint(theCollider);
	}
	
	public override void DeEnactPaint (Collider theCollider)
	{
		base.DeEnactPaint (theCollider);
	}
	
	public override void Update()
	{
		GameObject b = GameObject.Instantiate(WorldGlobal.Prefabs["banana"], myObject.transform.position + normal.normalized, Quaternion.identity) as GameObject;
		b.rigidbody.AddForce(-normal * 10);
	}
}
