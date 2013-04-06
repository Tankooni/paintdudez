using UnityEngine;
using System.Collections;

public class RedSplotch : PaintSplotch
{
	public RedSplotch(GameObject go)
		: base(go)
	{
		color = Color.red;
	}
	
	public override void EnactPaint (Collider theCollider)
	{
	}
	

	public override void Update()
	{
	
	}
}
