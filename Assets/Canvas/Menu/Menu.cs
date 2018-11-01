using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// This is the main UI script. This controls which objects are selected and displays menus depending on which player is being controlled and what they've selected.

public class Menu : MonoBehaviour
{

    public Text selectionText;
    public Text planeSelectionTextText;

    public GameObject sendGermanBomberButtonObject;
    public GameObject sendBritishFighterObject;
    public GameObject britishAirFieldsMapObject;
    public GameObject selectedObject;
    public GameObject britishAirfieldObject;
    public GameObject germanAirfieldObject;
    public GameObject planeSelectionUIObject;
    public GameObject planeSelectionTextObject;
    public Vector3 selectedObjectPosition;

    public GameObject exitPanelObject;
    public GameObject quitObject;
    public GameObject resumeObject;
    public Button quitButton;
    public Button resumeButton;

    // UI Images
    public GameObject BF109Image;
    public GameObject BF110Image;
    public GameObject HE111Image;
    public GameObject JU88Image;
    public GameObject DO17Image;
    public GameObject hurricaneImage;
    public GameObject spitfireImage;

    public BritishAirfield britishAirfieldScript;
    public GermanAirfield germanAirfieldScript;

    protected Vector3 OOB;
    protected Vector3 BMSButtonOriginal;
    protected Vector3 BSFButtonOriginal;
    protected Vector3 SGBButtonOriginal;
    protected Vector3 SBFButtonOriginal;
    protected Vector3 CoverObjectOriginal;
    protected Vector3 PlaneSelectionOriginal;
    protected Vector3 exitPanelOriginal;

    public int player; // Which player is controlling which team. 1 = German, 2 = British.

    // Use this for initialization
    void Start()
    {
        OOB = new Vector3(9999, 9999, 9999); // OOB means OUT OF BOUNDS, this is to remove UI elements from field of view.

        quitObject = GameObject.Find("Quit");
        resumeObject = GameObject.Find("Resume");

        selectionText.text = "Hello";

        selectedObject = GameObject.FindGameObjectWithTag("GROUND");

        SetButtons();
        setImages();
        ClearButtons();
    }

    // Update is called once per frame
    void Update()
    {

        CheckSelection();
        selectedObjectPosition = selectedObject.transform.position;
        keyControl();

    }

    public void SetSelectedObject(GameObject select) // Defines which GameObject is the designated selected object at a given time.
    {
        selectedObject = select;
    }

    void CheckSelection()                       //Checks which object is selected at any given time, calls the xxx selected functions below.
    {
        if (selectedObject)
        {
            if (selectedObject.tag == "GROUND") // Ground Selected
            {
                GroundSelected();
            }

            if (selectedObject.tag == "GERMANAIRFIELD") // German Airfield Selected
            {
                GermanAirFieldSelected();
            }

            if (selectedObject.tag == "BRITISHAIRFIELD") // British Airfield Selected
            {
                BritishAirFieldSelected();
            }

            if (selectedObject.tag == "BF109") // BF109 Selected
            {
                BF109Selected();
            }

            if (selectedObject.tag == "BF110") // BF110 Selected
            {
                BF110Selected();
            }

            if (selectedObject.tag == "HEINKEL") // HEINKEL Selected
            {
                heinkelSelected();
            }

            if (selectedObject.tag == "JUNKERS") // Junkers Selected
            {
                junkersSelected();
            }

            if (selectedObject.tag == "DORNIER") // Dornier Selected
            {
                dornierSelected();
            }

            if (selectedObject.tag == "HURRICANE") // Hurricane Selected
            {
                hurricaneSelected();
            }

            if (selectedObject.tag == "SPITFIRE") // Hurricane Selected
            {
                spitfireSelected();
            }

            else return;
        }
    }

    void GroundSelected()                                                   // xxx selected clears all buttons form canvas menu (sends them away) Then places relevant buttons.
    {
        ClearButtons();
        selectionText.text = "Nothing Selected";
    }

