using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BFighterWaveNumber : MonoBehaviour
{

    public GameObject wavesToSendObject;
    public GameObject planesPerWaveObject;
    public GameObject menuObject;
    public GameObject airfieldObject;

    public BFighterPlaneNumber planesPerWaveScript;
    public Menu menuScript;
    public BritishAirfield airfieldScript;
    public Text wavesToSendText;

    public int wavesToSend;
    public int planesPerWave;
    public int storedPlanes;

    void Start()
    {

        wavesToSendObject = GameObject.Find("BFighterWaveNumber");
        wavesToSendText = wavesToSendObject.GetComponent<Text>();

        menuObject = GameObject.Find("Menu");
        menuScript = menuObject.GetComponent<Menu>();

        planesPerWaveObject = GameObject.Find("BFighterPlaneNumber");
        planesPerWaveScript = planesPerWaveObject.GetComponent<BFighterPlaneNumber>();


    }

    // Update is called once per frame
    void Update()
    {

        wavesToSendText.text = "Waves to send: " + wavesToSend.ToString();
        planesPerWave = planesPerWaveScript.GetPlanesPerWave();

    }

    public void IncreaseWaves()
    {
        airfieldObject = menuScript.GetSelectedObject();
        airfieldScript = airfieldObject.GetComponent<BritishAirfield>();
        storedPlanes = airfieldScript.GetSpitfiresStored();

        if ((planesPerWave * (wavesToSend + 1)) < (storedPlanes))
        {
            wavesToSend = wavesToSend + 1;
        }
    }

    public void DecreaseWaves()
    {
        if (wavesToSend > 0)
        {
            wavesToSend = wavesToSend - 1;
        }
    }

    public int GetWaves()
    {
        return wavesToSend;
    }

    public void ResetWavesToSend()
    {
        wavesToSend = 0;
    }
}
