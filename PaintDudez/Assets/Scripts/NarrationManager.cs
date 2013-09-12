using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NarrationManager : MonoBehaviour
{
	public Dictionary<string, AudioClip> narrationClips;
	public Dictionary<string, GameObject> spawns;
	public Character player;
	public NarrativeCore narrativeCore;
	public PaintShooter shootsNLadders;
	public bool doneAudio = false;
	MeshRenderer NCMr;
	
	// TODO: Implement this if necessary
	Queue<AudioClip> audioQueue;
	
	void Awake()
	{
		WorldGlobal.Narrator = this;
		narrationClips = new Dictionary<string, AudioClip>();
		spawns = new Dictionary<string, GameObject>();
		audioQueue = new Queue<AudioClip>();
		
		spawns.Add("BlackRoomSpawn", GameObject.Find("BlackRoomSpawn"));
		spawns.Add("WhiteRoomSpawn", GameObject.Find("WhiteRoomSpawn"));
		spawns.Add("LevelSpawn", GameObject.Find("LevelSpawn"));
		
		StartCoroutine(AUDIOMONKEY());
		//player.transform.position = spawns["BlackRoomSpawn"].transform.position;
	}
	
//	public NarrationManager()
//	{
//		
//	}
	
	public void setPlayerPosition(string spawn)
	{
		if (player != null && player.transform != null)
		{
			player.transform.position = spawns[spawn].transform.position;
		}
	}
	
	public void AudioDone()
	{
		doneAudio = true;
	}
	
	IEnumerator AUDIOMONKEY()
	{
		Screen.lockCursor = true;
		while(narrativeCore == null)
		{
			yield return new WaitForSeconds(0);
		}
		NCMr = narrativeCore.GetComponent<MeshRenderer>();
		narrativeCore.renderer.material.color = Color.grey;
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		Debug.Log("Finished Black");
		doneAudio = false;
		setPlayerPosition("WhiteRoomSpawn"); 

//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		doneAudio = false;
		Debug.Log(shootsNLadders.painGun.transform.FindChild("b_gun_root/Sphere001"));
		shootsNLadders.painGun.transform.FindChild("b_gun_root/Sphere001").gameObject.SetActive(true);
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_GunBall"]);
		
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		doneAudio = false;
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_GunBallBlue"]);
		NCMr.enabled = true;
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		narrativeCore.renderer.material.color = Color.blue;
		doneAudio = false;
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_GunFire"]);
		
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		doneAudio = false;
		
		WorldGlobal.isReadyToFire = true;
		
//		while(!Input.GetMouseButtonDown(0))
//        {
//			yield return new WaitForSeconds(0);
//		}
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_StepOntoIt"]);
		
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		doneAudio = false;
		
		int count = 0;
//		while(player.myBehvariorS != "BluePaint")
//		{	
//			count++;
//			if(count >= 200)
//			{
//				Debug.Log("Bananas");
//				break;
//			}
//			yield return new WaitForSeconds(0);
//		}
//		if(count >= 200)
//		{
//			narrativeCore.PlaySound(WorldGlobal.audioClips["n_SafeBouncy"]);
//			while(!doneAudio)
//			{
//				yield return new WaitForSeconds(0);
//			}
//			doneAudio = false;
//		}
		
//		while(player.myBehvariorS != "BluePaint")
//		{
//			yield return new WaitForSeconds(0);
//		}
		
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_ItsBouncy"]);
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		doneAudio = false;
		
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_SmallMap"]);
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		doneAudio = false;
		
		setPlayerPosition("LevelSpawn");
		
		double monkeyTime = AudioSettings.dspTime;
		GameObject.Find("Camera+Gun").audio.PlayScheduled(monkeyTime + 3);
		GameObject.Find("Cube 1").audio.PlayScheduled(monkeyTime + 3);
		GameObject.Find("Cube 2").audio.PlayScheduled(monkeyTime + 3);
		GameObject.Find("Cube 3").audio.PlayScheduled(monkeyTime + 3);
		GameObject.Find("Cube 4").audio.PlayScheduled(monkeyTime + 3);
		
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_YourPlace"]);
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		doneAudio = false;
		
		//waiting for Red
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		doneAudio = false;
		Debug.Log("Red Done");
		
		//waiting for Green
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		doneAudio = false;
		Debug.Log("Green Done");
		
		//waiting for Growth
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		doneAudio = false;
		Debug.Log("Growth Done");
		
		WorldGlobal.isReadyToFireGrowth = true;
//		while(!WorldGlobal.hasFiredGrowth)
//		{
//			yield return new WaitForSeconds(0);
//		}
		
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_GrowColor"]);
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
		doneAudio = false;
	}
	
	public void QueueSound(AudioClip audio)
	{
		audioQueue.Enqueue(audio);
	}
	
	public void Update()
	{
		//Debug.Log(narrativeCore + " " + player);
	}
}
