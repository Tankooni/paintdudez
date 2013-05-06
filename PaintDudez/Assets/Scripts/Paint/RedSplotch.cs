using UnityEngine;
using System.Collections;
using MainGameComponents;

public class RedSplotch : PaintSplotch
{
	public myMoveVars charVals = null;
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
			
			AudioSource.PlayClipAtPoint(WorldGlobal.audioClips["speedPaint"], theCollider.transform.position, velMag);
		}
	}
	
	public override void Update()
	{
	}
}
