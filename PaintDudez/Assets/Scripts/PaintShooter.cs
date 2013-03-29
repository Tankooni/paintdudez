using UnityEngine;
using System.Collections.Generic;

public class PaintShooter : MonoBehaviour
{
	//List<PhysicMaterial> physicsMats = new List<PhysicMaterial>();
	public PhysicMaterial [] physicsMats;
	public int currentMat = 0;
	Camera cam;
	public static GameObject splatter = null;
	GameObject blob = null;
	GameObject ball = null;
	
	#region Picker vars
	Transform pickObj = null;
	float pickDist;
	RaycastHit pickHit;
	#endregion
	
	
	// Use this for initialization
	void Start()
	{
		//physicsMats = Resources.LoadAll("PhysMats") as PhysicMaterial;
		cam = GetComponentInChildren<Camera>() as Camera;
		PaintShooter.splatter = Resources.Load("Prefabs/splatterDecal") as GameObject;
		blob = Resources.Load("Prefabs/paintBlob") as GameObject;
		ball = Resources.Load("Prefabs/SphereZ") as GameObject;
		
	}
	
	
	
	// Update is called once per frame
	void Update()
	{
		if(Input.GetMouseButtonDown(1))
		{
			Screen.lockCursor = !(Screen.lockCursor);
		}
		
		if(Input.GetMouseButtonDown(0))
		{
			ShootPaint();
		}
		
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			Debug.Log("1");
		}
		
		if(Input.GetKey(KeyCode.E))
		{
			PickObject();
		}
		if(Input.GetKeyUp(KeyCode.E))
			DropObject();
		
		if(Input.GetKeyDown(KeyCode.Q))
		{
			
		}
	}
	
	void PickObject()
	{
		Ray pickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(!pickObj)
		{
			if(Physics.Raycast(pickRay, out pickHit) && pickHit.transform.tag == "Pick")
			{
				Debug.DrawLine(pickRay.origin, pickHit.point, Color.magenta);
				if (pickHit.rigidbody)
				{
					pickHit.rigidbody.velocity = Vector3.zero;
				}
				pickObj = pickHit.transform;
				pickDist = Vector3.Distance(pickObj.position, Camera.main.transform.position);
			}
		}
		else
		{
			pickObj.position = pickRay.GetPoint(pickDist);
			pickHit.rigidbody.velocity = Vector3.zero;
		}
		
		Debug.Log(pickHit.rigidbody.velocity);
	}
	
	void DropObject()
	{
		pickObj = null;
	}
	
	void ShootPaint()
	{
		Vector3 dir = cam.transform.forward;
		
		GameObject paint = Instantiate(blob, transform.position + transform.TransformDirection(Vector3.forward), Quaternion.identity) as GameObject;
		paint.rigidbody.AddForce(cam.transform.TransformDirection(Vector3.forward)*700);

		Debug.Log(cam.transform.forward);
//		RaycastHit hit;
//		
//		
//		if(Physics.Raycast(transform.position, dir, out hit, 1000))
//		{
//			GameObject decal;
////			Debug.Log("Hit: " + hit.rigidbody.name);
////			Debug.Log(cam.transform.position);
//			Debug.DrawLine(transform.position, hit.point, Color.red, 10);
//			
//			decal = Instantiate(splatter, hit.point + (hit.normal * 0.001f), Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
//			decal.transform.localScale = new Vector3(Random.Range(0.7f, 2.0f), Random.Range(0.7f, 2.0f), 1);
//			decal.transform.parent = hit.rigidbody.gameObject.transform;
//			decal.renderer.material.color = Color.blue;
//			
//			hit.rigidbody.collider.material = physicsMats[1];
//			hit.rigidbody.AddForce(new Vector3(0,500,0));
//		}
	}
}
