using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using MainGameComponents;

public class WorldGlobal : MonoBehaviour
{
	public static Dictionary<string, AudioClip> audioClips;
	public static Dictionary<string, GameObject> Prefabs;
	public static Dictionary<string, Material> Materials;
	public static Color currentColor;
	public static NarrationManager Narrator;
	Color newColor;
	Color oldColor;
	float percentage = 0;
	
	void Awake()
	{
		Init ();
		DontDestroyOnLoad(this);
	}
	
	void Update()
	{
		ChangeColor();
	}
	void OnLevelWasLoaded()
	{
		Init ();
	}
	void Init()
	{
		newColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 0.9f);
		oldColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value, 0.9f);
		
		audioClips = new Dictionary<string, AudioClip>();
		UnityEngine.Object[] os = Resources.LoadAll("Sounds");
		foreach(UnityEngine.Object o in  os)
		{
			audioClips.Add((o as AudioClip).name, o as AudioClip);
			//Debug.Log(o);
		}
		
		
//		audioClips.Add("splat", Resources.Load("Sounds/splat") as AudioClip);
//		audioClips.Add("shoot", Resources.Load("Sounds/shoot") as AudioClip);
//		audioClips.Add("bounce", Resources.Load("Sounds/bounce") as AudioClip);
//		audioClips.Add("ballBounce", Resources.Load("Sounds/ballBounce") as AudioClip);
//		audioClips.Add("speedPaint", Resources.Load("Sounds/speedPaint") as AudioClip);
//		audioClips.Add("clean", Resources.Load("Sounds/erasePaint") as AudioClip);
//		audioClips.Add("grow", Resources.Load("Sounds/growPaint") as AudioClip);
//		
		Prefabs = new Dictionary<string, GameObject>();
		os = Resources.LoadAll("Prefabs");
		foreach(UnityEngine.Object o in  os)
		{
			Prefabs.Add((o as GameObject).name, o as GameObject);
		}
		
		Materials = new Dictionary<string, Material>();
		Materials.Add("bubble", Resources.Load("Materials/SoapBubble") as Material);
		Materials.Add("default", Resources.Load("Materials/lambert1") as Material);
		WorldGlobal.Narrator = GetComponent<NarrationManager>();
//		Narrator = new NarrationManager();
	}

	void ChangeColor()
	{
		if(!(percentage >= 1))
		{
			percentage += .03f;
		}
		else
		{
			oldColor = newColor;
			newColor.r = UnityEngine.Random.value;
			newColor.g = UnityEngine.Random.value;
			newColor.b = UnityEngine.Random.value;
			percentage = 0;
		}
		
		currentColor.r = Mathf.Lerp(oldColor.r, newColor.r, percentage);
		currentColor.b = Mathf.Lerp(oldColor.b, newColor.b, percentage);
		currentColor.g = Mathf.Lerp(oldColor.g, newColor.g, percentage);
	}
}
