using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GermanStockPileUI : MonoBehaviour
{

    public int BF109Stockpile;
    public int BF110Stockpile;
    public int HeinkelStockpile;
    public int JunkersStockpile;
    public int stockpileToSend;
    public int inputInt;

    public int FighterPlanesBuilt;
    public int BomberPlanesBuilt;

    GameObject InputObject;
    GameObject reenforceBF109;
    GameObject reenforceBF110;
    GameObject reenforceHeinkel;
    GameObject reenforceJunkers;
    GameObject menuObject;
    GameObject germanAirfield;
    GameObject stockpileTextObject;

    GermanAirfield germanAirfieldScript;
    Menu menuScript;
    InputField inputField;
    Text stockpileTextText;
    Button BF109Button;
    Button BF110Button;
    Button heinkelButton;
    Button junkersButton;

    float timer;

    void Start()
    {

        BF109Stockpile = 60;
        BF110Stockpile = 60;
        HeinkelStockpile = 60;
        JunkersStockpile = 60;
        FighterPlanesBuilt = 6;
        BomberPlanesBuilt = 3;

        InputObject = GameObject.Find("GermanInput");
        reenforceBF109 = GameObject.Find("AddBF109");
        reenforceBF110 = GameObject.Find("AddBF110");
        reenforceHeinkel = GameObject.Find("AddHeinkel");
        reenforceJunkers = GameObject.Find("AddJunkers");
        stockpileTextObject = GameObject.Find("GermanStockpile");
        menuObject = GameObject.Find("Menu");

        menuScript = menuObject.GetComponent<Menu>();
        inputField = InputObject.GetComponent<InputField>();
        stockpileTextText = stockpileTextObject.GetComponent<Text>();

        SetButtons();

    }


    void Update()
    {

        stockpileTextText.text = "BF109 stockpile: " + BF109Stockpile.ToString() +
            "\n" + "BF110 stockpile: " + BF110Stockpile.ToString() +
            "\n" + "Heinkel stockpile: " + HeinkelStockpile.ToString() +
            "\n" + "Junkers stockpile: " + JunkersStockpile.ToString();
        timer -= Time.deltaTime;
        stockpilePlanes();
        CheckButtonAvailable();

    }

    public void PressReenforceBF109()
    {
        if (inputField.text != "") // check field isnt empty
        {
            inputInt = int.Parse(inputField.text);

            if (inputInt < BF109Stockpile)
            {
                stockpileToSend = inputInt;

                germanAirfield = menuScript.GetSelectedObject();
                germanAirfieldScript = germanAirfield.GetComponent<GermanAirfield>();

                germanAirfieldScript.SetIncomingBF109s();
                germanAirfieldScript.SetReenforcingBF109s(stockpileToSend);
                germanAirfieldScript.SetStockpileTimer(20);  // Set time taken to deliver planes!
                BF109Stockpile = BF109Stockpile - stockpileToSend;
                stockpileToSend = 0;
                inputField.text = "0";
            }
        }
    }

    public void PressReenforceBF110()
    {
        if (inputField.text != "")
        {
            inputInt = int.Parse(inputField.text);

            if (inputInt < BF109Stockpile)
            {
                stockpileToSend = inputInt;

                germanAirfield = menuScript.GetSelectedObject();
                germanAirfieldScript = germanAirfield.GetComponent<GermanAirfield>();

                germanAirfieldScript.SetIncomingBF110s();
                germanAirfieldScript.SetReenforcingBF110s(stockpileToSend);
                germanAirfieldScript.SetStockpileTimer(20);
                BF110Stockpile = BF110Stockpile - stockpileToSend;
                stockpileToSend = 0;
                inputField.text = "0";
            }
        }
    }

    public void PressReenforceHeinkel()
    {
        if (inputField.text != "")
        {
            inputInt = int.Parse(inputField.text);

            if (inputInt < BF109Stockpile)
            {
                stockpileToSend = inputInt;

                germanAirfield = menuScript.GetSelectedObject();
                germanAirfieldScript = germanAirfield.GetComponent<GermanAirfield>();

                germanAirfieldScript.SetIncomingHeinkels();
                germanAirfieldScript.SetReenforcingHeinkels(stockpileToSend);
                germanAirfieldScript.SetStockpileTimer(20);
                HeinkelStockpile = HeinkelStockpile - stockpileToSend;
                stockpileToSend = 0;
                inputField.text = "0";
            }
        }
    }

    public void PressReenforceJunkers()
    {
        if (inputField.text != "")
        {
            inputInt = int.Parse(inputField.text);

            if (inputInt < BF109Stockpile)
            {
                stockpileToSend = inputInt;

                germanAirfield = menuScript.GetSelectedObject();
                germanAirfieldScript = germanAirfield.GetComponent<GermanAirfield>();

                germanAirfieldScript.SetIncomingJunkers();
                germanAirfieldScript.SetReenforcingJunkers(stockpileToSend);
                germanAirfieldScript.SetStockpileTimer(20);
                JunkersStockpile = JunkersStockpile - stockpileToSend;
                stockpileToSend = 0;
                inputField.text = "0";
            }
        }
    }

    public int GetStockpileToSend()
    {
        return stockpileToSend;
    }

    void SetButtons()
    {
        BF109Button = reenforceBF109.GetComponent<Button>();
        BF109Button.onClick.AddListener(PressReenforceBF109);

        BF110Button = reenforceBF110.GetComponent<Button>();
        BF110Button.onClick.AddListener(PressReenforceBF110);

        heinkelButton = reenforceHeinkel.GetComponent<Button>();
        heinkelButton.onClick.AddListener(PressReenforceHeinkel);

        junkersButton = reenforceJunkers.GetComponent<Button>();
        junkersButton.onClick.AddListener(PressReenforceJunkers);
    }

    void stockpilePlanes()
    {
        if (timer < 0)
        {
            BF109Stockpile = BF109Stockpile + FighterPlanesBuilt;
            BF110Stockpile = BF110Stockpile + BomberPlanesBuilt;
            JunkersStockpile = JunkersStockpile + BomberPlanesBuilt;
            HeinkelStockpile = HeinkelStockpile + BomberPlanesBuilt;
            timer = 30;
        }
    }

    void CheckButtonAvailable()
    {
        germanAirfield = menuScript.GetSelectedObject();

        if (germanAirfield.tag == "GERMANAIRFIELD")
        {

            germanAirfieldScript = germanAirfield.GetComponent<GermanAirfield>();

            bool bf109 = germanAirfieldScript.GetIncomingBF109s();
            bool bf110 = germanAirfieldScript.GetIncomingBF110s();
            bool heinkel = germanAirfieldScript.GetIncomingHeinkels();
            bool junkers = germanAirfieldScript.GetIncomingJunkers();

            if (bf109 == true)
            {
                reenforceBF109.SetActive(false);
            }
            else
            {
                reenforceBF109.SetActive(true);
            }
            if (bf110 == true)
            {
                reenforceBF110.SetActive(false);
            }
            else
            {
                reenforceBF110.SetActive(true);
            }
            if (heinkel == true)
            {
                reenforceHeinkel.SetActive(false);
            }
            else
            {
                reenforceHeinkel.SetActive(true);
            }
            if (junkers == true)
            {
                reenforceJunkers.SetActive(false);
            }
            else
            {
                reenforceJunkers.SetActive(true);
            }
        }
    }
}
