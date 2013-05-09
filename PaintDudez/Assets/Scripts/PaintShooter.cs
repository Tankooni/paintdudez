using UnityEngine;
using System.Collections.Generic;
using System;
using MainGameComponents;

public class PaintStruct
{
	public Type paintType;
	public Color ballColor;
	public Material material;
	
	public PaintStruct(Type paintTypeIN, Color ballColorIN, Material materialIN)
	{
		paintType = paintTypeIN;
		ballColor = ballColorIN;
		material = materialIN;
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
	PaintStruct[] ammoType = new PaintStruct[10];
	PaintStruct cleanAmmo = null;
	
	#region Picker vars
	Transform pickObj = null;
	float pickDist;
	RaycastHit pickHit;
	#endregion
	
	Transform floatyBall = null;
	Transform paintSpawn = null;
	GameObject coreObject = null;
	public GameObject coreInstance = null;
	public GameObject painGun = null;
	GameObject flashLight = null;
	
	PaintStruct currentActivePaint;
	
	bool isPaintGunActive = false;
	
	myMoveVars characterValues = null;
	
	// Use this for initialization
	void Start()
	{
		WorldGlobal.Narrator.shootsNLadders = this;

		ammoType[0] = new PaintStruct(typeof(BlueSplotch), Color.blue, WorldGlobal.Materials["default"]);
		//ammoType[1] = new PaintStruct(typeof(GreenSplotch), Color.green, WorldGlobal.Materials["default"]);
		//ammoType[2] = new PaintStruct(typeof(RedSplotch), Color.red, WorldGlobal.Materials["default"]);
		//ammoType[3] = new PaintStruct(typeof(GrowSplotch), WorldGlobal.currentColor, WorldGlobal.Materials["default"]);
		//ammoType[4] = new PaintStruct(typeof(GravityPaint), new Color(0.211f, 0, 0.16f), WorldGlobal.Materials["default"]);
		//ammoType[5] = new PaintStruct(typeof(BananaSplotch), Color.yellow, WorldGlobal.Materials["default"]);
		
		cleanAmmo = new PaintStruct(typeof(CleanSplotch), new Color(0,0,0,0), WorldGlobal.Materials["bubble"]);
		
		//Set the current active color to our inital paint (Blue paint)
		currentActivePaint = ammoType[0];
		
		//physicsMats = Resources.LoadAll("PhysMats") as PhysicMaterial;
		//cam = GetComponentInChildren<Camera>() as Camera;
		cam = Camera.main;
//		blob = Resources.Load("Prefabs/paintBlob") as GameObject;
//		ball = Resources.Load("Prefabs/SphereZ") as GameObject;
		blob = WorldGlobal.Prefabs["paintBlob"];
		ball = WorldGlobal.Prefabs["SphereZ"];

		painGun = cam.transform.Find("PaintGun").gameObject;
		
		floatyBall = painGun.transform.Find("b_gun_root/b_gun_ball");
		paintSpawn = painGun.transform.Find("b_gun_root/b_gun_Shooter");
		coreObject = WorldGlobal.Prefabs["PaintGunCore"];
		coreInstance = Instantiate(coreObject, floatyBall.position, floatyBall.rotation) as GameObject;
		coreInstance.transform.parent = floatyBall;
		coreInstance.renderer.material.color = currentActivePaint.ballColor;
		
		flashLight = new GameObject("Light");
		flashLight.AddComponent<Light>();
		flashLight.light.type = LightType.Spot;
		flashLight.light.range = 100;
		flashLight.light.spotAngle = 40;
		flashLight.light.intensity = 0.5f;
		flashLight.light.enabled = false;
		flashLight.transform.position = paintSpawn.position + Vector3.Scale(paintSpawn.TransformDirection(Vector3.left),paintSpawn.position/6);
		flashLight.transform.rotation = Quaternion.LookRotation(paintSpawn.TransformDirection(Vector3.left));
		flashLight.transform.parent = paintSpawn;
		
	}
	
	
	
	// Update is called once per frame
	void Update()
	{	
		if(currentActivePaint.paintType == typeof(GrowSplotch))
			coreInstance.renderer.material.color = WorldGlobal.currentColor;
		
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Screen.lockCursor = !(Screen.lockCursor);	
		}
		
		if(Input.GetKeyDown(KeyCode.F))
			flashLight.light.enabled = !flashLight.light.enabled;
		
		//Debug.Log("Gun Vel: " + rigidbody.velocity);
		if(Input.GetMouseButtonDown(1) && WorldGlobal.isReadyToFire)
		{
            if (pickObj == null)
            {
                ShootPaint(cleanAmmo);
            }
            else
            {
                //Add force to cube and send it flying
                pickObj.rigidbody.AddForce(cam.transform.TransformDirection(Vector3.forward) * 300);
                pickObj = null;
            }
		}
        if (Input.GetMouseButtonDown(0) && WorldGlobal.isReadyToFire)
        {
            if (pickObj == null)
            {
                ShootPaint(currentActivePaint);
            }
            else
            {
                //Add force to cube and send it flying
                //shootOBJ();
				//pickObj.rigidbody.AddForce(cam.transform.TransformDirection(Vector3.forward) * 300);
				pickObj.rigidbody.AddForce(cam.transform.TransformDirection(Vector3.forward) * 300 + (characterValues.Vel * 20));
         		pickObj = null;
            }
        }

		
		if(Input.GetKeyDown(KeyCode.Alpha1))
	    {
            if (ammoType[0] != null)
            {
                currentActivePaint = ammoType[0];
                coreInstance.renderer.material.color = currentActivePaint.ballColor;
            }
	    }
	    else if(Input.GetKeyDown(KeyCode.Alpha2))
	    {
            if (ammoType[1] != null)
            {
                currentActivePaint = ammoType[1];
                coreInstance.renderer.material.color = currentActivePaint.ballColor;
            }
	    }
	    else if(Input.GetKeyDown (KeyCode.Alpha3))
	    {
            if (ammoType[2] != null)
            {
                currentActivePaint = ammoType[2];
                coreInstance.renderer.material.color = currentActivePaint.ballColor;
            }
	    }
		else if(Input.GetKeyDown (KeyCode.Alpha4))
	    {
			if(ammoType[3] != null)
			{
				currentActivePaint = ammoType[3];
				coreInstance.renderer.material.color = currentActivePaint.ballColor;
			}
	    }
		else if(Input.GetKeyDown (KeyCode.Alpha5))
	    {
            if (ammoType[4] != null)
            {
                currentActivePaint = ammoType[4];
                coreInstance.renderer.material.color = currentActivePaint.ballColor;
            }
	    }
		else if(Input.GetKeyDown (KeyCode.Alpha6))
	    {
            if (ammoType[5] != null)
            {
                currentActivePaint = ammoType[5];
                coreInstance.renderer.material.color = currentActivePaint.ballColor;
            }
	    }

        if (Input.GetKeyDown(KeyCode.E))
        {
            toggleObject();
        }

		if(Input.GetKey(KeyCode.B))
		{
			painGun.SetActive(true);
		}

		if( Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			if(pickObj != null)
			{
                if ((pickDist >= 1.3) && (Input.GetAxis("Mouse ScrollWheel") < 0))
                {
                    Debug.Log("Out");
                    pickDist += Input.GetAxis("Mouse ScrollWheel") * 3;
                }
                else if (pickDist <= 3 && (Input.GetAxis("Mouse ScrollWheel") > 0))
                {
                    Debug.Log("In");
                    pickDist += Input.GetAxis("Mouse ScrollWheel") * 3;
                }
			}
		}

        //Hold object in place
        if (pickObj != null)
        {
            //Make a new raycast to where we are pointing
            Ray pickRay = new Ray(paintSpawn.position, paintSpawn.TransformDirection(Vector3.left));
            //Move the object we've picked up to the previously defined ray position
            pickObj.rigidbody.MovePosition(pickRay.GetPoint(pickDist));
            if (pickHit.rigidbody && !pickHit.rigidbody.isKinematic)
            {
                pickHit.rigidbody.velocity = Vector3.zero;
            }
        }
	}

