using UnityEngine;
using System.Collections;

public class GravityPaint : PaintSplotch
{

	public GravityPaint(GameObject go)
		: base(go)
	{
		go.renderer.material.color = new Color(0.211f, 0, 0.16f);//Dark Tyrian Purplse
		colliderScale = 30;
		
		//go.collider.transform.localPosition = new Vector3(go.collider.transform.localPosition.x, go.collider.transform.localPosition.y + 2.0f, go.collider.transform.localPosition.z);
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		base.EnactPaint(theCollider);
		
		if(theCollider.gameObject.name == "CubePlayer")
		{
			theCollider.gameObject.SendMessage("SetBehavior", new object[]{"GravPaint", normal});
		}
	}
	
	public override void DeEnactPaint (Collider theCollider)
	{
		base.DeEnactPaint(theCollider);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
