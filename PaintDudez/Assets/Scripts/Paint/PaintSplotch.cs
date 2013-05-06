using UnityEngine;
using System.Collections;

public abstract class PaintSplotch
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
		Debug.Log(theCollider.gameObject.tag);
		if(theCollider.gameObject.tag == "Bubble")
		{
			AudioSource.PlayClipAtPoint(WorldGlobal.audioClips["clean"], theCollider.transform.position, 8.0f);
			
			GameObject.Destroy(theCollider.gameObject);
			GameObject.Destroy(myObject);
		}
		if(myObject == theCollider.gameObject)
			return;
		
	}
	
	public virtual void DeEnactPaint(Collider theCollider)
	{
		//Debug.Log("left paint");
		if(theCollider.name == "CubePlayer")
			theCollider.gameObject.SendMessage("SetBehavior", "Walk", SendMessageOptions.DontRequireReceiver);
	}
	
	// Update is called once per frame
	public virtual void Update()
	{
		
	}
}
