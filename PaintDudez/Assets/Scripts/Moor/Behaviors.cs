using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/*All the behaviors*/


namespace MainGameComponents
{
    
    public class Walk : CharacterBehavior
    {
        public Walk(myMoveVars mv) : base(mv)
        {
             //Debug.Log ("Walking!!!");
            //No need to change the myMoveVars
        }
		public Walk(myMoveVars mv, Vector3 norm) : base(mv, norm)
        {
             //Debug.Log ("Walking!!!");
            //No need to change the myMoveVars
        }
		public override void SetBehv()
		{
			CurBehv = "walk";
		}
        public override void HandleInput()
        {
			Vector3 myMove = Vector3.zero;
			Vector3 myRot = Vector3.zero;
            if(InputManager.GetKey("Up"))
				myMove.x += dataValues.hForce;
			if(InputManager.GetKey("Down"))
				myMove.x -= dataValues.hForce;
			if(InputManager.GetKey("Right"))
				if(!dataValues.FirstPlayer)
					myRot.y += 2.0f;
				else
					myMove.z += dataValues.hForce;
			if(InputManager.GetKey("Left"))
				if(!dataValues.FirstPlayer)
					myRot.y -= 2.0f;
				else
					myMove.z -= dataValues.hForce;
			if(InputManager.GetKey ("Behv1"))
			{
				if(!dataValues.inAir)
				{
					dataValues.Vel.y += dataValues.jForce;
					dataValues.inAir = true;
				}
			}
			
			if(myChar.getBehv()  == "walk")
			{
				if(InputManager.GetKey ("Mod3"))
					myChar.SetBehavior("run");
				if(InputManager.GetKey ("Mod2"))
					myChar.SetBehavior("hax");
			}
			myChar.transform.rotation = Quaternion.Euler(myChar.transform.rotation.eulerAngles + myRot);
			
			Vector3 myTransform = myChar.transform.TransformDirection(Vector3.forward);
			if(dataValues.FirstPlayer)
				myTransform = myChar.transform.TransformDirection(Vector3.back);
			
			myTransform.y = 0;
			
			myTransform.x *= -1;
			myTransform.z *= -1;
			dataValues.Vel += myTransform.normalized * myMove.x;
			if(dataValues.FirstPlayer)
			{
				myTransform = myChar.transform.TransformDirection(Vector3.right);
				dataValues.Vel += myTransform.normalized * myMove.z;
			}
			
			
			Vector3 myVel = dataValues.Vel;
			myVel.y = 0;
			
			/*if(myVel.magnitude > dataValues.maxHSpeed)
				myVel = Vector3.Scale(myVel.normalized, new Vector3(dataValues.maxHSpeed, 0, dataValues.maxHSpeed));*/
			myVel.Scale(new Vector3(0.9f,0.0f,0.9f));
			Debug.DrawLine(myChar.transform.position,myChar.transform.position + myChar.groundNormal,Color.blue, 10f);
			//Ground Compenation!
			//myVel = Vector3.Cross (Vector3.Cross (Vector3.up,myVel), myChar.groundNormal ).normalized * myVel.magnitude;
			
			Debug.DrawLine(myChar.transform.position,myChar.transform.position + myVel,Color.red, 10f);
			
			myVel.y += dataValues.Vel.y;
			
			if(dataValues.Vel.y < dataValues.maxVDownSpeed)
			{
				myVel.y = dataValues.maxVDownSpeed;
			}
			
			myVel -= dataValues.gForce;
			
			dataValues.Vel = myVel;
        }
        public override void HandleCamera()
        {
			GameObject myPlayer = GameObject.Find("CubePlayer");
			
			Transform myCamera = Camera.main.transform;
			if(!dataValues.FirstPlayer)
			{
				Vector3 TargetPosition = myPlayer.transform.TransformDirection(Vector3.forward);
				TargetPosition.y = 0;
				TargetPosition.Normalize();
				TargetPosition.y = 0.2f;
				TargetPosition.Scale(new Vector3(5.0f,5.0f,5.0f));
				
				TargetPosition += myPlayer.transform.position;
				Vector3 temp = (TargetPosition - myCamera.position).normalized;
				
				float SCALE = Vector3.Distance(myCamera.position, TargetPosition) / 10;
				temp.Scale (new Vector3(SCALE,SCALE,SCALE));
				myCamera.position = myCamera.position  + temp;
				if(Vector3.Distance(myCamera.position, TargetPosition) < SCALE)
					myCamera.position = TargetPosition;
				
				Quaternion TargetRotation = Quaternion.LookRotation(myPlayer.transform.position - myCamera.position);
				
				
				myCamera.rotation = Quaternion.RotateTowards(myCamera.rotation, TargetRotation, 5.0f);
			}
			else
			{
				//Screen.lockCursor = true;
				
				Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
				
				mouseInput.x *= dataValues.sensitivity.x / dataValues.smoothing.x;
				mouseInput.y *= dataValues.sensitivity.y / dataValues.smoothing.y;
				
				Vector3 newOrientation = myCamera.transform.eulerAngles + new Vector3(-mouseInput.y, mouseInput.x, 0);
				myCamera.position = myPlayer.transform.position;
				myPlayer.transform.eulerAngles = newOrientation;
        		myCamera.transform.eulerAngles = newOrientation;
				
			}
        }
    }
	public class Run : Walk 
	{
		public Run(myMoveVars mv) : base(mv)
		{
			dataValues.hForce = 3.0f ;
		}
		public override void HandleInput ()
		{
			if(myChar.getBehv() == "run")
			{
				if(!InputManager.GetKey ("Mod3"))
					myChar.SetBehavior("walk");
			}
			base.HandleInput();
			
		}
	}
	public class Hax : Walk
	{
		public Hax(myMoveVars mv) : base(mv)
		{
			dataValues.hForce = 15.0f;
		}
		public override void HandleInput ()
		{
			if(myChar.getBehv() == "hax")
			{
				if(!InputManager.GetKey ("Mod2"))
					myChar.SetBehavior("walk");
			}
			base.HandleInput();
			
		}
	}
	
	public class RedPaint : Walk
	{
		public RedPaint(myMoveVars mv) : base(mv)
		{
			dataValues.hForce = 7.0f;
			//dataValues.maxHSpeed = 1000.0f;
		}
	}
	public class BluePaint : Walk
	{
		public BluePaint(myMoveVars mv, Vector3 norm) : base(mv, norm)
		{
			CurBehv = "BluePaint";
			if(!dataValues.inAir)
			{
				Debug.Log("1");
				dataValues.Vel += myNormal * 30;
			}
			else
			{
				Debug.Log("2");
				Vector3 tempVec = myNormal;
				tempVec.Scale(-dataValues.Vel);
				dataValues.Vel += tempVec;
				
			}
			dataValues.inAir = true;
			//Debug.Log("Bounced: " + dataValues.Vel);

			
		}
	}
}
