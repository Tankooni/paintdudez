using UnityEngine;
using System.Collections;

public class SwingScript : MonoBehaviour {
	
	private bool canMove;
	public float rotationAmount;
	
	// Use this for initialization
	void Start ()
	{
		canMove = true;
		rotationAmount = 1.5f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (canMove)
		{
			var nextRotation = gameObject.transform.rotation.z + Mathf.Sin(Time.timeSinceLevelLoad * 3.0f) * rotationAmount;

			gameObject.transform.Rotate(0, 0, nextRotation);
		}
	}
}
