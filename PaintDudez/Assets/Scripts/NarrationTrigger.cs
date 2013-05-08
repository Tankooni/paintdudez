using UnityEngine;
using System.Collections;

public class NarrationTrigger : MonoBehaviour
{
	public string NarrationAudioClip = "";
	bool hasBeenTriggered = false;
	
	void Start ()
	{
		// If all is good, give the narration manager the appropriate audio clip, with this collider as a key
		if (NarrationAudioClip != "" && WorldGlobal.audioClips.ContainsKey(NarrationAudioClip))
		{
			Debug.Log(WorldGlobal.audioClips[NarrationAudioClip]);
			WorldGlobal.Narrator.narrationClips.Add(name, WorldGlobal.audioClips[NarrationAudioClip]);
		}
	}
	
	void Update ()
	{
	
	}
	
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, transform.localScale);
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (!hasBeenTriggered) // Only trigger once
		{
			if(col.gameObject.name == "CubePlayer") // Only trigger for the player
			{
				Debug.Log("NARRATION TREEGGUR " + name);
				
				// If all is good, play the clip at the player's position
				if (WorldGlobal.Narrator.narrationClips.ContainsKey(name) && WorldGlobal.Narrator.narrationClips[name] != null)
				{
					AudioSource.PlayClipAtPoint(WorldGlobal.Narrator.narrationClips[name], col.gameObject.transform.position);
					hasBeenTriggered = true;
				}
			}
		}
	}
}
