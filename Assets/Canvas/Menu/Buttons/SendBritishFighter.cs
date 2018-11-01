using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBritishFighter : MonoBehaviour {

    public GameObject britishFighterMapObject;
    public BritishFighterMap britishFighterMapScript;

    void Start () {

        britishFighterMapObject = GameObject.Find("BritishFighterMap");
        britishFighterMapScript = britishFighterMapObject.GetComponent<BritishFighterMap>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PressButton()
    {
        britishFighterMapScript.setIsBeingUsed(true);
    }
}
