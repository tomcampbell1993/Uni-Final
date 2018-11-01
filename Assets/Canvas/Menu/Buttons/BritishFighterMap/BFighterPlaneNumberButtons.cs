using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFighterPlaneNumberButtons : MonoBehaviour {

    public GameObject BFighterPlaneNumberObject;
    public BFighterPlaneNumber BFighterPlaneNumberScript;

	void Start () {

        BFighterPlaneNumberObject = GameObject.Find("BFighterPlaneNumber");
        BFighterPlaneNumberScript = BFighterPlaneNumberObject.GetComponent<BFighterPlaneNumber>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncreasePlanes()
    {
        BFighterPlaneNumberScript.IncreasePlanes();
    }

    public void DecreasePlanes()
    {
        BFighterPlaneNumberScript.DecreasePlanes();
    }
}
