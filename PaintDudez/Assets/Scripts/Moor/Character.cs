using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MainGameComponents;

[RequireComponent(typeof(CharacterController))]

public class Character : MonoBehaviour
{
    [NonSerialized]
	public Dictionary<string, Type> myStates = new Dictionary<string, Type>();
	[NonSerialized]
    private string DefaultBehavior = "walk";
	[NonSerialized]
    public CharacterBehavior myBehavior;
	[NonSerialized]
	public string myBehvariorS;
	[NonSerialized]
	public CharacterController myController;
	[NonSerialized]
	public CollisionFlags myFlags;
	[NonSerialized]
	public Vector3 groundNormal;
	[NonSerialized]
	public myMoveVars characterValues;
	
	public Vector3 lastGoodPos = Vector3.zero;
	
    public void Start()
    {
		WorldGlobal.Narrator.player = this;
		
		characterValues = new myMoveVars();
		groundNormal = Vector3.zero;
        myStates.Add("walk", typeof(Walk));
		myStates.Add("run", typeof(Run));
		myStates.Add("hax", typeof(Hax));
		myStates.Add("RedPaint", typeof(RedPaint));
		myStates.Add("BluePaint", typeof(BluePaint));
		myStates.Add("GravPaint", typeof(GravPaint));
		
		SetBehavior("");
        //myStates.Add("run", typeof(Run));
        InputManager.Init();
		
		myController = GetComponent<CharacterController>();
		
		gameObject.SendMessage("SetCharValues", characterValues);
		WorldGlobal.Narrator.setPlayerPosition("BlackRoomSpawn");
    }
	public void OnControllerColliderHit (ControllerColliderHit hit)
	{
		//Debug.Log(hit.normal.ToString());
		groundNormal = hit.normal;
	}
	public String getBehv()
	{
		return myBehvariorS;
	}
	
	public void SetBehavior(object[] o)
    {
		string myBehv = (string)o[0];
        if (!myStates.Keys.Contains(myBehv))
        {
            myBehv = DefaultBehavior;
			//Debug.Log(myBehv);
        }
		myBehvariorS = myBehv;
        myBehavior = (CharacterBehavior)Activator.CreateInstance(myStates[myBehv], new object[]{characterValues, (Vector3)o[1]});
    }
	public void SetBehavior(string myBehv)
    {
        if (!myStates.Keys.Contains(myBehv))
        {
            myBehv = DefaultBehavior;
			//Debug.Log(myBehv);
        }
		myBehvariorS = myBehv;
        myBehavior = (CharacterBehavior)Activator.CreateInstance(myStates[myBehv], new object[]{characterValues});
    }
	
	public void MoveUpdate()
	{
		myBehavior.HandleUpdate();
		myFlags = myController.Move(myBehavior.GetVel() * Time.smoothDeltaTime);
	}
	
    public void Update()
    {	
		characterValues.hAccel -= Time.smoothDeltaTime * 3;
		characterValues.hAccel = Mathf.Clamp(characterValues.hAccel, 0.0f, characterValues.maxHAccel);
		//Debug.Log(characterValues.hAccel + "    " + characterValues.maxHAccel);
		
		myBehavior.HandleUpdate();
		myFlags = myController.Move(myBehavior.GetVel() * Time.smoothDeltaTime);
		//myBehavior.dataValues.inAir = (myFlags & CollisionFlags.CollidedBelow) == 0;
		
		if((myFlags & CollisionFlags.CollidedBelow) == 0)
		{
			characterValues.inAir = true;
		}
		else if((myFlags & CollisionFlags.CollidedBelow) == CollisionFlags.Below)
		{
			characterValues.inAir = false;
			characterValues.Vel.y = 0;
			if(transform.tag != "DoNot")
				lastGoodPos = transform.position;
		}
		
		if((myFlags & CollisionFlags.CollidedAbove) == (CollisionFlags.Above))
		{
			characterValues.Vel.y = 0;
		}
		//Debug.Log("In Air: " + characterValues.inAir);
    }
	
	public void GetVel(RedSplotch rs)
	{
		rs.charVals = characterValues;
	}
	
	public void killedZ()
	{
		characterValues.PrevVel = Vector3.zero;
		characterValues.Vel = Vector3.zero;
		transform.position = lastGoodPos;
	}
}
