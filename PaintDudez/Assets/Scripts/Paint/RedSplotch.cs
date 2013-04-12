using UnityEngine;
using System.Collections;

public class RedSplotch : PaintSplotch
{
	public RedSplotch(GameObject go)
		: base(go)
	{
		go.renderer.material.color = Color.red;
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		
	}
	

	public override void Update()
	{
	
	}
}
