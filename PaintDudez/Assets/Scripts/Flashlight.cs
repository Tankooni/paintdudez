using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
	GameObject lightObject;
	Ray screenRay;
	// Use this for initialization
	void Start ()
	{
		lightObject = new GameObject("Light");;
		lightObject.AddComponent<Light>();
		lightObject.light.type = LightType.Spot;
		lightObject.light.color = Color.white;
		lightObject.light.spotAngle = 60;
		
		lightObject.transform.parent = transform.Find("Root/LightBone").transform;
		lightObject.transform.position = transform.Find("Root/LightBone").transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 50);
		screenRay =  Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit = new RaycastHit();
		if(Physics.Raycast(screenRay, out hit, 100.0f))
		{
			Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
			transform.LookAt(hitPoint);
			
		}
		
		if(Input.GetKeyDown(KeyCode.F))
			lightObject.light.enabled = !lightObject.light.enabled;
	}
}
