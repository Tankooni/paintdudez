using UnityEngine;
using System.Collections;

public class BlueSplotch : PaintSplotch
{
	public BlueSplotch(GameObject go)
		: base(go)
	{
		go.renderer.material.color = Color.blue;
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		base.EnactPaint(theCollider);
		if(theCollider.gameObject.name == "CubePlayer")
		{
			//theCollider.gameObject.SendMessage("SetVelocity", normal*30);
			theCollider.gameObject.SendMessage("SetBehavior", new object[]{"BluePaint", normal});
			
			theCollider.gameObject.audio.pitch = 1.0f;
			theCollider.gameObject.audio.volume = 1.0f;
			theCollider.gameObject.audio.PlayOneShot(WorldGlobal.audioClips["bounce"]);
			//theCollider.gameObject.SendMessage("MoveUpdate");
			//theCollider.gameObject.SendMessage("SetNormal", normal);
			//Debug.Log("Normal: " + normal);
		}
		else if(theCollider.rigidbody)
		{
			base.DeEnactPaint(theCollider);
			theCollider.rigidbody.velocity = Vector3.Reflect(theCollider.rigidbody.velocity, normal.normalized);
			
			//if (theCollider.rigidbody.mass
			AudioSource.PlayClipAtPoint(WorldGlobal.audioClips["ballBounce"], theCollider.transform.position, 4.0f);
			//Debug.Log("BlobNormal: " + normal);
			//Debug.DrawRay(transform.position, normal, Color.cyan, 1000);
		}
		
	}
	
//	public override void DeEnactPaint (Collider theCollider)
//	{
//		
//	}
	
	// Update is called once per frame
	public override void Update()
	{
	
	}
}