    void GermanAirFieldSelected()
    {
        germanAirfieldObject = selectedObject;
        germanAirfieldScript = germanAirfieldObject.GetComponent<GermanAirfield>();

        ClearButtons();
        selectionText.text = selectedObject.name;

        if (germanAirfieldScript.GetCurrentlySending() == false)
        {
            if (player == 1)
            {
                selectionText.text = selectedObject.name + " is ready for command.";
                sendGermanBomberButtonObject.transform.position = SGBButtonOriginal;
            }
        }

        else
        {
            if (player == 1)
            {
                selectionText.text = selectedObject.name + " is currently launching planes.";
                sendGermanBomberButtonObject.transform.position = OOB;
            }
        }


    }

    void BritishAirFieldSelected()
    {
        britishAirfieldObject = selectedObject;
        britishAirfieldScript = britishAirfieldObject.GetComponent<BritishAirfield>();

        ClearButtons();
        selectionText.text = selectedObject.name;

        if (britishAirfieldScript.GetCurrentlySending() == false) // Checks to see if the airfield is currently launching planes, if it isnt, airfield is selectable and vice versa
        {
            if (player == 2)
            {
                selectionText.text = selectedObject.name + " is ready for command.";
                sendBritishFighterObject.transform.position = SBFButtonOriginal;
            }
        }

        else
        {
            if (player == 2)
            {
                selectionText.text = selectedObject.name + " is currently launching planes";
                sendBritishFighterObject.transform.position = OOB;
            }
        }

    }

    void BF109Selected()
    {
        ClearButtons();
        clearImages();
        BF109Image.SetActive(true);

        selectionText.text = selectedObject.name;
        planeSelectionUIObject.transform.position = PlaneSelectionOriginal;


        MesserschmidtAI bf109 = selectedObject.GetComponent<MesserschmidtAI>();
        int pilot = bf109.GetPilot();
        int engine = bf109.GetEngine();

        string pilotText = "Healthy Pilot";
        string engineText = "Working Engines";
        if (pilot == 1)
        {
            pilotText = "Injured Pilot";
        }
        if (engine == 1)
        {
            engineText = "Damaged Engines";
        }

        int fuel = (int)bf109.getFuel();
        planeSelectionTextText.text = (selectedObject.name + "\n" +
            pilotText + "\n" +
            engineText + "\n" +
            "Plane Integrity: " + bf109.GetIntegrity().ToString() + "\n" +
            "Fuel: " + fuel.ToString() + "\n" +
            "Burst shots remaining: " + bf109.GetAmmunition().ToString());

    }

    void BF110Selected()
    {
        ClearButtons();
        clearImages();
        BF110Image.SetActive(true);

        selectionText.text = selectedObject.name;
        planeSelectionUIObject.transform.position = PlaneSelectionOriginal;


        BF110AI bf110 = selectedObject.GetComponent<BF110AI>();
        int pilot = bf110.GetPilot();
        int engine = bf110.GetEngine();

        string pilotText = "Healthy Pilot";
        string engineText = "Working Engines";
        if (pilot == 1)
        {
            pilotText = "Injured Pilot";
        }
        if (engine == 1)
        {
            engineText = "Damaged Engines";
        }

        int fuel = (int)bf110.getFuel();
        planeSelectionTextText.text = (selectedObject.name + "\n" +
            pilotText + "\n" +
            engineText + "\n" +
            "Plane Integrity: " + bf110.GetIntegrity().ToString() + "\n" +
            "Fuel: " + fuel.ToString() + "\n" +
            "Burst shots remaining: " + bf110.GetAmmunition().ToString());

    }

    void heinkelSelected()
    {
        ClearButtons();
        clearImages();
        HE111Image.SetActive(true);

        selectionText.text = selectedObject.name;
        planeSelectionUIObject.transform.position = PlaneSelectionOriginal;


        Heinkel heinkel = selectedObject.GetComponent<Heinkel>();
        int pilot = heinkel.GetPilot();
        int engine = heinkel.GetEngine();

        string pilotText = "Healthy Pilot";
        string engineText = "Working Engines";
        if (pilot == 1)
        {
            pilotText = "Injured Pilot";
        }
        if (engine == 1)
        {
            engineText = "Damaged Engines";
        }

        bool missionStatus = heinkel.GetMissionComplete();
        string missionString = "";
        if(missionStatus == true)
        {
            missionString = "Heading home.";
        }
        else
        {
            missionString = "On way to target.";
        }
        planeSelectionTextText.text = (selectedObject.name + "\n" +
            pilotText + "\n" +
            engineText + "\n" +
            "Plane Integrity: " + heinkel.GetIntegrity().ToString() + "\n" +
            "Current Mission: " + missionString);

    }

