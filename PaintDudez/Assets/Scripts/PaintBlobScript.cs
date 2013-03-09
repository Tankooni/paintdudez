using UnityEngine;
using System.Collections;

public class PaintBlobScript : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
	
	}
	
	void OnCollisionStay(Collision col)
	{
		foreach (ContactPoint contact in col.contacts)
		{
            Debug.DrawRay(contact.point, contact.normal, Color.white, 1000000);
			GameObject decal = Instantiate(PaintShooter.splatter, contact.point + (contact.normal * 0.001f), Quaternion.FromToRotation(Vector3.up, contact.normal)) as GameObject;
			decal.transform.localScale = new Vector3(Random.Range(0.7f, 2.0f), Random.Range(0.7f, 2.0f), 1);
			decal.transform.parent = contact.otherCollider.transform;
			decal.renderer.material.color = Color.blue;
        }

		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}
}
