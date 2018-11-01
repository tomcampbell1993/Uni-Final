using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectManager : MonoBehaviour
{

    public string objectName;
    public GameObject menuObject;

	// Use this for initialization
	void Start () {
        menuObject = GameObject.Find("Menu");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSelection(GameObject select)
    {
        Menu menuScript = menuObject.GetComponent<Menu>();
        menuScript.SetSelectedObject(select);
    }

}
