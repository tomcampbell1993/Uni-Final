using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BritishFighterMapButtons : MonoBehaviour
{

    public GameObject britishFighterMapObject;
    public GameObject MenuObject;
    public GameObject britishAirFieldObject;
    public GameObject bFighterPlaneNumberObject;
    public GameObject bFighterWaveNumberObject;
    public GameObject assingedAirfield; // This is the airfield assigned to this button.

    public int wavesToSend;
    public int planesPerWave;

    public BritishFighterMap britishFighterMapScript;
    public BFighterPlaneNumber bFighterPlaneNumberScript;
    public BFighterWaveNumber bFighterWaveNumberScript;
    public BritishAirfield britishAirfieldScript;
    public Menu menuScript;

    void Start()
    {

        britishFighterMapObject = GameObject.Find("BritishFighterMap");
        britishFighterMapScript = britishFighterMapObject.GetComponent<BritishFighterMap>();

        MenuObject = GameObject.Find("Menu");
        menuScript = MenuObject.GetComponent<Menu>();

        bFighterPlaneNumberObject = GameObject.Find("BFighterPlaneNumber");
        bFighterPlaneNumberScript = bFighterPlaneNumberObject.GetComponent<BFighterPlaneNumber>();

        bFighterWaveNumberObject = GameObject.Find("BFighterWaveNumber");
        bFighterWaveNumberScript = bFighterWaveNumberObject.GetComponent<BFighterWaveNumber>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PressButton()
    {

        britishAirFieldObject = menuScript.GetSelectedObject();
        britishAirfieldScript = britishAirFieldObject.GetComponent<BritishAirfield>();

        wavesToSend = bFighterPlaneNumberScript.GetWavesToSend();
        planesPerWave = bFighterPlaneNumberScript.GetPlanesPerWave();

        britishAirfieldScript.SetTargetAirfield(assingedAirfield);
        britishAirfieldScript.SetPlanesPerWave(planesPerWave);
        britishAirfieldScript.SetWavesToSend(wavesToSend);

        bFighterPlaneNumberScript.ResetPlanesPerWave();
        bFighterWaveNumberScript.ResetWavesToSend();

        britishFighterMapScript.setIsBeingUsed(false);
    }

    public void PressEscortButton()
    {
        britishAirFieldObject = menuScript.GetSelectedObject();
        britishAirfieldScript = britishAirFieldObject.GetComponent<BritishAirfield>();

        wavesToSend = bFighterPlaneNumberScript.GetWavesToSend();
        planesPerWave = bFighterPlaneNumberScript.GetPlanesPerWave();

        assingedAirfield = GameObject.FindGameObjectWithTag("BRITISHCONVOY");

        britishAirfieldScript.SetTargetAirfield(assingedAirfield);
        britishAirfieldScript.SetPlanesPerWave(planesPerWave);
        britishAirfieldScript.SetWavesToSend(wavesToSend);

        bFighterPlaneNumberScript.ResetPlanesPerWave();
        bFighterWaveNumberScript.ResetWavesToSend();

        britishFighterMapScript.setIsBeingUsed(false);
    }

    public void SetAssignedAirfield(GameObject airfield)
    {
        assingedAirfield = airfield;
    }
}
