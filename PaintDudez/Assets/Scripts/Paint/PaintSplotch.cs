using UnityEngine;
using System.Collections;

public abstract class PaintSplotch
{
	public Color color = Color.white;
	public Vector3 normal;
	protected GameObject myObject;
	protected GameObject myScalarObject;
	protected float colliderScale = 1;
	
	public GameObject ScalarObject
	{
		get
		{
			return myScalarObject;
		}
		set
		{
			myScalarObject = value;
			myScalarObject.transform.localScale = new Vector3(myScalarObject.transform.localScale.x, myScalarObject.transform.localScale.y * colliderScale, myScalarObject.transform.localScale.z);
		}
	}
	
	public PaintSplotch(GameObject go)
	{
		myObject = go;
	}
	
	public virtual void EnactPaint(Collider theCollider)
	{		
		//Debug.Log(theCollider.gameObject.tag);
		if(theCollider.gameObject.tag == "Bubble")
		{
			AudioSource.PlayClipAtPoint(WorldGlobal.audioClips["erasePaint"], theCollider.transform.position, 8.0f);
			
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
