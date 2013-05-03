using UnityEngine;
using System.Collections.Generic;

public class PaintShooter : MonoBehaviour
{
	//List<PhysicMaterial> physicsMats = new List<PhysicMaterial>();
	//public PhysicMaterial [] physicsMats;
	//public int currentMat = 0;
	Camera cam;
	public static GameObject splatter = null;
	GameObject blob = null;
	GameObject ball = null;
	
	Dictionary<string, PaintSplotch> PaintList = new Dictionary<string, PaintSplotch>();
	
	#region Picker vars
	Transform pickObj = null;
	float pickDist;
	RaycastHit pickHit;
	#endregion
	
	Transform floatyBall = null;
	Transform paintSpawn = null;
	GameObject coreObject = null;
	GameObject coreInstance = null;
	GameObject painGun = null;
	
	bool isPaintGunActive = false;
	
	// Use this for initialization
	void Start()
	{
		//physicsMats = Resources.LoadAll("PhysMats") as PhysicMaterial;
		//cam = GetComponentInChildren<Camera>() as Camera;
		cam = Camera.main;
		PaintShooter.splatter = Resources.Load("Prefabs/splatterDecal") as GameObject;
		blob = Resources.Load("Prefabs/paintBlob") as GameObject;
		ball = Resources.Load("Prefabs/SphereZ") as GameObject;

		painGun = cam.transform.Find("PaintGun").gameObject;
		
		floatyBall = painGun.transform.Find("b_gun_root/b_gun_ball");
		paintSpawn = painGun.transform.Find("b_gun_root/b_gun_Shooter");
		coreObject = Resources.Load("Prefabs/PaintGunCore") as GameObject;
		coreInstance = Instantiate(coreObject, floatyBall.position, floatyBall.rotation) as GameObject;
		coreInstance.transform.parent = floatyBall;
		coreInstance.renderer.material.color = Color.blue;
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
			coreInstance.renderer.material.color = Color.blue;
		}
		
		if(Input.GetKey(KeyCode.E))
			PickObject();
		if(Input.GetKeyUp(KeyCode.E))
			DropObject();
		if(Input.GetKey(KeyCode.B))
		{
			painGun.SetActive(true);
		}
		
//		if(Input.GetKey(KeyCode.Q))
//		{
//			Instantiate(ball, transform.position + transform.TransformDirection(Vector3.forward), Quaternion.identity);
//		}
		if( Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			if(pickObj != null)
			{
				pickDist += Input.GetAxis("Mouse ScrollWheel") * 3;
			}
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
				if (pickHit.rigidbody && !pickHit.rigidbody.isKinematic)
				{
					pickHit.rigidbody.velocity = Vector3.zero;
					pickHit.rigidbody.angularVelocity = Vector3.zero;
				}
				pickObj = pickHit.transform;
				pickDist = Vector3.Distance(pickObj.position, Camera.main.transform.position);
			}
		}
		else
		{
			
			pickObj.rigidbody.MovePosition(pickRay.GetPoint(pickDist));
			if (pickHit.rigidbody && !pickHit.rigidbody.isKinematic)
			{
				pickHit.rigidbody.velocity = Vector3.zero;
			}
		}
	}
	
	void DropObject()
	{
		pickObj = null;
	}
	
	void ShootPaint()
	{
//		Vector3 dir = cam.transform.forward;
//		
//		GameObject paint = Instantiate(blob, transform.position + transform.TransformDirection(Vector3.forward), Quaternion.identity) as GameObject;
//		//paint.SendMessage("SetPaint", );
//		Physics.IgnoreCollision(paint.collider, collider);
//		paint.rigidbody.AddForce(cam.transform.TransformDirection(Vector3.forward)*700);
		
//		Vector3 dir = paintSpawn.forward;
		GameObject paint = Instantiate(blob, paintSpawn.position, Quaternion.identity) as GameObject;
		paint.renderer.material.color = Color.blue;
		Physics.IgnoreCollision(paint.collider, collider);
		paint.rigidbody.AddForce(paintSpawn.TransformDirection(Vector3.left)*700);
		
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
