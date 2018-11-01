using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoverButtons : MonoBehaviour {

    public GameObject coverCanvasObject;
    public GameObject menuObject;
    public GameObject menuImageObject;
    public GameObject gameTimerObject;

    public Image menuImage;
    public Menu menuScript;
    public SingleAI singleAI;



	void Start () {

        coverCanvasObject = GameObject.Find("CoverCanvas");
        menuImageObject = GameObject.Find("UICoverImage");
        menuObject = GameObject.Find("Menu");
        gameTimerObject = GameObject.Find("GameTimer");

        menuScript = menuObject.GetComponent<Menu>();
        menuImage = menuImageObject.GetComponent<Image>();
        singleAI = gameTimerObject.GetComponent<SingleAI>();
    }
	
	void Update () {
		
	}

    public void PickBritain()
    {
        Debug.Log("Britain");
        menuScript.setPlayer(2);
        menuImage.color = new Color32(60, 60, 140, 255);
        coverCanvasObject.SetActive(false);
    }

    public void PickGermany()
    {
        Debug.Log("Germany");
        menuScript.setPlayer(1);
        menuImage.color = new Color32(140, 60, 60, 255);
        coverCanvasObject.SetActive(false);
    }

    public void PickEasy()
    {
        singleAI.SetEasy();
    }
    public void PickMedium()
    {
        singleAI.SetMedium();
    }
    public void PickHard()
    {
        singleAI.SetHard();
    }
}
