using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GermanBomberMapButtons : MonoBehaviour
{

    public int wavesToSend;
    public int escortsPerWave;
    public GameObject GEscortNumberObject;
    public GameObject GBomberNumberTextObject;
    public GameObject menuObject;
    public GameObject germanAirfieldObject;
    public GameObject assignedAirfield; // This assings the button an airfield to target.

    public GameObject germanBomberMapObject;

    // Use this for initialization
    void Start()
    {
        GBomberNumberTextObject = GameObject.FindGameObjectWithTag("GBOMBERNUMBERTEXT");
        GEscortNumberObject = GameObject.Find("GEscortNumber");
        menuObject = GameObject.Find("Menu");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {

    }

    public void PressButton() // Sends the amount of selected bomber waves to the selected german airfield. and designates the german airfield that it is the original airfield.
    {                           // Selected number of waves sent found in GBomberNumberText script.

        GBomberNumberText GBomberNumberTextScript = GBomberNumberTextObject.GetComponent<GBomberNumberText>();
        wavesToSend = GBomberNumberTextScript.GetBomberWave();

        GEscortNumber GEscortNumberScript = GEscortNumberObject.GetComponent<GEscortNumber>();
        escortsPerWave = GEscortNumberScript.GetEscortsPerWave();

        Menu menuScript = menuObject.GetComponent<Menu>();
        germanAirfieldObject = menuScript.GetSelectedObject();

        GermanAirfield germanAirfieldScript = germanAirfieldObject.GetComponent<GermanAirfield>();

        germanAirfieldScript.SetTargetAirfield(assignedAirfield); // sets the target airfield that planes from this airfield will go to.
        germanAirfieldScript.SetWavesToSend(wavesToSend);
        germanAirfieldScript.SetEscortsPerWave(escortsPerWave);

        // removes map from screen
        germanBomberMapObject = GameObject.Find("GermanBomberMap");
        GermanBomberMap germanBomberMapScript = germanBomberMapObject.GetComponent<GermanBomberMap>();

        germanBomberMapScript.SetIsBeingUsedFalse();
    }

    public void SetAssignedAirfield(GameObject airfield)
    {
        assignedAirfield = airfield;
    }
}
