using UnityEngine;
using System.Collections;

public class NarrativeCore : MonoBehaviour
{
	public bool IsPlaying = false;
	// Use this for initialization
	void Start()
	{
		WorldGlobal.Narrator.narrativeCore = this;
	}
	
	// Update is called once per frame
	void Update()
	{
		//renderer.material.color = WorldGlobal.currentColor;
	}
	
	public bool PlaySound(AudioClip sound)
	{
		if(IsPlaying == true)
			return false;
		IsPlaying = true;
		audio.PlayOneShot(sound);
		StartCoroutine(SoundBool(sound.length));
		return true;
	}
	
	IEnumerator SoundBool(float time)
	{
		yield return new WaitForSeconds(time);
		IsPlaying = false;
	}
}