    void junkersSelected()
    {
        ClearButtons();
        clearImages();
        JU88Image.SetActive(true);

        selectionText.text = selectedObject.name;
        planeSelectionUIObject.transform.position = PlaneSelectionOriginal;


        Junkers junkers = selectedObject.GetComponent<Junkers>();
        int pilot = junkers.GetPilot();
        int engine = junkers.GetEngine();

        string pilotText = "Healthy Pilot";
        string engineText = "Working Engines";
        if (pilot == 1)
        {
            pilotText = "Injured Pilot";
        }
        if (engine == 1)
        {
            engineText = "Damaged Engines";
        }

        bool missionStatus = junkers.GetMissionComplete();
        string missionString = "";
        if (missionStatus == true)
        {
            missionString = "Heading home.";
        }
        else
        {
            missionString = "On way to target.";
        }
        planeSelectionTextText.text = (selectedObject.name + "\n" +
            pilotText + "\n" +
            engineText + "\n" +
            "Plane Integrity: " + junkers.GetIntegrity().ToString() + "\n" +
            "Current Mission: " + missionString);

    }

    void dornierSelected()
    {
        ClearButtons();
        clearImages();
        DO17Image.SetActive(true);

        selectionText.text = selectedObject.name;
        planeSelectionUIObject.transform.position = PlaneSelectionOriginal;


        Dornier dornier = selectedObject.GetComponent<Dornier>();
        int pilot = dornier.GetPilot();
        int engine = dornier.GetEngine();

        string pilotText = "Healthy Pilot";
        string engineText = "Working Engines";
        if (pilot == 1)
        {
            pilotText = "Injured Pilot";
        }
        if (engine == 1)
        {
            engineText = "Damaged Engines";
        }

        bool missionStatus = dornier.GetMissionComplete();
        string missionString = "";
        if (missionStatus == true)
        {
            missionString = "Heading home.";
        }
        else
        {
            missionString = "On way to target.";
        }
        planeSelectionTextText.text = (selectedObject.name + "\n" +
            pilotText + "\n" +
            engineText + "\n" +
            "Plane Integrity: " + dornier.GetIntegrity().ToString() + "\n" +
            "Current Mission: " + missionString);

    }

    void hurricaneSelected()
    {
        ClearButtons();
        clearImages();
        hurricaneImage.SetActive(true);

        selectionText.text = selectedObject.name;
        planeSelectionUIObject.transform.position = PlaneSelectionOriginal;


        HurricaneAI hurricane = selectedObject.GetComponent<HurricaneAI>();
        int pilot = hurricane.GetPilot();
        int engine = hurricane.GetEngine();

        string pilotText = "Healthy Pilot";
        string engineText = "Working Engines";
        if (pilot == 1)
        {
            pilotText = "Injured Pilot";
        }
        if (engine == 1)
        {
            engineText = "Damaged Engines";
        }

        int fuel = (int)hurricane.getFuel();
        planeSelectionTextText.text = (selectedObject.name + "\n" +
            pilotText + "\n" +
            engineText + "\n" +
            "Plane Integrity: " + hurricane.GetIntegrity().ToString() + "\n" +
            "Fuel: " + fuel.ToString() + "\n" +
            "Burst shots remaining: " + hurricane.GetAmmunition().ToString());

    }

    void spitfireSelected()
    {
        ClearButtons();
        clearImages();
        spitfireImage.SetActive(true);

        selectionText.text = selectedObject.name;
        planeSelectionUIObject.transform.position = PlaneSelectionOriginal;


        SpitfireAI spitfire = selectedObject.GetComponent<SpitfireAI>();
        int pilot = spitfire.GetPilot();
        int engine = spitfire.GetEngine();

        string pilotText = "Healthy Pilot";
        string engineText = "Working Engines";
        if (pilot == 1)
        {
            pilotText = "Injured Pilot";
        }
        if (engine == 1)
        {
            engineText = "Damaged Engines";
        }

        int fuel = (int)spitfire.getFuel();
        planeSelectionTextText.text = (selectedObject.name + "\n" +
            pilotText + "\n" +
            engineText + "\n" +
            "Plane Integrity: " + spitfire.GetIntegrity().ToString() + "\n" +
            "Fuel: " + fuel.ToString() + "\n" +
            "Burst shots remaining: " + spitfire.GetAmmunition().ToString());

    }

