using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFighterWaveNumberButtons : MonoBehaviour {

    public GameObject BFighterWaveNumberObject;
    public BFighterWaveNumber BFighterWaveNumberScipt;

	void Start () {

        BFighterWaveNumberObject = GameObject.Find("BFighterWaveNumber");
        BFighterWaveNumberScipt = BFighterWaveNumberObject.GetComponent<BFighterWaveNumber>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncreaseWaves()
    {
        BFighterWaveNumberScipt.IncreaseWaves();
    }

    public void DecreaseWaves()
    {
        BFighterWaveNumberScipt.DecreaseWaves();
    }
}
