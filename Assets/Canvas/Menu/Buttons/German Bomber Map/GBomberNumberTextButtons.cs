using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GBomberNumberTextButtons : MonoBehaviour { // This controls the buttons to increase and decrease the amount of planes the player wants to send, look for GBomberNumberText

    public GameObject GBomberNumberTextObject;
    public GameObject GEscortNumberObject;

	// Use this for initialization
	void Start () {
        GBomberNumberTextObject = GameObject.FindGameObjectWithTag("GBOMBERNUMBERTEXT");
        GEscortNumberObject = GameObject.Find("GEscortNumber");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncreaseButton()
    {
        GBomberNumberText GBomberNumberTextScript = GBomberNumberTextObject.GetComponent<GBomberNumberText>();
        GBomberNumberTextScript.Increase();

        GEscortNumber GEscortNumberScript = GEscortNumberObject.GetComponent<GEscortNumber>();
        GEscortNumberScript.ResetEscort();
    }

    public void DecreaseButton()
    {
        GBomberNumberText GBomberNumberTextScript = GBomberNumberTextObject.GetComponent<GBomberNumberText>();
        GBomberNumberTextScript.Decrease();

        GEscortNumber GEscortNumberScript = GEscortNumberObject.GetComponent<GEscortNumber>();
        GEscortNumberScript.ResetEscort();
    }
}
