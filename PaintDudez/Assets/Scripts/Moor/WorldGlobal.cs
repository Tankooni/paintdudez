using UnityEngine;
using System.Collections;
using System;
using MainGameComponents;

public class WorldGlobal : MonoBehaviour {

	Character myChar;
	
	void Awake () {
		Init ();
		Debug.Log("HAHAHAHAHAHA!!");
		DontDestroyOnLoad(this);
	}
	
	void Update () {
		myChar.Update ();
	}
	void OnLevelWasLoaded() {
		Init ();
	}
	void Init() {
		myChar = new Character();
	}
}
