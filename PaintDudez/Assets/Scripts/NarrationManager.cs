using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NarrationManager
{
	public Dictionary<string, AudioClip> narrationClips;
	public Character player;
	public NarrativeCore narrativeCore;
	
	// TODO: Implement this if necessary
	Queue<AudioClip> audioQueue;
	
	public NarrationManager()
	{
		narrationClips = new Dictionary<string, AudioClip>();
		audioQueue = new Queue<AudioClip>();
	}
	
	public void QueueSound(AudioClip audio)
	{
		audioQueue.Enqueue(audio);
	}
	
	public void Update()
	{
		Debug.Log(narrativeCore + " " + player);
	}
}
