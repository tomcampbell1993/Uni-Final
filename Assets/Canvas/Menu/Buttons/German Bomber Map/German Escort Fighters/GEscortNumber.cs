using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GEscortNumber : MonoBehaviour {

    public int planesPerWave;
    public int totalPlanes;
    public GameObject GEscortNumberTextObject;
    public GameObject GBomberNumberObject;
    public GameObject menuObject;
    public GameObject germanAirfieldObject;

    // Use this for initialization
    void Start () {

        planesPerWave = 0;
        menuObject = GameObject.Find("Menu");
        GBomberNumberObject = GameObject.Find("GBomberNumberText");
        GEscortNumberTextObject = GameObject.Find("GEscortNumber");

    }
	
	// Update is called once per frame
	void Update () {

        GBomberNumberText GbomberNumberScript = GBomberNumberObject.GetComponent<GBomberNumberText>();
        totalPlanes = (GbomberNumberScript.GetBomberWave()) * (planesPerWave);

        Text GEscortNumberText = GEscortNumberTextObject.GetComponent<Text>();
        GEscortNumberText.text = "Escorts per wave: " + planesPerWave.ToString() + ". Total Planes: " + totalPlanes.ToString();

        Menu menuScript = menuObject.GetComponent<Menu>();

        germanAirfieldObject = menuScript.GetSelectedObject();
        if(germanAirfieldObject == GameObject.FindGameObjectWithTag("GERMANAIRFIELD"))
        {
            GermanAirfield germanAirfieldScript = germanAirfieldObject.GetComponent<GermanAirfield>();

            if (totalPlanes > germanAirfieldScript.GetBF109sStored())
            {
                ResetEscort();
            }
        }

    }

    public void IncreasePlanes()
    {
        int escortType; // 1 is bf109, 2 is bf110


        Menu menuScript = menuObject.GetComponent<Menu>();

        germanAirfieldObject = menuScript.GetSelectedObject();
        GermanAirfield germanAirfieldScript = germanAirfieldObject.GetComponent<GermanAirfield>();
        escortType = germanAirfieldScript.GetEscortType();

        GBomberNumberText GbomberNumberScript = GBomberNumberObject.GetComponent<GBomberNumberText>(); // predicts the next number of planes if waves were to increase
        int nextTotalPlanes = (GbomberNumberScript.GetBomberWave()) * (planesPerWave + 1);

        if(escortType == 1)
        {
            if (nextTotalPlanes > (germanAirfieldScript.GetBF109sStored())) // checks if too many planes would have to exist
            {
                return;
            }
        }
        
        if (escortType == 2)
        {
            if (nextTotalPlanes > (germanAirfieldScript.GetBF110sStored())) // checks if too many planes would have to exist
            {
                return;
            }
        }

        planesPerWave = planesPerWave + 1;
    }

    public void DecreasePlanes()
    {
        if(planesPerWave > 0)
        {
            planesPerWave = planesPerWave - 1;
        }
    }

    public void ResetEscort()
    {
        planesPerWave = 0;
    }

    public int GetEscortsPerWave()
    {
        return planesPerWave;
    }
}
