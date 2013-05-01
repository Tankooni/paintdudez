using UnityEngine;
using System.Collections;

public class PaintBlobScript : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
		
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag != "Paint")
		{
			ContactPoint contact = col.contacts[0];
			Ray ray = new Ray(contact.point + contact.normal, -contact.normal);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000))
			{
				Debug.DrawRay(hit.point, hit.normal, Color.red, 1000);

				GameObject decal = Instantiate(PaintShooter.splatter, hit.point + (contact.normal * 0.001f), Quaternion.FromToRotation(Vector3.up, contact.normal)) as GameObject;
				decal.transform.localScale = new Vector3(Random.Range(1.0f, 4.0f), decal.transform.localScale.y, Random.Range(1.0f, 4.0f));
				//decal.transform.parent = contact.otherCollider.transform;
				decal.SendMessage("SetNormal", contact.normal);
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
	
	}
}
