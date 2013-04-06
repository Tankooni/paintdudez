using UnityEngine;
using System.Collections;

public class GreenSplotch : PaintSplotch
{	
	public GreenSplotch(GameObject go)
		: base(go)
	{
		color = Color.green;
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		
	}
	// Update is called once per frame
	public override void Update()
	{
	
	}
}
