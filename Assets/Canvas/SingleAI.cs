using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAI : MonoBehaviour {

    public List<GameObject> germanAirfieldList;
    public List<GameObject> britishAirfieldList;
    public Menu menuScript;

    public int difficultyChance;

    // Script is currently attached to the GameTimer Game Object.

	void Start () {

        difficultyChance = 24;

        menuScript = GameObject.Find("Menu").GetComponent<Menu>();

        germanAirfieldList = new List<GameObject>();
        britishAirfieldList = new List<GameObject>();

        foreach (GameObject germanAirfield in GameObject.FindGameObjectsWithTag("GERMANAIRFIELD"))
        {
            germanAirfieldList.Add(germanAirfield);
        }
        foreach (GameObject britishAirfield in GameObject.FindGameObjectsWithTag("BRITISHAIRFIELD"))
        {
            britishAirfieldList.Add(britishAirfield);
        }

    }

	void Update () {

	}

    public void GermanAI() // called in game timer
    {
        if(menuScript.GetPlayer() == 2)
        {
            for (int i = 0; i < germanAirfieldList.Count; i++)
            {
                GameObject germanObject = germanAirfieldList[i];
                GermanAirfield germanAirfield = germanObject.GetComponent<GermanAirfield>();

                // Below is Bomber wave with escort.

                int randomGerman = Random.Range(1, difficultyChance); //  chance of an airfield sending planes, there are 9 german airfields.
                if (randomGerman == 1)
                {
                    germanAirfield.SetPlaneType(Random.Range(3, 6));
                    germanAirfield.SetEscortType(Random.Range(1, 3)); // For some reason, the last number of each random range is never called.
                    germanAirfield.SetEscortsPerWave(Random.Range(2, 9));

                    int britishAirfieldNumber = Random.Range(0, 9);
                    germanAirfield.SetTargetAirfield(britishAirfieldList[britishAirfieldNumber]); // randomly pick ONE british airfield

                    germanAirfield.SetWavesToSend(Random.Range(1, 4));
                }
            }
        }
        
    }

    public void BritishAI()
    {
        if(menuScript.GetPlayer() == 1)
        {
            for(int i = 0; i <britishAirfieldList.Count; i++)
            {
                GameObject britishObject = britishAirfieldList[i];
                BritishAirfield britishAirfield = britishObject.GetComponent<BritishAirfield>();

                int randomBritish = Random.Range(1, difficultyChance);
                if(randomBritish == 1)
                {
                    britishAirfield.SetFighterType(Random.Range(1, 3));
                    britishAirfield.SetPlanesPerWave(5);

                    int britishAirfieldNumber = Random.Range(0, 9);
                    britishAirfield.SetTargetAirfield(britishAirfieldList[britishAirfieldNumber]); // randomly pick ONE british airfield

                    britishAirfield.SetWavesToSend(Random.Range(1, 5));

                }
            }
        }
    }

    public void SetEasy()
    {
        difficultyChance = 36;
    }
    public void SetMedium()
    {
        difficultyChance = 24;
    }
    public void SetHard()
    {
        difficultyChance = 16;
    }

}
