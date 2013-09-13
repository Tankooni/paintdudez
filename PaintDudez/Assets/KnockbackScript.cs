using UnityEngine;
using System.Collections;
using MainGameComponents;

public class KnockbackScript : MonoBehaviour
{
	public float KnockbackForce = 100.0f;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	void OnTriggerEnter(Collider col)
	{
		Debug.Log("HERE!!!!! " + col.gameObject.name);
		if(col.gameObject.name == "CubePlayer") // Only trigger for the player
		{
			Debug.Log("HIT PLAYER!");
			
			
//			CharacterBehavior playerBehavior = (col as ) as CharacterBehavior);
//			playerBehavior.dataValues.Vel = playerBehavior.dataValues.Vel.normalized * KnockbackForce;
		}
	}
}
