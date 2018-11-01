using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BritishChoosePlanes : MonoBehaviour
{

    public GameObject menuObject;
    public GameObject airfield;
    public GameObject bFighterPlaneNumberObject;
    public GameObject bFighterWaveNumberObject;

    public Menu menuScript;
    public BritishAirfield airfieldScript;
    public BFighterPlaneNumber bFighterPlaneNumberScript;
    public BFighterWaveNumber bFighterWaveNumberScript;

    // Use this for initialization
    void Start()
    {
        menuObject = GameObject.Find("Menu");

        menuScript = menuObject.GetComponent<Menu>();

        bFighterPlaneNumberObject = GameObject.Find("BFighterPlaneNumber");
        bFighterPlaneNumberScript = bFighterPlaneNumberObject.GetComponent<BFighterPlaneNumber>();

        bFighterWaveNumberObject = GameObject.Find("BFighterWaveNumber");
        bFighterWaveNumberScript = bFighterWaveNumberObject.GetComponent<BFighterWaveNumber>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChooseSpitfire()
    {
        airfield = menuScript.GetSelectedObject();
        airfieldScript = airfield.GetComponent<BritishAirfield>();

        airfieldScript.SetFighterType(1);

        bFighterPlaneNumberScript.ResetPlanesPerWave();
        bFighterWaveNumberScript.ResetWavesToSend();
    }

    public void ChooseHurricane()
    {
        airfield = menuScript.GetSelectedObject();
        airfieldScript = airfield.GetComponent<BritishAirfield>();

        airfieldScript.SetFighterType(2);

        bFighterPlaneNumberScript.ResetPlanesPerWave();
        bFighterWaveNumberScript.ResetWavesToSend();
    }
}
