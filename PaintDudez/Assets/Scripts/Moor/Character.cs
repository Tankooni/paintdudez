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
    public void Awake()
    {
		groundNormal = Vector3.zero;
        myStates.Add("walk", typeof(Walk));
		myStates.Add("run", typeof(Run));
		myStates.Add("hax", typeof(Hax));
		myStates.Add("RedPaint", typeof(RedPaint));
		myStates.Add("BluePaint", typeof(BluePaint));
		
		SetBehavior("");
        //myStates.Add("run", typeof(Run));
        InputManager.Init();
		
		myController = GetComponent<CharacterController>();
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
    public void SetBehavior(string myBehv)
    {
        if (!myStates.Keys.Contains(myBehv))
        {
            myBehv = DefaultBehavior;
			Debug.Log(myBehv);
        }
		myBehvariorS = myBehv;
        myBehavior = (CharacterBehavior)Activator.CreateInstance(myStates[myBehv]);
    }
    public void Update()
    {
		myBehavior.HandleUpdate();
		myFlags = myController.Move(myBehavior.GetVel() * Time.smoothDeltaTime);
		myBehavior.dataValues.inAir = (myFlags & CollisionFlags.CollidedBelow) == 0;
    }
}
