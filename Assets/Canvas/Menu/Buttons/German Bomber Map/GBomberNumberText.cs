using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GBomberNumberText : MonoBehaviour
{

    public int bombersToSendText;
    public int bomberWave;
    public Text numberText;
    public GameObject MenuObject;
    public GameObject germanAirfieldObject;

    public int planeType;
    // 1 BF109
    // 2 BF110
    // 3 Heinkell HE111
    // 4 Dornier Do17
    // 5 Junkers Ju 88

    void Start()
    {

        planeType = 3;
        bombersToSendText = 0;
        bomberWave = 0; // This is where it is actually defined how many waves of 3 bombers will be sent
        MenuObject = GameObject.Find("Menu");

    }

    void Update()
    {
        numberText.text = "Planes to send: " + bombersToSendText;
    }

    public void Increase() // makes sure no more planes cannot be sent that don't exist
    {
        Menu MenuScript = MenuObject.GetComponent<Menu>();
        germanAirfieldObject = MenuScript.GetSelectedObject();
        GermanAirfield germanAirfieldScript = germanAirfieldObject.GetComponent<GermanAirfield>();

        if (planeType == 3)
        {
            if ((bombersToSendText + 3) <= (germanAirfieldScript.heinkelsStored))
            {
                bombersToSendText = bombersToSendText + 3;
                bomberWave = bomberWave + 1;
            }
        }

        if (planeType == 4)
        {
            if ((bombersToSendText + 3) <= (germanAirfieldScript.dorniersStored))
            {
                bombersToSendText = bombersToSendText + 3;
                bomberWave = bomberWave + 1;
            }
        }

        if (planeType == 5)
        {
            if ((bombersToSendText + 3) <= (germanAirfieldScript.junkersStored))
            {
                bombersToSendText = bombersToSendText + 3;
                bomberWave = bomberWave + 1;
            }
        }

        if (planeType == 1)
        {
            if ((bombersToSendText + 3) <= (germanAirfieldScript.bf109sStored))
            {
                bombersToSendText = bombersToSendText + 3;
                bomberWave = bomberWave + 1;
            }
        }

        if (planeType == 2)
        {
            if ((bombersToSendText + 3) <= (germanAirfieldScript.bf110sStored))
            {
                bombersToSendText = bombersToSendText + 3;
                bomberWave = bomberWave + 1;
            }
        }

    }

    public void Decrease()
    {
        if (bombersToSendText > 0)
        {
            bombersToSendText = bombersToSendText - 3;
            bomberWave = bomberWave - 1;
        }
    }

    public int GetBomberWave()
    {
        return bomberWave;
    }

    public void ResetBomberWave()
    {
        bombersToSendText = 0;
        bomberWave = 0;
    }

    public void SetPlaneType(int plane)
    {
        planeType = plane;
    }

}
