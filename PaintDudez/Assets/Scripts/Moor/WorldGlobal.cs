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
	Color newColor;
	Color oldColor;
	float percentage = 0;
	
	void Awake()
	{
		Init ();
		DontDestroyOnLoad(this);
		newColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
		oldColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
	}
	
	void Update()
	{
		if(!(percentage >= 1))
		{
			percentage += .03f;
			ChangeColor(ref WorldGlobal.currentColor, oldColor, newColor, percentage);
		}
		else
		{
			Debug.Log("NewColor");
			oldColor = newColor;
			newColor.r = UnityEngine.Random.value;
			newColor.g = UnityEngine.Random.value;
			newColor.b = UnityEngine.Random.value;
			percentage = 0;
		}
	}
	void OnLevelWasLoaded()
	{
		Init ();
	}
	void Init()
	{
		audioClips = new Dictionary<string, AudioClip>();
		audioClips.Add("splat", Resources.Load("Sounds/splat") as AudioClip);
		audioClips.Add("shoot", Resources.Load("Sounds/shoot") as AudioClip);
		audioClips.Add("bounce", Resources.Load("Sounds/bounce") as AudioClip);
		audioClips.Add("ballBounce", Resources.Load("Sounds/ballBounce") as AudioClip);
		audioClips.Add("speedPaint", Resources.Load("Sounds/speedPaint") as AudioClip);
		audioClips.Add("clean", Resources.Load("Sounds/erasePaint") as AudioClip);
		audioClips.Add("grow", Resources.Load("Sounds/growPaint") as AudioClip);
		
		Prefabs = new Dictionary<string, GameObject>();
		Prefabs.Add("blob", Resources.Load("Prefabs/paintBlob") as GameObject);
		Prefabs.Add("ball", Resources.Load("Prefabs/SphereZ") as GameObject);
		Prefabs.Add("gunCore", Resources.Load("Prefabs/PaintGunCore") as GameObject);
		Prefabs.Add("splatter", Resources.Load("Prefabs/splatterDecal") as GameObject);
		Prefabs.Add("growthBlock", Resources.Load("Prefabs/GrowthBlock") as GameObject);
		
		Materials = new Dictionary<string, Material>();
		Materials.Add("bubble", Resources.Load("Materials/SoapBubble") as Material);
		Materials.Add("default", Resources.Load("Materials/lambert1") as Material);
	}
	
	void ChangeColor(ref Color n, Color a, Color b, float perc)
	{
		n.r = Mathf.Lerp(a.r, b.r, perc);
		n.b = Mathf.Lerp(a.b, b.b, perc);
		n.g = Mathf.Lerp(a.g, b.g, perc);
	}
}
