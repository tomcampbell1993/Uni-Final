using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BritishFighterMap : MonoBehaviour
{

    bool isBeingUsed;

    protected Vector3 initialPosition;
    protected Vector3 OOB;

    public GameObject colerneObject;
    public GameObject coltishallObject;
    public GameObject croydonObject;
    public GameObject exeterObject;
    public GameObject hornchurchObject;
    public GameObject leeonsolentObject;
    public GameObject lympneObject;
    public GameObject manstonObject;
    public GameObject martleshamObject;
    public GameObject warmwellObject;
    public GameObject escortConvoyObject;

    public GameObject chooseSpitfireObject;
    public GameObject chooseHurricaneObject;

    public GameObject increaseBFighterNumberObject;
    public GameObject decreaseBFighterNumberObject;
    public GameObject increaseBFighterWaveObject;
    public GameObject decreaseBFighterWaveObject;

    void Start()
    {

        initialPosition = transform.position;
        OOB = new Vector3(9999, 9999, 9999);

        SetButtons();

    }

    // Update is called once per frame
    void Update()
    {

        if (isBeingUsed == true)
        {
            transform.position = initialPosition;
        }
        else
        {
            transform.position = OOB;
        }

        KeyControl();
    }

    public void setIsBeingUsed(bool used)
    {
        isBeingUsed = used;
    }

    void SetButtons()
    {
        // Sets all the press button calls for the airfields.

        colerneObject = GameObject.Find("RAF ColerneUK");
        Button colerneButton = colerneObject.GetComponent<Button>();
        BritishFighterMapButtons colerneScript = colerneObject.GetComponent<BritishFighterMapButtons>();
        colerneScript.SetAssignedAirfield(GameObject.Find("RAF Colerne Airfield"));
        colerneButton.onClick.AddListener(colerneScript.PressButton);

        coltishallObject = GameObject.Find("RAF ColtishallUK");
        Button coltishallButton = coltishallObject.GetComponent<Button>();
        BritishFighterMapButtons coltishallScript = coltishallObject.GetComponent<BritishFighterMapButtons>();
        coltishallScript.SetAssignedAirfield(GameObject.Find("RAF Coltishall Airfield"));
        coltishallButton.onClick.AddListener(coltishallScript.PressButton);

        croydonObject = GameObject.Find("RAF CroydonUK");
        Button croydonButton = croydonObject.GetComponent<Button>();
        BritishFighterMapButtons croydonScript = croydonObject.GetComponent<BritishFighterMapButtons>();
        croydonScript.SetAssignedAirfield(GameObject.Find("RAF Croydon Airfield"));
        croydonButton.onClick.AddListener(croydonScript.PressButton);

        exeterObject = GameObject.Find("RAF ExeterUK");
        Button exeterButton = exeterObject.GetComponent<Button>();
        BritishFighterMapButtons exeterScript = exeterObject.GetComponent<BritishFighterMapButtons>();
        exeterScript.SetAssignedAirfield(GameObject.Find("RAF Exeter Airfield"));
        exeterButton.onClick.AddListener(exeterScript.PressButton);

        hornchurchObject = GameObject.Find("RAF HornchurchUK");
        Button hornchucrchButton = hornchurchObject.GetComponent<Button>();
        BritishFighterMapButtons hornchurchScript = hornchurchObject.GetComponent<BritishFighterMapButtons>();
        hornchurchScript.SetAssignedAirfield(GameObject.Find("RAF Hornchurch Airfield"));
        hornchucrchButton.onClick.AddListener(hornchurchScript.PressButton);

        leeonsolentObject = GameObject.Find("RAF Lee on SolentUK");
        Button leeonsolentButton = leeonsolentObject.GetComponent<Button>();
        BritishFighterMapButtons leeonsolentScript = leeonsolentObject.GetComponent<BritishFighterMapButtons>();
        leeonsolentScript.SetAssignedAirfield(GameObject.Find("RAF Lee on Solent Airfield"));
        leeonsolentButton.onClick.AddListener(leeonsolentScript.PressButton);

        lympneObject = GameObject.Find("RAF LympneUK");
        Button lympneButton = lympneObject.GetComponent<Button>();
        BritishFighterMapButtons lympneScript = lympneObject.GetComponent<BritishFighterMapButtons>();
        lympneScript.SetAssignedAirfield(GameObject.Find("RAF Lympne Airfield"));
        lympneButton.onClick.AddListener(lympneScript.PressButton);

        manstonObject = GameObject.Find("RAF ManstonUK");
        Button manstonButton = manstonObject.GetComponent<Button>();
        BritishFighterMapButtons manstonScript = manstonObject.GetComponent<BritishFighterMapButtons>();
        manstonScript.SetAssignedAirfield(GameObject.Find("RAF Manston Airfield"));
        manstonButton.onClick.AddListener(manstonScript.PressButton);

        martleshamObject = GameObject.Find("RAF MartleshamUK");
        Button martleshamButton = martleshamObject.GetComponent<Button>();
        BritishFighterMapButtons martleshamScript = martleshamObject.GetComponent<BritishFighterMapButtons>();
        martleshamScript.SetAssignedAirfield(GameObject.Find("RAF Martlesham Airfield"));
        martleshamButton.onClick.AddListener(martleshamScript.PressButton);

        warmwellObject = GameObject.Find("RAF WarmwellUK");
        Button warmwellButton = warmwellObject.GetComponent<Button>();
        BritishFighterMapButtons warmwellScript = warmwellObject.GetComponent<BritishFighterMapButtons>();
        warmwellScript.SetAssignedAirfield(GameObject.Find("RAF Warmwell Airfield"));
        warmwellButton.onClick.AddListener(warmwellScript.PressButton);

        escortConvoyObject = GameObject.Find("EscortConvoy"); // ESCORT CONVOY BUTTON
        Button escortButton = escortConvoyObject.GetComponent<Button>();
        BritishFighterMapButtons escortScript = escortConvoyObject.GetComponent<BritishFighterMapButtons>();
        escortButton.onClick.AddListener(escortScript.PressEscortButton);
        

        // Below are the increase number of planes and waves buttons

        increaseBFighterNumberObject = GameObject.Find("IncreaseBFNumberButton");
        Button IBFNButton = increaseBFighterNumberObject.GetComponent<Button>();
        BFighterPlaneNumberButtons increasePlaneNumberScript = increaseBFighterNumberObject.GetComponent<BFighterPlaneNumberButtons>();
        IBFNButton.onClick.AddListener(increasePlaneNumberScript.IncreasePlanes);

        decreaseBFighterNumberObject = GameObject.Find("DecreaseBFNumberButton");
        Button DBFNButton = decreaseBFighterNumberObject.GetComponent<Button>();
        BFighterPlaneNumberButtons decreasePlaneNumberScript = decreaseBFighterNumberObject.GetComponent<BFighterPlaneNumberButtons>();
        DBFNButton.onClick.AddListener(decreasePlaneNumberScript.DecreasePlanes);

        increaseBFighterWaveObject = GameObject.Find("IncreaseBFWavesButton");
        Button IBFWButton = increaseBFighterWaveObject.GetComponent<Button>();
        BFighterWaveNumberButtons increaseWaveScript = increaseBFighterWaveObject.GetComponent<BFighterWaveNumberButtons>();
        IBFWButton.onClick.AddListener(increaseWaveScript.IncreaseWaves);

        decreaseBFighterWaveObject = GameObject.Find("DecreaseBFWavesButton");
        Button DBFWButton = decreaseBFighterWaveObject.GetComponent<Button>();
        BFighterWaveNumberButtons decreaseWavesScript = decreaseBFighterWaveObject.GetComponent<BFighterWaveNumberButtons>();
        DBFWButton.onClick.AddListener(decreaseWavesScript.DecreaseWaves);

        // Below are the buttons to choose which fighters fly

        chooseSpitfireObject = GameObject.Find("ChooseSpitfire");
        Button CSButton = chooseSpitfireObject.GetComponent<Button>();
        BritishChoosePlanes chooseSpitfireScript = chooseSpitfireObject.GetComponent<BritishChoosePlanes>();
        CSButton.onClick.AddListener(chooseSpitfireScript.ChooseSpitfire);

        chooseHurricaneObject = GameObject.Find("ChooseHurricane");
        Button CHButton = chooseHurricaneObject.GetComponent<Button>();
        BritishChoosePlanes chooseHurricaneScript = chooseHurricaneObject.GetComponent<BritishChoosePlanes>();
        CHButton.onClick.AddListener(chooseHurricaneScript.ChooseHurricane);

    }

    void KeyControl()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (isBeingUsed == true)
            {
                isBeingUsed = false;
            }
        }
    }

    public bool GetInUse()
    {
        return isBeingUsed;
    }
}