    void ClearButtons()
    {
        sendGermanBomberButtonObject.transform.position = OOB;
        sendBritishFighterObject.transform.position = OOB;
        planeSelectionUIObject.transform.position = OOB;
    }

    void SetButtons()                                                                                               // Assigns buttons. Calls buttons from scripts, Assigns original positions on canvas. Addlisteners.
    {

        sendGermanBomberButtonObject = GameObject.FindGameObjectWithTag("SENDGBBUTTON");
        SGBButtonOriginal = sendGermanBomberButtonObject.transform.position;
        Button SGBButton = sendGermanBomberButtonObject.GetComponent<Button>();
        SendGermanBomberButton sendGermanBomberButton = sendGermanBomberButtonObject.GetComponent<SendGermanBomberButton>();
        SGBButton.onClick.AddListener(sendGermanBomberButton.PressButton);

        sendBritishFighterObject = GameObject.Find("SendBritishFighter");
        SBFButtonOriginal = sendBritishFighterObject.transform.position;
        Button sendBritishFighterButton = sendBritishFighterObject.GetComponent<Button>();
        SendBritishFighter sendBritishFighterScript = sendBritishFighterObject.GetComponent<SendBritishFighter>();
        sendBritishFighterButton.onClick.AddListener(sendBritishFighterScript.PressButton);

        planeSelectionUIObject = GameObject.Find("PlaneSelectionUI");
        PlaneSelectionOriginal = planeSelectionUIObject.transform.position;

        planeSelectionTextObject = GameObject.Find("PlaneSelectionText");
        planeSelectionTextText = planeSelectionTextObject.GetComponent<Text>();

        // exit Game

        exitPanelObject = GameObject.Find("ManualExit");
        exitPanelOriginal = exitPanelObject.transform.position;
        exitPanelObject.transform.position = OOB;

        quitButton = quitObject.GetComponent<Button>();
        quitButton.onClick.AddListener(QuitGame);

        resumeButton = resumeObject.GetComponent<Button>();
        resumeButton.onClick.AddListener(Resume);


    }

    void setImages()
    {
        BF109Image = GameObject.Find("BF109SC");
        BF110Image = GameObject.Find("BF110SC");
        HE111Image = GameObject.Find("HE111SC");
        JU88Image = GameObject.Find("JUNKERSSC");
        DO17Image = GameObject.Find("DORNIERSC");
        hurricaneImage = GameObject.Find("HURRICANESC");
        spitfireImage = GameObject.Find("SPITFIRESC");
    }

    void clearImages()
    {
        BF109Image.SetActive(false);
        BF110Image.SetActive(false);
        HE111Image.SetActive(false);
        JU88Image.SetActive(false);
        DO17Image.SetActive(false);
        hurricaneImage.SetActive(false);
        spitfireImage.SetActive(false);
    }

    public Vector3 GetSelectedPosition()
    {
        return selectedObjectPosition;
    }

    public GameObject GetSelectedObject()
    {
        return selectedObject;
    }

    public void setPlayer(int x)
    {
        player = x;
    }
    public int GetPlayer()
    {
        return player;
    }

    void keyControl()
    {
        if (Input.GetKeyDown("escape"))
        {
            GermanBomberMap gm = GameObject.Find("GermanBomberMap").GetComponent<GermanBomberMap>();
            BritishFighterMap bm = GameObject.Find("BritishFighterMap").GetComponent<BritishFighterMap>();

            if ((gm.GetInUse() ==false) && (bm.GetInUse() == false))
            {
                exitPanelObject.transform.position = exitPanelOriginal;
            }
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Resume()
    {
        Debug.Log("resume");
        exitPanelObject.transform.position = OOB;
    }
}
