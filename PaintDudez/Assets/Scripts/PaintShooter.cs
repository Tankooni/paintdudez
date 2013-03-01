using UnityEngine;
using System.Collections.Generic;

public class PaintShooter : MonoBehaviour
{
	//List<PhysicMaterial> physicsMats = new List<PhysicMaterial>();
	public PhysicMaterial [] physicsMats;
	public int currentMat = 0;
	Camera cam;
	GameObject splatter;
	
	// Use this for initialization
	void Start()
	{
		//physicsMats = Resources.LoadAll("PhysMats") as PhysicMaterial;
		cam = GetComponentInChildren<Camera>() as Camera;
		splatter = Resources.Load("Prefabs/splatterDecal") as GameObject;
		Debug.Log(splatter);
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
			Debug.Log("Fire");
			ShootPaint();
		}
		
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			Debug.Log("1");
		}
		
	}
	
	void ShootPaint()
	{
		Vector3 dir = cam.transform.forward;
		
		RaycastHit hit;
		
		
		if(Physics.Raycast(transform.position, dir, out hit, 10))
		{
			GameObject decal;
//			Debug.Log("Hit: " + hit.rigidbody.name);
//			Debug.Log(cam.transform.position);
			Debug.DrawLine(transform.position, hit.point, Color.red, 10);
			
			decal = Instantiate(splatter, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
			
			hit.rigidbody.renderer.material.color = new Color(Random.value, Random.value, Random.value);
			
			hit.rigidbody.collider.material = physicsMats[1];
			hit.rigidbody.AddForce(new Vector3(0,500,0));
		}
	}
}
