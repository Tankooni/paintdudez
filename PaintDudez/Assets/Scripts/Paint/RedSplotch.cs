using UnityEngine;
using System.Collections;
using MainGameComponents;

public class RedSplotch : PaintSplotch
{
	public myMoveVars charVals = null;
	float incAmount;
	
	public RedSplotch(GameObject go)
		: base(go)
	{
		go.renderer.material.color = Color.red;
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		base.EnactPaint(theCollider);
		if(theCollider.gameObject.name == "CubePlayer")
		{
			theCollider.gameObject.SendMessage("GetVel", this);
			theCollider.gameObject.SendMessage("SetBehavior", "RedPaint",SendMessageOptions.DontRequireReceiver);
			
			// lol :)
			float velMag = charVals.Vel.magnitude / new Vector3(charVals.maxHSpeed, charVals.maxVUpSpeed, charVals.maxHSpeed).magnitude;
			
			//Debug.Log("velmag: " + velMag);
			//incAmount = 0.25f;
			
			AudioSource.PlayClipAtPoint(WorldGlobal.audioClips["speedPaint"], theCollider.transform.position, 4.0f * velMag);
		}
	}
	
	public override void DeEnactPaint (Collider theCollider)
	{
		base.DeEnactPaint (theCollider);
		
		if (theCollider.gameObject.name == "CubePlayer")
		{
			//incAmount = 0;
		}
	}
	
	public override void Update()
	{
	}
}
