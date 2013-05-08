using UnityEngine;
using System.Collections;

public class KillZ : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name == "CubePlayer") // Only trigger for the player
		{
			Debug.Log("KILL Z " + name);
			col.gameObject.SendMessage("killedZ", SendMessageOptions.DontRequireReceiver);
		}
		else
			Destroy(col.gameObject);
	}
	
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, transform.localScale);
	}
}
