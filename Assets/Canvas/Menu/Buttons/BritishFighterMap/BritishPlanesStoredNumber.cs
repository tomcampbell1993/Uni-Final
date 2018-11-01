using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BritishPlanesStoredNumber : MonoBehaviour {

    public GameObject spitfiresStoredObject;
    public GameObject hurricanesStoredObject;
    public GameObject menuObject;
    public GameObject britishAirfieldObject;

    public Text spitfiresStoredText;
    public Text hurricanesStoredText;

    public Menu menuScript;

	// Use this for initialization
	void Start () {

        spitfiresStoredObject = GameObject.Find("SpitfiresStored");
        hurricanesStoredObject = GameObject.Find("HurricanesStored");
        menuObject = GameObject.Find("Menu");

        spitfiresStoredText = spitfiresStoredObject.GetComponent<Text>();
        hurricanesStoredText = hurricanesStoredObject.GetComponent<Text>();
        menuScript = menuObject.GetComponent<Menu>();


    }
	
	// Update is called once per frame
	void Update () {

        SetTexts();
	}

    public void SetTexts()
    {
        britishAirfieldObject = menuScript.GetSelectedObject();

        if(britishAirfieldObject.tag == "BRITISHAIRFIELD")
        {

            BritishAirfield britishAirfieldScript = britishAirfieldObject.GetComponent<BritishAirfield>();

            spitfiresStoredText.text = "Planes stored: " + britishAirfieldScript.GetSpitfiresStored().ToString();

            hurricanesStoredText.text = "planes stored: " + britishAirfieldScript.GetHurricanesStored().ToString();

        }
    }
}
