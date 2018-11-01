using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BritishStockpileUI : MonoBehaviour
{

    public int hurricaneStockpile;
    public int spitfireStockpile;
    public int stockpileToSend;
    public int inputInt;
    public int planesBuilt;

    GameObject fighterInputObject;
    GameObject reenforceSpitfire;
    GameObject reenforceHurricane;
    GameObject menuObject;
    GameObject britishAirfield;
    GameObject stockpileTextObject;

    BritishAirfield britishAirfieldScript;
    Menu menuScript;
    InputField fighterInputInput;
    Text stockpileTextText;
    Button spitfireButton;
    Button hurricaneButton;

    float timer;

    void Start()
    {

        hurricaneStockpile = 60;
        spitfireStockpile = 60;
        planesBuilt = 5;

        fighterInputObject = GameObject.Find("FighterInput");
        reenforceSpitfire = GameObject.Find("AddSpitfires");
        reenforceHurricane = GameObject.Find("AddHurricanes");
        stockpileTextObject = GameObject.Find("StockpileText");
        menuObject = GameObject.Find("Menu");

        menuScript = menuObject.GetComponent<Menu>();
        fighterInputInput = fighterInputObject.GetComponent<InputField>();
        stockpileTextText = stockpileTextObject.GetComponent<Text>();

        SetButtons();

    }


    void Update()
    {

        stockpileTextText.text = "Spitfire stockpile: " + spitfireStockpile.ToString() + "\n" + "Hurricane stockpile: " + hurricaneStockpile.ToString();
        timer -= Time.deltaTime;
        stockpilePlanes();
        CheckButtonAvailable();

    }

    public void PressReenforceSpitfire()
    {
        if (fighterInputInput.text != "") // check field isnt empty!
        {
            inputInt = int.Parse(fighterInputInput.text);

            if (inputInt < spitfireStockpile)
            {
                stockpileToSend = inputInt;

                britishAirfield = menuScript.GetSelectedObject();
                britishAirfieldScript = britishAirfield.GetComponent<BritishAirfield>();

                britishAirfieldScript.SetIncomingSpitfires();
                britishAirfieldScript.SetReenforcingSpitfires(stockpileToSend);
                britishAirfieldScript.SetStockpileTimer(20);  // set time taken to deliver planes
                spitfireStockpile = spitfireStockpile - stockpileToSend;
                stockpileToSend = 0;
                fighterInputInput.text = "0";
            }
        }

    }

    public void PressReenforceHurricane()
    {
        if (fighterInputInput.text != "")
        {
            inputInt = int.Parse(fighterInputInput.text);

            if (inputInt < hurricaneStockpile)
            {
                stockpileToSend = inputInt;

                britishAirfield = menuScript.GetSelectedObject();
                britishAirfieldScript = britishAirfield.GetComponent<BritishAirfield>();

                britishAirfieldScript.SetIncomingHurricanes();
                britishAirfieldScript.SetReenforcingHurricanes(stockpileToSend);
                britishAirfieldScript.SetStockpileTimer(20);
                hurricaneStockpile = hurricaneStockpile - stockpileToSend;
                stockpileToSend = 0;
                fighterInputInput.text = "0";
            }
        }
    }

    public int GetStockpileToSend()
    {
        return stockpileToSend;
    }

    void SetButtons()
    {
        spitfireButton = reenforceSpitfire.GetComponent<Button>();
        spitfireButton.onClick.AddListener(PressReenforceSpitfire);

        hurricaneButton = reenforceHurricane.GetComponent<Button>();
        hurricaneButton.onClick.AddListener(PressReenforceHurricane);
    }

    void stockpilePlanes()
    {
        if (timer < 0)
        {
            hurricaneStockpile = hurricaneStockpile + planesBuilt;
            spitfireStockpile = spitfireStockpile + planesBuilt;
            timer = 30;
            if (planesBuilt > 3)
            {
                planesBuilt = planesBuilt - 1;
            }
        }
    }

    public void AddPlanesBuilt(int planes)
    {
        planesBuilt = planesBuilt + planes;
    }

    void CheckButtonAvailable()
    {
        britishAirfield = menuScript.GetSelectedObject();

        if (britishAirfield.tag == "BRITISHAIRFIELD")
        {

            britishAirfieldScript = britishAirfield.GetComponent<BritishAirfield>();

            bool spitfire = britishAirfieldScript.GetIncomingSpitfires();
            bool hurricane = britishAirfieldScript.GetIncomingHurricanes();

            if (spitfire == true)
            {
                reenforceSpitfire.SetActive(false);
            }
            else
            {
                reenforceSpitfire.SetActive(true);
            }

            if (hurricane == true)
            {
                reenforceHurricane.SetActive(false);
            }
            else
            {
                reenforceHurricane.SetActive(true);
            }
        }
    }
}
