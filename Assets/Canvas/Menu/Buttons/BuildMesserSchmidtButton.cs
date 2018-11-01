using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildMesserSchmidtButton : MonoBehaviour
{

    public GameObject messerschmidt;
    public MesserschmidtAI messerschmidtAI;

    public GameObject menuObject;
    public Vector3 spawnPosition;

    // Use this for initialization
    void Start()
    {
        menuObject = GameObject.Find("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        Menu menuScript = menuObject.GetComponent<Menu>();
        spawnPosition = menuScript.GetSelectedPosition();

    }

    public void PressButton()
    {
        Debug.Log("Messerschmidt build Button pressed");

        GameObject messerschmidtInstance;
        messerschmidtInstance = Instantiate(messerschmidt, spawnPosition, transform.root.rotation) as GameObject;  // Creates a messershcmidt at the airfield


        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("BRITISHFIGHTER")) // adds enemies to list of newly created plane
        {
            messerschmidtAI = messerschmidtInstance.GetComponent<MesserschmidtAI>();
            messerschmidtAI.AddToTargetsList(spitfire);
        }
    }
}
