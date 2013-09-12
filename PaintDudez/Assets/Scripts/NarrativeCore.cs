using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NarrativeCore : MonoBehaviour
{
	public bool IsPlaying = false;
	MeshRenderer NCMr;
	public Queue<AudioClip> toPlay = new Queue<AudioClip>();
	
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
		{
			Debug.Log("queueing");
			WorldGlobal.Narrator.QueueSound(sound);
			return false;
		}
			Debug.Log("playing");
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
			WorldGlobal.Narrator.SendMessage("AudioDone");
			audio.Stop();
			
		}
	}
	
	IEnumerator SoundBool(float time)
	{
		yield return new WaitForSeconds(time);
		WorldGlobal.Narrator.SendMessage("AudioDone");
		IsPlaying = false;
		StopCoroutine("SoundBool");
	}
}
