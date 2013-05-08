using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NarrationManager
{
	public Dictionary<string, AudioClip> narrationClips;
	Character player;
	
	// TODO: Implement this if necessary
	Queue<AudioClip> audioQueue;
	
	public NarrationManager()
	{
		narrationClips = new Dictionary<string, AudioClip>();
		audioQueue = new Queue<AudioClip>();
	}
	
	void Update ()
	{
	
	}
}
