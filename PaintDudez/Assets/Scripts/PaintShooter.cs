using UnityEngine;
using System.Collections.Generic;
using System;

public class paintStruct
{
	public Type paintColor;
	public Color ballColor;
	
	public paintStruct(Type paintColorIN, Color ballColorIN)
	{
		paintColor = paintColorIN;
		ballColor = ballColorIN;
	}
}


public class PaintShooter : MonoBehaviour
{
	//List<PhysicMaterial> physicsMats = new List<PhysicMaterial>();
	//public PhysicMaterial [] physicsMats;
	//public int currentMat = 0;
	Camera cam;
	GameObject blob = null;
	GameObject ball = null;
	
	Dictionary<string, PaintSplotch> PaintList = new Dictionary<string, PaintSplotch>();
	
	//List of paint in the gun
	paintStruct[] ammoType = new paintStruct[10];
	
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
	
	paintStruct currentActivePaint;
	
	bool isPaintGunActive = false;
	
	
	// Use this for initialization
	void Start()
	{
		//Blue paint
		//ammoType.Add(new paintStruct(typeof(BlueSplotch), Color.blue));
		ammoType[0] = new paintStruct(typeof(BlueSplotch), Color.blue);
		ammoType[1] = new paintStruct(typeof(GreenSplotch), Color.green);
		ammoType[2] = new paintStruct(typeof(RedSplotch), Color.red);
		//Set the current active color to our inital paint (Blue paint)
		currentActivePaint = ammoType[0];
		
		//physicsMats = Resources.LoadAll("PhysMats") as PhysicMaterial;
		//cam = GetComponentInChildren<Camera>() as Camera;
		cam = Camera.main;
//		blob = Resources.Load("Prefabs/paintBlob") as GameObject;
//		ball = Resources.Load("Prefabs/SphereZ") as GameObject;
		blob = WorldGlobal.Prefabs["blob"];
		ball = WorldGlobal.Prefabs["ball"];

		painGun = cam.transform.Find("PaintGun").gameObject;
		
		floatyBall = painGun.transform.Find("b_gun_root/b_gun_ball");
		paintSpawn = painGun.transform.Find("b_gun_root/b_gun_Shooter");
		coreObject = WorldGlobal.Prefabs["gunCore"];
		coreInstance = Instantiate(coreObject, floatyBall.position, floatyBall.rotation) as GameObject;
		coreInstance.transform.parent = floatyBall;
		coreInstance.renderer.material.color = Color.blue;
	}
	
	
	
	// Update is called once per frame
	void Update()
	{
		Debug.Log("Gun Vel: " + rigidbody.velocity);
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
			currentActivePaint = ammoType[0];
			coreInstance.renderer.material.color = currentActivePaint.ballColor;
	    }
	    else if(Input.GetKeyDown(KeyCode.Alpha2))
	    {
			currentActivePaint = ammoType[1];
			coreInstance.renderer.material.color = currentActivePaint.ballColor;
	    }
	    else if(Input.GetKeyDown (KeyCode.Alpha3))
	    {
			currentActivePaint = ammoType[2];
			coreInstance.renderer.material.color = currentActivePaint.ballColor;
	    }
		else if(Input.GetKeyDown (KeyCode.Alpha4))
	    {
			if(ammoType[3] != null)
			{
				currentActivePaint = ammoType[3];
				coreInstance.renderer.material.color = currentActivePaint.ballColor;
			}
	    }
		
		if(Input.GetKey(KeyCode.E))
			PickObject();
		if(Input.GetKeyUp(KeyCode.E))
			DropObject();
		if(Input.GetKey(KeyCode.B))
		{
			painGun.SetActive(true);
		}
		
		if(Input.GetKey(KeyCode.Q))
		{
			Instantiate(ball, transform.position + transform.TransformDirection(Vector3.forward), Quaternion.identity);
		}
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
		paint.SendMessage("setMyPaint", currentActivePaint);
		paint.renderer.material.color = currentActivePaint.ballColor;
		Physics.IgnoreCollision(paint.collider, collider);
		paint.rigidbody.AddForce(paintSpawn.TransformDirection(Vector3.left)*700);
		
		AudioSource tempSource = audio;
		tempSource.pitch = 3.0f;
		tempSource.volume = 0.5f;
		tempSource.PlayOneShot(WorldGlobal.audioClips["shoot"]);
		
		//shootSfx.Play();
		
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
