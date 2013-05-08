using UnityEngine;
using System.Collections;

public class GravityPaint : PaintSplotch {

	public GravityPaint(GameObject go) : base(go)
	{
		go.renderer.material.color = new Color(0.211f, 0, 0.16f);//Dark Tyrian Purplse
		go.collider.transform.localScale = new Vector3(go.collider.transform.localScale.x, go.collider.transform.localScale.y * 12.5f, go.collider.transform.localScale.z);
		//go.collider.transform.localPosition = new Vector3(go.collider.transform.localPosition.x, go.collider.transform.localPosition.y + 2.0f, go.collider.transform.localPosition.z);
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		base.EnactPaint(theCollider);
	}
	
	public override void DeEnactPaint (Collider theCollider)
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
