using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using MainGameComponents;

public class WorldGlobal : MonoBehaviour {

	//Character myChar;
	public static Dictionary<string, AudioClip> audioClips;
	public static Dictionary<string, GameObject> Prefabs;
	
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
		
		Prefabs = new Dictionary<string, GameObject>();
		Prefabs.Add("blob", Resources.Load("Prefabs/paintBlob") as GameObject);
		Prefabs.Add("ball", Resources.Load("Prefabs/SphereZ") as GameObject);
		Prefabs.Add("gunCore", Resources.Load("Prefabs/PaintGunCore") as GameObject);
		Prefabs.Add("splatter", Resources.Load("Prefabs/splatterDecal") as GameObject);
	}
}
