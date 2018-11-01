using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BritishAirfield : BritishBuilding
{
    public int wavesToSend;
    public int planesPerWave;
    public int spitfiresStored;
    public int hurricanesStored;
    public int fightertype; // fighter type 1 is Spitfire, 2 is hurricane.
    public int reenforcingSpitfires;
    public int reenforcingHurricanes;

    public GameObject spitfire;
    public GameObject hurricane;
    public GameObject targetAirfield;

    public float timer;
    public float stockPileTimer;
    public bool currentlySending;
    bool hurricanesIncoming;
    bool spitfiresIncoming;

    // Use this for initialization
    void Start()
    {
        currentlySending = false;
        objectName = gameObject.name;
        menuObject = GameObject.Find("Menu");
        wavesToSend = 0;

        hurricanesStored = 60;
        spitfiresStored = 60;
        fightertype = 1;
    }

    // Update is called once per frame
    void Update()
    {
        stockPileTimer -= Time.deltaTime;
        timer -= Time.deltaTime;
        Spawning();
        ReenforceSpitfires();
        ReenforceHurricanes();
    }

    private void OnMouseDown()
    {
        if (!IsPointerOverUIObject())
        {
            SetSelection(transform.root.gameObject);
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void Spawning()
    {
        if(wavesToSend > 0)
        {
            currentlySending = true;

            if(timer < 0)
            {
                if(fightertype == 1)
                {
                    SpawnSpitfires();
                }
                if(fightertype == 2)
                {
                    SpawnHurricanes();
                }
                wavesToSend = wavesToSend - 1;
                timer = 2;
            }
        }

        if (wavesToSend == 0)
        {
            currentlySending = false;
        }

    }

    void SpawnSpitfires()
    {

        int xl = 0;
        int zl = 0;
        int xr = 0;
        int zr = 0;

        if(planesPerWave == 1)
        {
            GameObject spitfireInstanceSingle;
            spitfireInstanceSingle = Instantiate(spitfire, transform.position, transform.rotation) as GameObject;

            SpitfireAI spitfireAI = spitfireInstanceSingle.GetComponent<SpitfireAI>();
            spitfireAI.SetDefaultTarget(targetAirfield);
            spitfireAI.SetOriginalAirbase(transform.root.gameObject);

            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF109")) // add all current BF109s to list of targets
            {
                spitfireAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF110")) // add all current BF110s to list of targets
            {
                spitfireAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("HEINKEL")) // add all current BF110s to list of targets
            {
                spitfireAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("JUNKERS")) // add all current BF110s to list of targets
            {
                spitfireAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("DORNIER")) // add all current BF110s to list of targets
            {
                spitfireAI.AddToTargetsList(germanFighter);
            }

            spitfiresStored = spitfiresStored - 1;
        }

        if(planesPerWave > 1)
        {
            int wingSize = ((planesPerWave - 1)/2);

            GameObject spitfireInstanceMiddle;
            spitfireInstanceMiddle = Instantiate(spitfire, transform.position, transform.rotation) as GameObject;

            SpitfireAI spitfireAI = spitfireInstanceMiddle.GetComponent<SpitfireAI>();
            spitfireAI.SetDefaultTarget(targetAirfield);
            spitfireAI.SetOriginalAirbase(transform.root.gameObject);

            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF109")) // targets for middle (AI)
            {
                spitfireAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF110"))
            {
                spitfireAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("HEINKEL")) // add all current BF110s to list of targets
            {
                spitfireAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("JUNKERS")) // add all current BF110s to list of targets
            {
                spitfireAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("DORNIER")) // add all current BF110s to list of targets
            {
                spitfireAI.AddToTargetsList(germanFighter);
            }

            spitfiresStored = spitfiresStored - 1;

            for (int i =0; i < wingSize; i++)
            {
                xl = xl - 5;
                zl = zl - 5;

                GameObject spitfireInstanceLeft;
                spitfireInstanceLeft = Instantiate(spitfire, transform.position + new Vector3(xl,0,zl), transform.rotation) as GameObject;

                SpitfireAI spitfireAILeft = spitfireInstanceLeft.GetComponent<SpitfireAI>();
                spitfireAILeft.SetDefaultTarget(targetAirfield);
                spitfireAILeft.SetOriginalAirbase(transform.root.gameObject);

                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF109")) // targets for Left wing (AILeft)
                {
                    spitfireAILeft.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF110"))
                {
                    spitfireAILeft.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("HEINKEL")) // add all current BF110s to list of targets
                {
                    spitfireAILeft.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("JUNKERS")) // add all current BF110s to list of targets
                {
                    spitfireAILeft.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("DORNIER")) // add all current BF110s to list of targets
                {
                    spitfireAILeft.AddToTargetsList(germanFighter);
                }

                spitfiresStored = spitfiresStored - 1;

            }

            for (int j = 0; j < wingSize; j++)
            {
                xr = xr + 5;
                zr = zr - 5;

                GameObject spitfireInstanceRight;
                spitfireInstanceRight = Instantiate(spitfire, transform.position + new Vector3(xr, 0, zr), transform.rotation) as GameObject;

                SpitfireAI spitfireAIRight = spitfireInstanceRight.GetComponent<SpitfireAI>();
                spitfireAIRight.SetDefaultTarget(targetAirfield);
                spitfireAIRight.SetOriginalAirbase(transform.root.gameObject);

                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF109")) // targets for right wing (AIRight)
                {
                    spitfireAIRight.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF110"))
                {
                    spitfireAIRight.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("HEINKEL")) // add all current BF110s to list of targets
                {
                    spitfireAIRight.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("JUNKERS")) // add all current BF110s to list of targets
                {
                    spitfireAIRight.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("DORNIER")) // add all current BF110s to list of targets
                {
                    spitfireAIRight.AddToTargetsList(germanFighter);
                }

                spitfiresStored = spitfiresStored - 1;

            }
        }
    }

    void SpawnHurricanes()
    {
        int xl = 0;
        int zl = 0;
        int xr = 0;
        int zr = 0;

        if (planesPerWave == 1)
        {
            GameObject hurricaneInstanceSingle;
            hurricaneInstanceSingle = Instantiate(hurricane, transform.position, transform.rotation) as GameObject;

            HurricaneAI hurricaneAI = hurricaneInstanceSingle.GetComponent<HurricaneAI>();
            hurricaneAI.SetDefaultTarget(targetAirfield);
            hurricaneAI.SetOriginalAirbase(transform.root.gameObject);

            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF109"))
            {
                hurricaneAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF110"))
            {
                hurricaneAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("HEINKEL"))
            {
                hurricaneAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("JUNKERS"))
            {
                hurricaneAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("DORNIER"))
            {
                hurricaneAI.AddToTargetsList(germanFighter);
            }

            hurricanesStored = hurricanesStored - 1;
        }

        if (planesPerWave > 1)
        {
            int wingSize = ((planesPerWave - 1) / 2);

            GameObject hurricaneInstanceMiddle;
            hurricaneInstanceMiddle = Instantiate(hurricane, transform.position, transform.rotation) as GameObject;

            HurricaneAI hurricaneAI = hurricaneInstanceMiddle.GetComponent<HurricaneAI>();
            hurricaneAI.SetDefaultTarget(targetAirfield);
            hurricaneAI.SetOriginalAirbase(transform.root.gameObject);

            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF109"))
            {
                hurricaneAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF110"))
            {
                hurricaneAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("HEINKEL"))
            {
                hurricaneAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("JUNKERS"))
            {
                hurricaneAI.AddToTargetsList(germanFighter);
            }
            foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("DORNIER"))
            {
                hurricaneAI.AddToTargetsList(germanFighter);
            }

            hurricanesStored = hurricanesStored - 1;

            for (int i = 0; i < wingSize; i++)
            {
                xl = xl - 5;
                zl = zl - 5;

                GameObject hurricaneInstanceLeft;
                hurricaneInstanceLeft = Instantiate(hurricane, transform.position + new Vector3(xl, 0, zl), transform.rotation) as GameObject;

                HurricaneAI hurricaneAILeft = hurricaneInstanceLeft.GetComponent<HurricaneAI>();
                hurricaneAILeft.SetDefaultTarget(targetAirfield);
                hurricaneAILeft.SetOriginalAirbase(transform.root.gameObject);

                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF109"))
                {
                    hurricaneAILeft.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF110"))
                {
                    hurricaneAILeft.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("HEINKEL"))
                {
                    hurricaneAILeft.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("JUNKERS"))
                {
                    hurricaneAILeft.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("DORNIER"))
                {
                    hurricaneAILeft.AddToTargetsList(germanFighter);
                }

                hurricanesStored = hurricanesStored - 1;

            }

            for (int j = 0; j < wingSize; j++)
            {
                xr = xr + 5;
                zr = zr - 5;

                GameObject hurricaneInstanceRight;
                hurricaneInstanceRight = Instantiate(hurricane, transform.position + new Vector3(xr, 0, zr), transform.rotation) as GameObject;

                HurricaneAI hurricaneAIRight = hurricaneInstanceRight.GetComponent<HurricaneAI>();
                hurricaneAIRight.SetDefaultTarget(targetAirfield);
                hurricaneAIRight.SetOriginalAirbase(transform.root.gameObject);

                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF109"))
                {
                    hurricaneAIRight.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("BF110"))
                {
                    hurricaneAIRight.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("HEINKEL"))
                {
                    hurricaneAIRight.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("JUNKERS"))
                {
                    hurricaneAIRight.AddToTargetsList(germanFighter);
                }
                foreach (GameObject germanFighter in GameObject.FindGameObjectsWithTag("DORNIER"))
                {
                    hurricaneAIRight.AddToTargetsList(germanFighter);
                }

                hurricanesStored = hurricanesStored - 1;

            }
        }
    }

    public void GetBombed()
    {
        int spitfiresDestroyed = 2;
        int hurricanesDestroyed = 2;

        spitfiresStored = spitfiresStored - 2;
        hurricanesStored = hurricanesStored - 2;

        int majorHit = Random.Range(1, 4); // chance of a major hit
        if(majorHit == 1)
        {
            spitfiresStored = spitfiresStored - 3;
            hurricanesStored = hurricanesStored - 3;

            spitfiresDestroyed = 5;
            hurricanesDestroyed = 5;

            int criticalHit = Random.Range(1, 4);  // chance of a critical hit
            if(criticalHit == 1)
            {
                spitfiresStored = spitfiresStored - 4;
                hurricanesStored = hurricanesStored - 4;
                Debug.Log("Critical hit");

                spitfiresDestroyed = 9;
                hurricanesDestroyed = 9;
            }
        }

        if(spitfiresStored < 0)
        {
            spitfiresDestroyed = spitfiresDestroyed + spitfiresStored;
            spitfiresStored = 0;
        }
        if (hurricanesStored < 0)
        {
            hurricanesDestroyed = hurricanesDestroyed + hurricanesStored;
            hurricanesStored = 0;
        }

        ScoreScreen score = GameObject.Find("ScoreScreen").GetComponent<ScoreScreen>();
        score.addBritishFighter(hurricanesDestroyed + spitfiresDestroyed);
    }

    public void SetWavesToSend(int waves)
    {
        wavesToSend = waves;
    }

    public void SetPlanesPerWave(int planes)
    {
        planesPerWave = planes;
    }

    public void SetTargetAirfield(GameObject airfield)
    {
        targetAirfield = airfield;
    }

    public string GetName()
    {
        return name;
    }

    public int GetSpitfiresStored()
    {
        return spitfiresStored;
    }

    public int GetHurricanesStored()
    {
        return hurricanesStored;
    }

    public bool GetCurrentlySending()
    {
        return currentlySending;
    }

    public void SetFighterType(int fighter)
    {
        fightertype = fighter;
    }

    public void AddSpitfire()
    {
        spitfiresStored = spitfiresStored + 1;
    }

    public void AddHurricane()
    {
        hurricanesStored = hurricanesStored + 1;
    }

    public void ReenforceSpitfires()
    {
        if((spitfiresIncoming == true)
            &&(stockPileTimer < 0))
        {
            spitfiresStored = spitfiresStored + reenforcingSpitfires;
            spitfiresIncoming = false;
        }
    }

    public void ReenforceHurricanes()
    {
        if ((hurricanesIncoming == true)
            && (stockPileTimer < 0))
        {
            hurricanesStored = hurricanesStored + reenforcingHurricanes;
            hurricanesIncoming = false;
        }
    }

    public void SetIncomingSpitfires()
    {
        spitfiresIncoming = true;
    }
    public bool GetIncomingSpitfires()
    {
        return spitfiresIncoming;
    }

    public void SetIncomingHurricanes()
    {
        hurricanesIncoming = true;
    }
    public bool GetIncomingHurricanes()
    {
        return hurricanesIncoming;
    }

    public void SetReenforcingHurricanes(int planes)
    {
        reenforcingHurricanes = planes;
    }

    public void SetReenforcingSpitfires(int planes)
    {
        reenforcingSpitfires = planes;
    }

    public void SetStockpileTimer(int time)
    {
        stockPileTimer = time;
    }

}
