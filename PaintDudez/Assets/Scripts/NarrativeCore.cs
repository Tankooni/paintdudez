using UnityEngine;
using System.Collections;

public class NarrativeCore : MonoBehaviour
{
	public bool IsPlaying = false;
	MeshRenderer NCMr;
	void Start()
	{
		NCMr = GetComponent<MeshRenderer>();
		NCMr.enabled = false;
	}
	// Use this for initialization
	void Awake()
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
	
	public void StopSound()
	{
		if(IsPlaying == true)
		{
			IsPlaying = false;
			audio.Stop();
			
		}
	}
	
	IEnumerator SoundBool(float time)
	{
		yield return new WaitForSeconds(time);
		WorldGlobal.Narrator.SendMessage("AudioDone");
		IsPlaying = false;
	}
}
