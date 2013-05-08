using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using MainGameComponents;

public class WorldGlobal : MonoBehaviour {

	//Character myChar;
	public static Dictionary<string, AudioClip> audioClips;
	public static Dictionary<string, GameObject> Prefabs;
	public static Dictionary<string, Material> Materials;
	public static NarrationManager Narrator;
	
	void Awake () {
		Init ();
		DontDestroyOnLoad(this);
	}
	
	void Update () {
		//myChar.Update();
	}
	void OnLevelWasLoaded() {
		Init ();
	}
	void Init() {
		//myChar = new Character();
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
		
		Narrator = new NarrationManager();
	}
}
