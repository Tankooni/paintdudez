using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System.Management;
using System;
//using System.IO;


namespace MainGameComponents
{
	public class myMoveVars
	{
        //These have to be defined:
        public bool canMove;
        public float maxHSpeed;
        public float maxVUpSpeed;
		public float maxVDownSpeed;
        public Vector3 gForce;
		public float hForce;
		public float hAccel;
		public float hAccelDeg;
		public float jForce;
		public bool FirstPlayer = true;
		
		
        //Also Must Be Defined: (Maybe)
        public List<string> myAnimations;

		//Only if FirstPlayer = true, then define:
		public Vector2 sensitivity = new Vector2(10, 10);
		public Vector2 smoothing = new Vector2(3, 3);
		
        //Per loop basis modified
        public Vector3 Vel;
		public Vector3 PrevVel;
        public bool inAir;
        public Vector3 jumpVel;

        public myMoveVars()
        {
			FirstPlayer = true;
            canMove = true;
			hForce = 1.0f;
			hAccel = 0.0f;
			hAccelDeg = -0.1f;
			jForce = 16.666f;
            maxHSpeed = 100.0f ;
            maxVUpSpeed = 40 ;
			maxVDownSpeed = -100 ;
            gForce = new Vector3(0.0f, 0.6333f, 0.0f);

        }
    }

    /// <summary>
    /// The generic abstract class that all behaviors are built off of.
    /// </summary>
	public abstract class CharacterBehavior
	{
        /// <summary>
        /// Handles all the sotrage and management of the character
        /// movement variables.
        /// 
        /// The defaults fot the variables are set up to be Walk
        /// </summary>
        
        [AddComponentMenu("Moor/Behavior")]

        protected int myState;
        protected Character myChar;
		public myMoveVars dataValues;
		public string CurBehv;
		public Vector3 myNormal;
		
		public GameObject myPlayer = null;
		
