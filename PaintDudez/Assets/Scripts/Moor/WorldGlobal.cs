using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using MainGameComponents;

public class WorldGlobal : MonoBehaviour {

	//Character myChar;
	public static Dictionary<string, AudioClip> audioClips;
	
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
	}
}
