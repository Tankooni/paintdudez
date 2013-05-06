using UnityEngine;
using System.Collections;

public class GrowSplotch : PaintSplotch
{
	GameObject myBlockHolder = null;
	GameObject myBlock = null;
	
	public GrowSplotch(GameObject go)
		: base(go)
	{
		//go.renderer.material.color = Color.green;
		myBlockHolder = MonoBehaviour.Instantiate(WorldGlobal.Prefabs["growthBlock"], go.transform.position, go.transform.rotation) as GameObject;
		myBlock = myBlockHolder.transform.FindChild("Cube").gameObject;
		myBlock.renderer.material.color = new Color(Random.value, Random.value, Random.value, .9f);
		//myBlock.transform.parent = go.transform;
		//MonoBehaviour.Destroy(go);
	}
	
	public override void EnactPaint (Collider theCollider)
	{
	}
	
	public override void DeEnactPaint (Collider theCollider)
	{
	}
	
	// Update is called once per frame
	public override void Update()
	{
		
		myBlock.renderer.material.color = new Color(Random.value, Random.value, Random.value, 0.9f);
		myBlockHolder.transform.localScale = new Vector3(myBlockHolder.transform.localScale.x, myBlockHolder.transform.localScale.y + 1.01f, myBlockHolder.transform.localScale.z);
		//Debug.Log("I'M GROWING!");
		//if(myCollide) myCollide.rigidbody.velocity = Vector3.zero;
	}
}
