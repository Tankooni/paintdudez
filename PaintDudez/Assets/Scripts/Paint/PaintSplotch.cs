using UnityEngine;
using System.Collections;

public class PaintSplotch
{
	public Color color = Color.white;
	public Vector3 normal;
	protected GameObject myObject;
	
	public PaintSplotch(GameObject go)
	{
		myObject = go;
	}
	
	public virtual void EnactPaint(Collider theCollider)
	{
		if(myObject == theCollider.gameObject)
			return;
	}
	
	public virtual void DeEnactPaint(Collider theCollider)
	{
		
	}
	
	// Update is called once per frame
	public virtual void Update()
	{
		
	}
}
