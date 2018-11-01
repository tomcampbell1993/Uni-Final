using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendGermanBomberButton : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PressButton() // Instantiate code actually in british air fields map buttons script.
    {
        //Debug.Log("Send German Bomber Button Pressed");

        GameObject GBomberNumberTextObject = GameObject.FindGameObjectWithTag("GBOMBERNUMBERTEXT"); // This resets the selected number of planes to send to 0 every time you bring up the new menu
    
        GBomberNumberText GBomberNumberTextScript = GBomberNumberTextObject.GetComponent<GBomberNumberText>();
        GBomberNumberTextScript.ResetBomberWave();

        GameObject germanBomberMapObject = GameObject.Find("GermanBomberMap");
        GermanBomberMap germanBomberMapScript = germanBomberMapObject.GetComponent<GermanBomberMap>();

        germanBomberMapScript.SetIsBeingUsedTrue();
    }
}
