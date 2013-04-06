using UnityEngine;
using System.Collections;

public class PaintSplotch
{
	public Color color = Color.white;
	public Vector3 normal;
	GameObject myObject;
	
	public PaintSplotch(GameObject go)
	{
		myObject = go;
	}
	
	public virtual void EnactPaint(Collider theCollider)
	{
		
	}
	
	// Update is called once per frame
	public virtual void Update()
	{
		
	}
}
