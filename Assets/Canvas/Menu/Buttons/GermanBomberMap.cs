using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GermanBomberMap : MonoBehaviour
{

    public bool isBeingUsed;
    public GameObject colerneButtonObject;
    public GameObject coltishallButtonObject;
    public GameObject croydonButtonObject;
    public GameObject exeterButtonObject;
    public GameObject hornchurchButtonObject;
    public GameObject leeonsolentButtonObject;
    public GameObject lympneButtonObject;
    public GameObject manstonButtonObject;
    public GameObject martleshamButtonObject;
    public GameObject warmwellButtonObject;

    public GameObject increaseByThreeButtonObject;
    public GameObject decreaseByThreeButtonObject;
    public GameObject escortIncreaseObject;
    public GameObject escortDecreaseObject;

    public GameObject chooseHeinkelObject;
    public GameObject chooseDornierObject;
    public GameObject chooseJunkersObject;
    public GameObject chooseBF109Object;
    public GameObject chooseBF110Object;
    public GameObject chooseBF109EscortObject;
    public GameObject chooseBF110EscortObject;

    protected Vector3 initialPosition;
    protected Vector3 escortIncreaseInitialPos;
    protected Vector3 escortDecreaseInitialPos;
    protected Vector3 OOB;

    Button GEscortIncreaseButton;
    Button GEscortDecreaseButton;

    // Use this for initialization
    void Start()
    {
        OOB = new Vector3(9999, 9999, 9999);
        initialPosition = transform.position;
        isBeingUsed = false;
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

    public void SetIsBeingUsedTrue()
    {
        isBeingUsed = true;
    }

    public void SetIsBeingUsedFalse()
    {
        isBeingUsed = false;
    }

    void SetButtons()  // Sets targets of british airfields
    {

        colerneButtonObject = GameObject.Find("RAF Colerne");
        Button CEButton = colerneButtonObject.GetComponent<Button>();
        GermanBomberMapButtons colerneButton = colerneButtonObject.GetComponent<GermanBomberMapButtons>();
        colerneButton.SetAssignedAirfield(GameObject.Find("RAF Colerne Airfield"));
        CEButton.onClick.AddListener(colerneButton.PressButton);

        coltishallButtonObject = GameObject.Find("RAF Coltishall");
        Button CLButton = coltishallButtonObject.GetComponent<Button>();
        GermanBomberMapButtons coltishallButton = coltishallButtonObject.GetComponent<GermanBomberMapButtons>();
        coltishallButton.SetAssignedAirfield(GameObject.Find("RAF Coltishall Airfield"));
        CLButton.onClick.AddListener(coltishallButton.PressButton);

        martleshamButtonObject = GameObject.Find("RAF Martlesham");
        Button MMButton = martleshamButtonObject.GetComponent<Button>();
        GermanBomberMapButtons martleshamButton = martleshamButtonObject.GetComponent<GermanBomberMapButtons>();
        martleshamButton.SetAssignedAirfield(GameObject.Find("RAF Martlesham Airfield"));
        MMButton.onClick.AddListener(martleshamButton.PressButton);

        hornchurchButtonObject = GameObject.Find("RAF Hornchurch");
        Button HHButton = hornchurchButtonObject.GetComponent<Button>();
        GermanBomberMapButtons hornchurchButton = hornchurchButtonObject.GetComponent<GermanBomberMapButtons>();
        hornchurchButton.SetAssignedAirfield(GameObject.Find("RAF Hornchurch Airfield"));
        HHButton.onClick.AddListener(hornchurchButton.PressButton);

        croydonButtonObject = GameObject.Find("RAF Croydon");
        Button CNButton = croydonButtonObject.GetComponent<Button>();
        GermanBomberMapButtons croydonButton = croydonButtonObject.GetComponent<GermanBomberMapButtons>();
        croydonButton.SetAssignedAirfield(GameObject.Find("RAF Croydon Airfield"));
        CNButton.onClick.AddListener(croydonButton.PressButton);

        manstonButtonObject = GameObject.Find("RAF Manston");
        Button MNButton = manstonButtonObject.GetComponent<Button>();
        GermanBomberMapButtons manstonButton = manstonButtonObject.GetComponent<GermanBomberMapButtons>();
        manstonButton.SetAssignedAirfield(GameObject.Find("RAF Manston Airfield"));
        MNButton.onClick.AddListener(manstonButton.PressButton);

        lympneButtonObject = GameObject.Find("RAF Lympne");
        Button LEButton = lympneButtonObject.GetComponent<Button>();
        GermanBomberMapButtons lympneButton = lympneButtonObject.GetComponent<GermanBomberMapButtons>();
        lympneButton.SetAssignedAirfield(GameObject.Find("RAF Lympne Airfield"));
        LEButton.onClick.AddListener(lympneButton.PressButton);

        leeonsolentButtonObject = GameObject.Find("RAF Lee on Solent");
        Button LTButton = leeonsolentButtonObject.GetComponent<Button>();
        GermanBomberMapButtons leeonsolentButton = leeonsolentButtonObject.GetComponent<GermanBomberMapButtons>();
        leeonsolentButton.SetAssignedAirfield(GameObject.Find("RAF Lee on Solent Airfield"));
        LTButton.onClick.AddListener(leeonsolentButton.PressButton);

        exeterButtonObject = GameObject.Find("RAF Exeter");
        Button ERButton = exeterButtonObject.GetComponent<Button>();
        GermanBomberMapButtons exeterButton = exeterButtonObject.GetComponent<GermanBomberMapButtons>();
        exeterButton.SetAssignedAirfield(GameObject.Find("RAF Exeter Airfield"));
        ERButton.onClick.AddListener(exeterButton.PressButton);

        warmwellButtonObject = GameObject.Find("RAF Warmwell");
        Button WLButton = warmwellButtonObject.GetComponent<Button>();
        GermanBomberMapButtons warmwellButton = warmwellButtonObject.GetComponent<GermanBomberMapButtons>();
        warmwellButton.SetAssignedAirfield(GameObject.Find("RAF Warmwell Airfield"));
        WLButton.onClick.AddListener(warmwellButton.PressButton);

        // Below sets buttons influencing number of german bomber waves.

        increaseByThreeButtonObject = GameObject.FindGameObjectWithTag("INCREASEBYTHREE");
        Button IBTButton = increaseByThreeButtonObject.GetComponent<Button>();
        GBomberNumberTextButtons increaseByThreeButton = increaseByThreeButtonObject.GetComponent<GBomberNumberTextButtons>();
        IBTButton.onClick.AddListener(increaseByThreeButton.IncreaseButton);

        decreaseByThreeButtonObject = GameObject.FindGameObjectWithTag("DECREASEBYTHREE");
        Button DBTButton = decreaseByThreeButtonObject.GetComponent<Button>();
        GBomberNumberTextButtons decreaseByThreeButton = decreaseByThreeButtonObject.GetComponent<GBomberNumberTextButtons>();
        DBTButton.onClick.AddListener(decreaseByThreeButton.DecreaseButton);

        // Buttons for Escort fighters

        escortIncreaseObject = GameObject.Find("IncreaseGEscort");
        escortIncreaseInitialPos = escortIncreaseObject.transform.position;
        GEscortIncreaseButton = escortIncreaseObject.GetComponent<Button>();
        GEscortNumberButtons GEscortIncreaseButtonScript = escortIncreaseObject.GetComponent<GEscortNumberButtons>();
        GEscortIncreaseButton.onClick.AddListener(GEscortIncreaseButtonScript.IncreaseSize);

        escortDecreaseObject = GameObject.Find("DecreaseGEscort");
        escortDecreaseInitialPos = escortDecreaseObject.transform.position;
        GEscortDecreaseButton = escortDecreaseObject.GetComponent<Button>();
        GEscortNumberButtons GEscortDecreaseButtonScript = escortDecreaseObject.GetComponent<GEscortNumberButtons>();
        GEscortDecreaseButton.onClick.AddListener(GEscortDecreaseButtonScript.DecreaseSize);

        // Buttons to choose Planes

        chooseHeinkelObject = GameObject.Find("ChooseHeinkel");
        Button CHButton = chooseHeinkelObject.GetComponent<Button>();
        GermanChoosePlanes chooseHeinkelScript = chooseHeinkelObject.GetComponent<GermanChoosePlanes>();
        CHButton.onClick.AddListener(chooseHeinkelScript.ChooseHeinkel);

        chooseDornierObject = GameObject.Find("ChooseDornier");
        Button CDButton = chooseDornierObject.GetComponent<Button>();
        GermanChoosePlanes chooseDornierScript = chooseDornierObject.GetComponent<GermanChoosePlanes>();
        CDButton.onClick.AddListener(chooseDornierScript.ChooseDornier);

        chooseJunkersObject = GameObject.Find("ChooseJunkers");
        Button CJButton = chooseJunkersObject.GetComponent<Button>();
        GermanChoosePlanes chooseJunkersScript = chooseJunkersObject.GetComponent<GermanChoosePlanes>();
        CJButton.onClick.AddListener(chooseJunkersScript.ChooseJunkers);

        chooseBF109Object = GameObject.Find("ChooseBF109");
        Button CBF109Button = chooseBF109Object.GetComponent<Button>();
        GermanChoosePlanes chooseBF109Script = chooseBF109Object.GetComponent<GermanChoosePlanes>();
        CBF109Button.onClick.AddListener(chooseBF109Script.ChooseBF109);

        chooseBF110Object = GameObject.Find("ChooseBF110");
        Button CBF110Button = chooseBF110Object.GetComponent<Button>();
        GermanChoosePlanes chooseBF110Script = chooseBF110Object.GetComponent<GermanChoosePlanes>();
        CBF110Button.onClick.AddListener(chooseBF110Script.ChooseBF110);

        chooseBF109EscortObject = GameObject.Find("EscortBF109");
        Button CBF109EButton = chooseBF109EscortObject.GetComponent<Button>();
        GermanChoosePlanes chooseBF109EscortScript = chooseBF109EscortObject.GetComponent<GermanChoosePlanes>();
        CBF109EButton.onClick.AddListener(chooseBF109EscortScript.ChooseEscortBF109);

        chooseBF110EscortObject = GameObject.Find("EscortBF110");
        Button CBF110EButton = chooseBF110EscortObject.GetComponent<Button>();
        GermanChoosePlanes chooseBF110EscortScript = chooseBF110EscortObject.GetComponent<GermanChoosePlanes>();
        CBF110EButton.onClick.AddListener(chooseBF110EscortScript.ChooseEscortBF110);
    }

    public void SetEscortButtonInteractable(bool interact)
    {
        GEscortIncreaseButton.interactable = interact;
        GEscortDecreaseButton.interactable = interact;
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
