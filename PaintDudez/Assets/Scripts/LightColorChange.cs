using UnityEngine;
using System.Collections;

public class LightColorChange : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		this.light.color = WorldGlobal.currentColor;
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.light.color = WorldGlobal.currentColor;
	}
}
