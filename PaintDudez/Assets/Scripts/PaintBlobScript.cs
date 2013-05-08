using UnityEngine;
using System.Collections;

public class PaintBlobScript : MonoBehaviour
{
	PaintStruct myPaint;
	// Use this for initialization
	void Start()
	{
		if(myPaint.paintType == typeof(CleanSplotch))
			gameObject.rigidbody.gameObject.audio.collider.gameObject.gameObject.gameObject.tag = "Bubble";
	}
	
	
	void setMyPaint(PaintStruct paintIN)
	{
		myPaint = paintIN;
		renderer.material = myPaint.material;
		renderer.material.color = myPaint.ballColor;
	}
	
	void OnCollisionEnter(Collision col)
	{
//		if(col.gameObject.tag == "Paint")
		//Debug.Log("Name: " + col.gameObject.name);
		if(col.gameObject.name == "splatterDecal")
		{
			Debug.Log(col.gameObject);
		}
		if(col.gameObject.layer != 8)
		{
			ContactPoint contact = col.contacts[0];
			Ray ray = new Ray(contact.point + contact.normal, -contact.normal);
			Debug.DrawLine(contact.point + contact.normal, contact.point + contact.normal*3, Color.cyan, 1000);
			RaycastHit hit;
			//LayerMask lm = ~(1 << 8);
			if (Physics.Raycast(ray, out hit, 100))
			{
				//Debug.DrawRay(hit.point, hit.normal, Color.red, 1000);

				GameObject decal = Instantiate(WorldGlobal.Prefabs["splatterDecal"], hit.point + (contact.normal * 0.001f), Quaternion.FromToRotation(Vector3.up, contact.normal)) as GameObject;
				//decal.transform.localScale = new Vector3(Random.Range(1.0f, 4.0f), decal.transform.localScale.y, Random.Range(1.0f, 4.0f));
				decal.transform.localScale = new Vector3(2, decal.transform.localScale.y, 2);
				//decal.transform.parent = contact.otherCollider.transform;
				
				AudioSource tempSource = audio;
				tempSource.pitch = 2.0f;
				audio.PlayOneShot(WorldGlobal.audioClips["splat"]);
				
				decal.SendMessage("SetNormal", contact.normal);
				decal.SendMessage("SetSplotch", myPaint.paintType);
			}
			
//			foreach (ContactPoint contact in col.contacts)
//			{
//				Ray ray = new Ray(contact.point + contact.normal, -contact.normal);
//				//Debug.DrawRay(contact.point + contact.normal, contact.normal, Color.black, 1000);
//				RaycastHit hit;
//				//ray.origin = ray.GetPoint(100);
//				
//				
//	            //Debug.DrawRay(contact.point, contact.normal, Color.white, 1000000);
//				GameObject decal = Instantiate(PaintShooter.splatter, contact.point + (contact.normal * 0.001f), Quaternion.FromToRotation(Vector3.up, contact.normal)) as GameObject;
//				decal.transform.localScale = new Vector3(Random.Range(0.7f, 2.0f), Random.Range(0.7f, 2.0f), 1);
//				decal.transform.parent = contact.otherCollider.transform;
//				decal.SendMessage("SetNormal", contact.normal);
//				
//				
//	
//	        }
	
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		//This is hacky code for right now.
		if(myPaint.paintType == typeof(GrowSplotch))
			gameObject.renderer.material.color = WorldGlobal.currentColor;
	}
}
