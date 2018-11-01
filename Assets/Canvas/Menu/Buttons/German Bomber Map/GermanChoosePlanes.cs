using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GermanChoosePlanes : MonoBehaviour {

    public GameObject menuObject;
    public GameObject airfieldObject;
    public GameObject germanBomberMapObject;
    public GameObject GBomberNumberTextObject;

    public Menu menuScript;
    public GermanBomberMap germanBomberMapScript;
    public GermanAirfield airfieldScript;
    public GBomberNumberText GbomberNumberTextScript;

	// Use this for initialization
	void Start () {

        menuObject = GameObject.Find("Menu");
        menuScript = menuObject.GetComponent<Menu>();

        germanBomberMapObject = GameObject.Find("GermanBomberMap");
        germanBomberMapScript = germanBomberMapObject.GetComponent<GermanBomberMap>();

        GBomberNumberTextObject = GameObject.Find("GBomberNumberText");
        GbomberNumberTextScript = GBomberNumberTextObject.GetComponent<GBomberNumberText>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChooseHeinkel()
    {
        airfieldObject = menuScript.GetSelectedObject();
        airfieldScript = airfieldObject.GetComponent<GermanAirfield>();

        airfieldScript.SetPlaneType(3);
        germanBomberMapScript.SetEscortButtonInteractable(true);
        GbomberNumberTextScript.ResetBomberWave();
        GbomberNumberTextScript.SetPlaneType(3);
    }

    public void ChooseDornier()
    {
        airfieldObject = menuScript.GetSelectedObject();
        airfieldScript = airfieldObject.GetComponent<GermanAirfield>();

        airfieldScript.SetPlaneType(4);
        germanBomberMapScript.SetEscortButtonInteractable(true);
        GbomberNumberTextScript.ResetBomberWave();
        GbomberNumberTextScript.SetPlaneType(4);
    }

    public void ChooseJunkers()
    {
        airfieldObject = menuScript.GetSelectedObject();
        airfieldScript = airfieldObject.GetComponent<GermanAirfield>();

        airfieldScript.SetPlaneType(5);
        germanBomberMapScript.SetEscortButtonInteractable(true);
        GbomberNumberTextScript.ResetBomberWave();
        GbomberNumberTextScript.SetPlaneType(5);
    }

    public void ChooseBF109()
    {
        airfieldObject = menuScript.GetSelectedObject();
        airfieldScript = airfieldObject.GetComponent<GermanAirfield>();

        airfieldScript.SetPlaneType(1);
        germanBomberMapScript.SetEscortButtonInteractable(false);
        GbomberNumberTextScript.ResetBomberWave();
        GbomberNumberTextScript.SetPlaneType(1);
    }

    public void ChooseBF110()
    {
        airfieldObject = menuScript.GetSelectedObject();
        airfieldScript = airfieldObject.GetComponent<GermanAirfield>();

        airfieldScript.SetPlaneType(2);
        germanBomberMapScript.SetEscortButtonInteractable(false);
        GbomberNumberTextScript.ResetBomberWave();
        GbomberNumberTextScript.SetPlaneType(2);
    }

    public void ChooseEscortBF109()
    {
        airfieldObject = menuScript.GetSelectedObject();
        airfieldScript = airfieldObject.GetComponent<GermanAirfield>();

        airfieldScript.SetEscortType(1);
        GbomberNumberTextScript.ResetBomberWave();
    }

    public void ChooseEscortBF110()
    {
        airfieldObject = menuScript.GetSelectedObject();
        airfieldScript = airfieldObject.GetComponent<GermanAirfield>();

        airfieldScript.SetEscortType(2);
        GbomberNumberTextScript.ResetBomberWave();
    }
}
