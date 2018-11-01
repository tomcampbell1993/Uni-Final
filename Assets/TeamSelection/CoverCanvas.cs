using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoverCanvas : MonoBehaviour
{

    public GameObject pickGermanyObject;
    public GameObject pickBritainObject;
    public GameObject easyObject;
    public GameObject mediumObject;
    public GameObject hardObject;

    void Start()
    {

        pickBritainObject = GameObject.Find("PickBritish");
        pickGermanyObject = GameObject.Find("PickGerman");
        easyObject = GameObject.Find("Easy");
        mediumObject = GameObject.Find("Medium");
        hardObject = GameObject.Find("Hard");

        setButtons();

    }

    void Update()
    {
        KeyControl();
    }

    void setButtons()
    {

        Button pickGermanButton = pickGermanyObject.GetComponent<Button>();
        CoverButtons pickGermanScript = pickGermanyObject.GetComponent<CoverButtons>();
        pickGermanButton.onClick.AddListener(pickGermanScript.PickGermany);

        Button pickBritishButton = pickBritainObject.GetComponent<Button>();
        CoverButtons pickBritainSctipt = pickBritainObject.GetComponent<CoverButtons>();
        pickBritishButton.onClick.AddListener(pickBritainSctipt.PickBritain);

        Button easyButton = easyObject.GetComponent<Button>();
        CoverButtons easyScript = easyObject.GetComponent<CoverButtons>();
        easyButton.onClick.AddListener(easyScript.PickEasy);

        Button mediumButton = mediumObject.GetComponent<Button>();
        CoverButtons mediumScript = mediumObject.GetComponent<CoverButtons>();
        mediumButton.onClick.AddListener(mediumScript.PickMedium);

        Button hardButton = hardObject.GetComponent<Button>();
        CoverButtons hardScript = hardObject.GetComponent<CoverButtons>();
        hardButton.onClick.AddListener(hardScript.PickHard);

    }

    void KeyControl()
    {
        if (Input.GetKeyDown("return"))
        {
            GameObject intro = GameObject.Find("IntroImage");
            intro.SetActive(false);
        }
    }
}
