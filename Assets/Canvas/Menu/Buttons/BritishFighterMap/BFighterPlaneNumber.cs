using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BFighterPlaneNumber : MonoBehaviour {

    public GameObject planesPerWaveObject;
    public GameObject BFighterWaveNumberObject;
    public GameObject menuObject;
    public GameObject airfieldObject;

    public Menu menuScript;
    public BritishAirfield airfieldScript;
    public Text planesPerWaveText;

    public BFighterWaveNumber BFighterWaveNumberScript;

    public int planesPerWave;
    public int wavesToSend;
    public int totalPlanes;
    public int planesStored;

	// Use this for initialization
	void Start () {

        planesPerWave = 0;
        wavesToSend = 0;

        planesPerWaveObject = GameObject.Find("BFighterPlaneNumber");
        BFighterWaveNumberObject = GameObject.Find("BFighterWaveNumber");

        menuObject = GameObject.Find("Menu");
        menuScript = menuObject.GetComponent<Menu>();

        BFighterWaveNumberScript = BFighterWaveNumberObject.GetComponent<BFighterWaveNumber>();

        planesPerWaveText = planesPerWaveObject.GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {

        wavesToSend = BFighterWaveNumberScript.GetWaves();
        totalPlanes = planesPerWave * wavesToSend;

        planesPerWaveText.text = "Planes per wave: " + planesPerWave.ToString() + ". Total planes: " + totalPlanes.ToString();

	}

    public void IncreasePlanes()
    {

        airfieldObject = menuScript.GetSelectedObject();
        airfieldScript = airfieldObject.GetComponent<BritishAirfield>();

        planesStored = airfieldScript.GetSpitfiresStored();

        if((planesPerWave == 0) && (planesStored > 0))
        {
            planesPerWave = 1;
        }
        else if(((planesPerWave + 2)*wavesToSend) < planesStored)
        {
            planesPerWave = planesPerWave + 2;
        }
    }

    public void DecreasePlanes()
    {
        if (planesPerWave == 0)
        {
            return;
        }
        if(planesPerWave == 1)
        {
            planesPerWave = 0;
        }
        if(planesPerWave > 1)
        {
            planesPerWave = planesPerWave - 2;
        }
    }

    public int GetPlanesPerWave()
    {
        return planesPerWave;
    }

    public int GetWavesToSend()
    {
        return wavesToSend;
    }

    public void ResetPlanesPerWave()
    {
        planesPerWave = 0;
    }
}