    void toggleObject()
    {
        Ray pickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		//Ray pickRay = new Ray(paintSpawn.position, paintSpawn.TransformDirection(Vector3.left));
        if (!pickObj)
        {
            if (Physics.Raycast(pickRay, out pickHit, 2.5f) && pickHit.transform.tag == "Pick")
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
            pickObj = null;
        }
    }

    void addPaint(PaintStruct paintIN)
    {
        for (int x = 0; x <= ammoType.Length; x++)
        {
            if(ammoType[x] == null)
            {
                ammoType[x] = paintIN;
                //currentActivePaint = ammoType[x];
                break;
            }
        }
    }
	
	void SetCharValues(myMoveVars vvkafjshfs)
	{
		characterValues = vvkafjshfs;
	}	
	
	void ShootPaint(PaintStruct ammoType)
	{
				
		if(WorldGlobal.isReadyToFireGrowth|| (pickObj == null && currentActivePaint.paintType != typeof(GrowSplotch)))
		{
			if(currentActivePaint.paintType ==  typeof(GrowSplotch))
				WorldGlobal.hasFiredGrowth= true;
	//		Vector3 dir = cam.transform.forward;
	//		
	//		GameObject paint = Instantiate(blob, transform.position + transform.TransformDirection(Vector3.forward), Quaternion.identity) as GameObject;
	//		//paint.SendMessage("SetPaint", );
	//		Physics.IgnoreCollision(paint.collider, collider);
	//		paint.rigidbody.AddForce(cam.transform.TransformDirection(Vector3.forward)*700);
			
	//		Vector3 dir = paintSpawn.forward;
			GameObject paint = Instantiate(blob, paintSpawn.position, Quaternion.identity) as GameObject;
			paint.SendMessage("setMyPaint", ammoType);
			
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
}
