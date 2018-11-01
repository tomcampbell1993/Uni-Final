using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GBomberStoredText : MonoBehaviour {

    public GameObject heinkelsStoredObject;
    public GameObject dorniersStoredObject;
    public GameObject junkersStoredObject;
    public GameObject bf109sStoredObject;
    public GameObject bf110sStoredObject;
    public GameObject MenuObject;
    public GameObject germanAirfieldObject;

    // Use this for initialization
    void Start () {
        heinkelsStoredObject = GameObject.Find("HeinkelsStored");
        dorniersStoredObject = GameObject.Find("DorniersStored");
        junkersStoredObject = GameObject.Find("JunkersStored");
        bf109sStoredObject = GameObject.Find("BF109sStored");
        bf110sStoredObject = GameObject.Find("BF110sStored");
        MenuObject = GameObject.Find("Menu");

    }
	
	// Update is called once per frame
	void Update () {

        SetTexts();
    }

    void SetTexts()
    {
        Menu menuScript = MenuObject.GetComponent<Menu>();
        germanAirfieldObject = menuScript.GetSelectedObject();

        if(germanAirfieldObject.tag == "GERMANAIRFIELD")
        {
            GermanAirfield germanAirfieldScript = germanAirfieldObject.GetComponent<GermanAirfield>();

            Text heinkelsStoredText = heinkelsStoredObject.GetComponent<Text>();
            heinkelsStoredText.text = "Planes Stored: " + germanAirfieldScript.GetHeinkelsStored().ToString();

            Text dorniersStoredText = dorniersStoredObject.GetComponent<Text>();
            dorniersStoredText.text = "Planes Stored: " + germanAirfieldScript.GetDorniersStored().ToString();

            Text junkersStoredText = junkersStoredObject.GetComponent<Text>();
            junkersStoredText.text = "Planes Stored: " + germanAirfieldScript.GetJunkersStored().ToString();

            Text bf109sStoredText = bf109sStoredObject.GetComponent<Text>();
            bf109sStoredText.text = "Planes Stored: " + germanAirfieldScript.GetBF109sStored().ToString();

            Text bf110sStoredText = bf110sStoredObject.GetComponent<Text>();
            bf110sStoredText.text = "Planes Stored: " + germanAirfieldScript.GetBF110sStored().ToString();
        }
    }
}
