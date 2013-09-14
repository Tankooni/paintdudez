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
		spawns.Add("TestSpawn", GameObject.Find("TestSpawn"));
		
		
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
		/*********
		 * Setup *
		 *********/
		Screen.lockCursor = true;
		
		while(narrativeCore == null)
		{
			yield return new WaitForSeconds(0);
		}
		
		NCMr = narrativeCore.GetComponent<MeshRenderer>();
		narrativeCore.renderer.material.color = Color.grey;
		
		/**************
		 * Black Room *
		 **************/
		while(!doneAudio)
		{
			yield return new WaitForSeconds(0);
		}
		
		Debug.Log("Finished Black");
		doneAudio = false;
		
		/**************
		 * White Room *
		 **************/
		setPlayerPosition("WhiteRoomSpawn"); 

		while(!doneAudio)
		{
			yield return new WaitForSeconds(0);
		}
		
		doneAudio = false;
		
		/****************
		 * Imagine Ball *
		 ****************/
		shootsNLadders.painGun.transform.FindChild("b_gun_root/Sphere001").gameObject.SetActive(true);
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_GunBall"]);
		
		while(!doneAudio)
		{
			yield return new WaitForSeconds(0);
		}
		
		doneAudio = false;
		
		/************************
		 * Imagine Ball is Blue *
		 ************************/
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_GunBallBlue"]);
		NCMr.enabled = true;
		
		while(!doneAudio)
		{
			yield return new WaitForSeconds(0);
		}
		
		narrativeCore.renderer.material.color = Color.blue;
		doneAudio = false;
		
		/******************
		 * Imagine Firing *
		 ******************/
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_GunFire"]);
		
		while(!doneAudio)
		{
			yield return new WaitForSeconds(0);
		}
		
		doneAudio = false;
		
		WorldGlobal.isReadyToFire = true;
		
		while(!Input.GetMouseButtonDown(0)) // Wait for fire
        {
			yield return new WaitForSeconds(0);
		}
		
		/****************
		 * Step Onto It *
		 ****************/
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_StepOntoIt"]);
		
		while(!doneAudio)
		{
			yield return new WaitForSeconds(0);
		}
		
		doneAudio = false;
		int count = 0;
		
		while(player.myBehvariorS != "BluePaint") // Wait for player to step on blue paint
		{	
			if(++count >= 200) // If player takes too long, tell them it's safe
			{
				break;
			}
			
			yield return new WaitForSeconds(0);
		}
		
		
		/*************
		 * It's Safe *
		 *************/
		if(count >= 200)
		{
			narrativeCore.PlaySound(WorldGlobal.audioClips["n_SafeBouncy"]);
			
			while(!doneAudio)
			{
				yield return new WaitForSeconds(0);
			}
			
			doneAudio = false;
		}
		
		while(player.myBehvariorS != "BluePaint") // Wait for player to step on blue paint
		{
			yield return new WaitForSeconds(0);
		}
		
		
		/***************
		 * It's Bouncy *
		 ***************/
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_ItsBouncy"]);
		
		while(!doneAudio)
		{
			yield return new WaitForSeconds(0);
		}
		
		doneAudio = false;
		
		
		/************************
		 * Laid Out a Small Map *
		 ************************/
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_SmallMap"]);
		
		while(!doneAudio)
		{
			yield return new WaitForSeconds(0);
		}
		
		doneAudio = false;
			
			// Spawn player
		setPlayerPosition("LevelSpawn");
		//setPlayerPosition("TestSpawn");
		
			// Start music cubes in sync
		double monkeyTime = AudioSettings.dspTime;
		GameObject.Find("Camera+Gun").audio.PlayScheduled(monkeyTime + 3);
		GameObject.Find("MusicCubeDrums").audio.PlayScheduled(monkeyTime + 3);
		GameObject.Find("MusicCubeDrums2").audio.PlayScheduled(monkeyTime + 3);
		GameObject.Find("MusicCubeChimes").audio.PlayScheduled(monkeyTime + 3);
		GameObject.Find("MusicCubeChimes2").audio.PlayScheduled(monkeyTime + 3);
		GameObject.Find("MusicCubeBoops").audio.PlayScheduled(monkeyTime + 3);
		GameObject.Find("MusicCubeBoops2").audio.PlayScheduled(monkeyTime + 3);
		GameObject.Find("MusicCubeBubbles").audio.PlayScheduled(monkeyTime + 3);
		
		
		/**************
		 * Your Place *
		 **************/
		narrativeCore.PlaySound(WorldGlobal.audioClips["n_YourPlace"]);
		
		while(!doneAudio)
		{
			yield return new WaitForSeconds(0);
		}
		
		doneAudio = false;
		
		/**************************
		 * Wait for Red Narration *
		 **************************/
		while(!doneAudio)
		{
			yield return new WaitForSeconds(0);
		}
		
		doneAudio = false;
		Debug.Log("Red Done");
		
		/****************************
		 * Wait for Green Narration *
		 ****************************/
		while(!doneAudio)
		{
			yield return new WaitForSeconds(0);
		}
		
		doneAudio = false;
		Debug.Log("Green Done");
		
		WorldGlobal.isReadyToFireGrowth = true; // Allow player to fire growth paint
		
//		/*****************************
//		 * Wait for Growth Narration *
//		 *****************************/
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
//		
//		doneAudio = false;
//		Debug.Log("Growth Done");
//		
//		WorldGlobal.isReadyToFireGrowth = true; // Allow player to fire growth paint
//		
//		while(!WorldGlobal.hasFiredGrowth) // Wait for player to fire growth paint
//		{
//			yield return new WaitForSeconds(0);
//		}
//		
//		
//		/*************************
//		 * Wow It's so Colorful! *
//		 *************************/
//		narrativeCore.PlaySound(WorldGlobal.audioClips["n_GrowColor"]);
//		
//		while(!doneAudio)
//		{
//			yield return new WaitForSeconds(0);
//		}
//		
//		doneAudio = false;
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
