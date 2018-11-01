using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneStatScreens : MonoBehaviour {

    public GameObject menuObject;
    public GameObject planeSeletionObject;
    public GameObject selectedObject;

    Menu menuScript;
    Text planeSelectionText;

	void Start () {
        menuObject = GameObject.Find("Menu");
        menuScript = menuObject.GetComponent<Menu>();
	}

	void Update () {
		
	}

    void checkSelection()
    {
        selectedObject = menuScript.GetSelectedObject();
    }
}
