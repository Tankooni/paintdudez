using UnityEngine;
using System.Collections;

public class CleanSplotch : PaintSplotch
{	
	public CleanSplotch(GameObject go)
		: base(go)
	{
		go.renderer.material.color = new Color(0,0,0,.5f);
		GameObject.Destroy(go);
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		base.EnactPaint(theCollider);
//		if(theCollider.tag == "Paint")
//		{
//			Debug.Log("REMOVEPAINT");
//			GameObject.Destroy(theCollider.gameObject);
//			GameObject.Destroy(myObject);
//			
//		}
		
	}
	
	public override void DeEnactPaint (Collider theCollider)
	{
		base.DeEnactPaint (theCollider);
	}
	
	// Update is called once per frame
	public override void Update()
	{
		//if(myCollide) myCollide.rigidbody.velocity = Vector3.zero;
	}
}
