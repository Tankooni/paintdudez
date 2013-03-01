using UnityEngine;
using System.Collections.Generic;

public class PaintShooter : MonoBehaviour
{
	//List<PhysicMaterial> physicsMats = new List<PhysicMaterial>();
	public PhysicMaterial [] physicsMats;
	public int currentMat = 0;
	
	// Use this for initialization
	void Start()
	{
		//physicsMats = Resources.LoadAll("PhysMats") as PhysicMaterial;
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
		Vector3 dir = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		
		
		if(Physics.Raycast(transform.position, dir, out hit, 10))
		{
			Debug.Log("Hit: " + hit.rigidbody.name);
			hit.rigidbody.renderer.material.color = new Color(Random.value, Random.value, Random.value);
			Debug.DrawLine(transform.position, hit.transform.position, Color.red, 10);
		}
	}
}
