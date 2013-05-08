using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MainGameComponents;

public class pickUp : MonoBehaviour 
{
    public PaintStruct myPaint;
    
    public Type myPaintType;
    
    public Color myBallColor;
    
    public Material myMaterial;

	// Use this for initialization
	void Start () 
    {
        //Blue paint
        if (myBallColor == new Color(0, 0, 1, 0))
        {
            myPaintType = typeof(BlueSplotch);
        }
        //Green paint
        if (myBallColor == new Color(0, 1, 0, 0))
        {
            myPaintType = typeof(GreenSplotch);
        }
        //Red paint
        if (myBallColor == new Color(1, 0, 0, 0))
        {
            myPaintType = typeof(RedSplotch);
        }
        //Yellow paint
        if (myBallColor == new Color(1, 1, 0, 0))
        {
            myPaintType = typeof(BananaSplotch);
        }
        //Grow paint
        if (myBallColor == new Color(0, 0, 0, 0))
        {
            myPaintType = typeof(GrowSplotch);
        }
        //Gravity paint
        if (myBallColor == new Color(1, 1, 1, 0))
        {
            Debug.Log("Gravity Paint");
            myPaintType = typeof(GravityPaint);
            myBallColor = new Color(0.211f, 0, 0.16f);
        }
        myPaint = new PaintStruct(myPaintType, myBallColor, myMaterial);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Paint")
        {
            Debug.Log("PICKUP!");
            col.gameObject.SendMessage("addPaint", myPaint);
            Destroy(this.gameObject);
        }
    }   

	// Update is called once per frame
	void Update () 
    {
        if (myPaintType == typeof(GrowSplotch))
        {
            myBallColor = WorldGlobal.currentColor;
            myMaterial.color = WorldGlobal.currentColor;
        }
	}
}
