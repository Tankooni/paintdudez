using UnityEngine;
using System.Collections.Generic;
using MainGameComponents;

public class BananaSplotch : PaintSplotch
{	
	public static List<GameObject> BananaList = new List<GameObject>();
	public static List<GameObject> Splotches = new List<GameObject>();
	
	public BananaSplotch(GameObject go)
		: base(go)
	{
		go.renderer.material.color = Color.yellow;
		BananaSplotch.Splotches.Add(go);
	}
	
	public override void EnactPaint (Collider theCollider)
	{
		base.EnactPaint(theCollider);
	}
	
	public override void DeEnactPaint (Collider theCollider)
	{
		base.DeEnactPaint (theCollider);
	}
	
	public override void Update()
	{
		if(BananaSplotch.BananaList.Count <= 500)
		{
			GameObject b = GameObject.Instantiate(WorldGlobal.Prefabs["banana"], myObject.transform.position, Quaternion.identity) as GameObject;
			b.rigidbody.AddForce(-normal * 10);
			BananaSplotch.BananaList.Add(b);
		}
		else
		{
			BananaSplotch.Splotches.Remove(myObject);
			GameObject.Destroy(myObject);
		}
		
		foreach(GameObject bo in BananaSplotch.BananaList)
			bo.renderer.material.color = new Color(Random.value, Random.value, Random.value);
	}
}