		public CharacterBehavior(myMoveVars mv)
		{
			dataValues = mv;
			CurBehv = "none";
			myPlayer = GameObject.Find("CubePlayer");
            myChar = (Character)myPlayer.GetComponent(typeof(Character));
            
			SetBehv();
		}
		public CharacterBehavior(myMoveVars mv, Vector3 norm)
			:this(mv)
		{
			myNormal = norm;
		}

		
		public abstract void SetBehv();
        ///<summary>
		///Deals with the input
        /// - Keys add to velocity
        /// - Keys trigger different states
        /// - Keys change states
        /// </summary>
		public abstract void HandleInput();
		public abstract void HandleCamera();
        public Vector3 GetVel()
		{
			return dataValues.Vel;
		}
        public void HandleUpdate()
        {

            HandleInput();
			HandleCamera();
        }
	}
    /// <summary>
    /// Handles all the bindings of keys for our game
    /// </summary>
	static public class InputManager
	{
        static public void Init()
        {
            Console.WriteLine("Updated...");
			DefaultLayout();
            //Load();
        }
		static private Dictionary<string, int> InputLayout = new Dictionary<string, int>();
		static string ControlSettingsFile = "ccFile.txt";
		/// <summary>
		/// Binds _KeyName to key code _KeyValue
        /// [STATIC]
		/// </summary>
		/// <param name="_KeyName"></param>
		/// <param name="_KeyValue"></param>
		static public void ChangeKey(string _KeyName, int _KeyValue)
		{
			if(!InputLayout.ContainsKey(_KeyName))
				InputLayout.Add(_KeyName,0);
			InputLayout[_KeyName] = _KeyValue;
		}
		/// <summary>
		/// Returns the input that is bound to key named _KeyName
		/// </summary>
		/// <param name="_KeyName"></param>
		/// <returns></returns>
		static public bool GetKey(string _KeyName)
		{
			if(InputLayout.ContainsKey(_KeyName))
				return Input.GetKey((KeyCode)InputLayout[_KeyName]);
            //Unchangeables - Locked in
            if (_KeyName == "Up")
                return Input.GetKey(KeyCode.UpArrow);
            if (_KeyName == "Down")
                return Input.GetKey(KeyCode.DownArrow);
            if (_KeyName == "Left")
                return Input.GetKey(KeyCode.LeftArrow);
            if (_KeyName == "Right")
                return Input.GetKey(KeyCode.RightArrow);
            if(_KeyName == "Back")
                return Input.GetKey(KeyCode.Escape);
			return false;
		}
		/// <summary>
		/// Will erase the current key bindings and reset them to their defaults
		/// </summary>
		static public void DefaultLayout()
		{
            InputLayout.Clear();
            InputLayout.Add("Up" , KeyCode.W.GetHashCode());
            InputLayout.Add("Down" , KeyCode.S.GetHashCode());
            InputLayout.Add("Right" , KeyCode.D.GetHashCode());
            InputLayout.Add("Left" , KeyCode.A.GetHashCode());
            InputLayout.Add("Mod1" , KeyCode.LeftControl.GetHashCode());
            InputLayout.Add("Mod2" , KeyCode.LeftAlt.GetHashCode());
            InputLayout.Add("Mod3" , KeyCode.LeftShift.GetHashCode());
            InputLayout.Add("Back" , KeyCode.Escape.GetHashCode());
            InputLayout.Add("Behv0" , KeyCode.E.GetHashCode());
            InputLayout.Add("Behv1" , KeyCode.Space.GetHashCode());
            InputLayout.Add("Behv2" , KeyCode.Mouse0.GetHashCode());
            InputLayout.Add("Behv3" , KeyCode.Mouse1.GetHashCode());
            InputLayout.Add("Behv4" , KeyCode.Z.GetHashCode());
            InputLayout.Add("Behv5" , KeyCode.C.GetHashCode());
            InputLayout.Add("ChatGUI" , KeyCode.Mouse0.GetHashCode());
            InputLayout.Add("InvGUI" , KeyCode.Mouse0.GetHashCode());
            InputLayout.Add("Scream" , KeyCode.Mouse0.GetHashCode());
            InputLayout.Add("LocalChat" , KeyCode.Mouse0.GetHashCode());
		}
		/*
        /// <summary>
        /// This will load the current saved key setup
        /// </summary>
		static public void Load()
		{
            if (!File.Exists(GLOBALVARS.SettingsPath + ControlSettingsFile))
            {
                Save();
				Load ();
				return;
            }
            try
            {
                using (StreamReader _reader = new StreamReader(GLOBALVARS.SettingsPath + ControlSettingsFile))
                {
                    string line;
                    InputLayout.Clear();
                    while ((line = _reader.ReadLine()) != null)
                    {
                        int myInt;
                        int.TryParse(line.Substring(line.IndexOf(' ')), out myInt);
                        InputLayout.Add(
                            line.Substring(0,line.IndexOf(' ')),
                            myInt);
                        Console.WriteLine("Loaded "+line.Substring(0,line.IndexOf(' ')) + " as "+myInt.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)
                    Save();
                   
            }
		}
        
        /// <summary>
        /// Saves the current input schema.
        /// While preforming checks.
        /// </summary>
		static public void Save()
		{
			if(!File.Exists(GLOBALVARS.SettingsPath+ControlSettingsFile))
			{
                DefaultLayout();
                WriteCurrentTheme();
			}
		}
        /// <summary>
        /// Actually preforms the writing required to save the schema
        /// </summary>
        static private void WriteCurrentTheme()
        {
            using (StreamWriter fWrite = File.CreateText(GLOBALVARS.SettingsPath + ControlSettingsFile))
            {
                foreach (KeyValuePair<string, int> pair in InputLayout)
                {
                    fWrite.WriteLine(pair.Key + " "+ pair.Value.GetHashCode().ToString());
                }
            }
        }*/
		
	}
	public struct GLOBALVARS
	{
		static public string SettingsPath = @".\Settings\";
	}
	
	
	
	
}