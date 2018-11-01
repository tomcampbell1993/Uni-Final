using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class GermanAirfield : GermanBuilding
{
    public GameObject BF109;
    public GameObject heinkel;
    public GameObject dornier;
    public GameObject junkers;
    public GameObject BF110;

    public Dornier dornierAI;
    public MesserschmidtAI BF109AI;
    public Heinkel heinkelAI;
    public Junkers junkersAI;
    public BF110AI BF110AI;

    public GameObject targetAirfield;
    public GameObject originalAirfield;

    public int bomberWavesToSend;
    public int escortsPerWave;
    public int heinkelsStored;
    public int dorniersStored;
    public int junkersStored;
    public int bf109sStored;
    public int bf110sStored;

    public int reenforcingBF109s;
    public int reenforcingBF110s;
    public int reenforcingHeinkels;
    public int reenforcingJunkers;
    bool BF109sIncoming;
    bool BF110sIncoming;
    bool HeinkelsIncoming;
    bool JunkersIncoming;
    public float stockPileTimer;

    public int planeType;
    // 1 BF109
    // 2 BF110
    // 3 Heinkell HE111
    // 4 Dornier Do17
    // 5 Junkers Ju 88

    public int escortType;
    // 1 BF109
    // 2 BF110

    public float timer;

    public bool currentlySending;
    void Start()
    {
        currentlySending = false;
        planeType = 3;
        objectName = "German Airfield";
        menuObject = GameObject.Find("Menu");
        bomberWavesToSend = 0;
        heinkelsStored = 30;
        dorniersStored = 60;
        junkersStored = 40;
        bf109sStored = 30;
        bf110sStored = 30;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        stockPileTimer -= Time.deltaTime;
        Spawning();
        ReenforceBF109s();
        ReenforceBF110s();
        ReenforceJunkers();
        ReenforceHeinkels();
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
        if (bomberWavesToSend > 0)
        {
            currentlySending = true;

            if (timer < 0)
            {
                if(planeType == 3)
                {
                    SpawnHeinkels();
                    timer = 2.0f; // send bomber ever X seconds
                    bomberWavesToSend = bomberWavesToSend - 1;
                }

                if (planeType == 4)
                {
                    SpawnDorniers();
                    timer = 2.0f; // send bomber ever X seconds
                    bomberWavesToSend = bomberWavesToSend - 1;
                }

                if (planeType == 5)
                {
                    SpawnJunkers();
                    timer = 2.0f; // send bomber ever X seconds
                    bomberWavesToSend = bomberWavesToSend - 1;
                }

                if (planeType == 1)
                {
                    SpawnBF109s();
                    timer = 2.0f; // send bomber ever X seconds
                    bomberWavesToSend = bomberWavesToSend - 1;
                }

                if (planeType == 2)
                {
                    SpawnBF110s();
                    timer = 2.0f; // send bomber ever X seconds
                    bomberWavesToSend = bomberWavesToSend - 1;
                }
            }
        }

        if (bomberWavesToSend == 0)
        {
            currentlySending = false;
        }
    }

    public void SpawnHeinkels() // Creates bombers with targets set by code from British air fields map buttons.
    {
        GameObject heinkelInstanceOne; // first bomber
        heinkelInstanceOne = Instantiate(heinkel, transform.position, transform.root.rotation) as GameObject;

        heinkelAI = heinkelInstanceOne.GetComponent<Heinkel>();
        heinkelAI.SetTarget(targetAirfield);
        heinkelAI.SetEnemyAirbase(targetAirfield);
        heinkelAI.SetOriginalAirBase(transform.root.gameObject);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            heinkelAI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            heinkelAI.AddToTargetsList(hurricane);
        }


        GameObject heinkelInstanceTwo; // second bomber
        heinkelInstanceTwo = Instantiate(heinkel, transform.position + new Vector3(10, 0, -10), transform.root.rotation) as GameObject;

        heinkelAI = heinkelInstanceTwo.GetComponent<Heinkel>();
        heinkelAI.SetTarget(targetAirfield);
        heinkelAI.SetEnemyAirbase(targetAirfield);
        heinkelAI.SetOriginalAirBase(transform.root.gameObject);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            heinkelAI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            heinkelAI.AddToTargetsList(hurricane);
        }


        GameObject heinkelInstanceThree; // thrid bomber
        heinkelInstanceThree = Instantiate(heinkel, transform.position + new Vector3(-10, 0, -10), transform.root.rotation) as GameObject;

        heinkelAI = heinkelInstanceThree.GetComponent<Heinkel>();
        heinkelAI.SetTarget(targetAirfield);
        heinkelAI.SetEnemyAirbase(targetAirfield);
        heinkelAI.SetOriginalAirBase(transform.root.gameObject);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            heinkelAI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            heinkelAI.AddToTargetsList(hurricane);
        }

        heinkelsStored = heinkelsStored - 3;

        if(escortType == 1)
        {
            GameObject BF109Escort;
            int zPos = 0;
            for (int i = 0; i < escortsPerWave; i++)
            {
                zPos = zPos + 3;
                BF109Escort = Instantiate(BF109, transform.position + new Vector3(0, 0, zPos), transform.root.rotation) as GameObject;
                BF109AI = BF109Escort.GetComponent<MesserschmidtAI>();
                BF109AI.SetAssignedEscort(heinkelInstanceOne);
                BF109AI.setOriginalAirbase(transform.root.gameObject);
                BF109AI.setDefaultTarget(heinkelInstanceOne);
                BF109AI.setCurrentTarget(heinkelInstanceOne);

                foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
                {
                    BF109AI.AddToTargetsList(spitfire);
                }
                foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
                {
                    BF109AI.AddToTargetsList(hurricane);
                }

                bf109sStored = bf109sStored - 1;
            }
        }

        if (escortType == 2)
        {
            GameObject BF110Escort;
            int zPos = 0;
            for (int i = 0; i < escortsPerWave; i++)
            {
                zPos = zPos + 3;
                BF110Escort = Instantiate(BF110, transform.position + new Vector3(0, 0, zPos), transform.root.rotation) as GameObject;
                BF110AI = BF110Escort.GetComponent<BF110AI>();
                BF110AI.SetAssignedEscort(heinkelInstanceOne);
                BF110AI.setOriginalAirbase(transform.root.gameObject);
                BF110AI.setDefaultTarget(heinkelInstanceOne);
                BF110AI.setCurrentTarget(heinkelInstanceOne);

                foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
                {
                    BF110AI.AddToTargetsList(spitfire);
                }
                foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
                {
                    BF110AI.AddToTargetsList(hurricane);
                }

                bf110sStored = bf110sStored - 1;
            }
        }

    }

    public void SpawnDorniers()
    {
        GameObject dornierInstanceOne; // first bomber
        dornierInstanceOne = Instantiate(dornier, transform.position, transform.root.rotation) as GameObject;

        dornierAI = dornierInstanceOne.GetComponent<Dornier>();
        dornierAI.SetTarget(targetAirfield);
        dornierAI.SetEnemyAirbase(targetAirfield);
        dornierAI.SetOriginalAirBase(transform.root.gameObject);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            dornierAI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            dornierAI.AddToTargetsList(hurricane);
        }


        GameObject dornierInstanceTwo; // second bomber
        dornierInstanceTwo = Instantiate(dornier, transform.position + new Vector3(10, 0, -10), transform.root.rotation) as GameObject;

        dornierAI = dornierInstanceTwo.GetComponent<Dornier>();
        dornierAI.SetTarget(targetAirfield);
        dornierAI.SetEnemyAirbase(targetAirfield);
        dornierAI.SetOriginalAirBase(transform.root.gameObject);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            dornierAI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            dornierAI.AddToTargetsList(hurricane);
        }


        GameObject dornierInstanceThree; // thrid bomber
        dornierInstanceThree = Instantiate(dornier, transform.position + new Vector3(-10, 0, -10), transform.root.rotation) as GameObject;

        dornierAI = dornierInstanceThree.GetComponent<Dornier>();
        dornierAI.SetTarget(targetAirfield);
        dornierAI.SetEnemyAirbase(targetAirfield);
        dornierAI.SetOriginalAirBase(transform.root.gameObject);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            dornierAI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            dornierAI.AddToTargetsList(hurricane);
        }

        dorniersStored = dorniersStored - 3;

        if (escortType == 1)
        {
            GameObject BF109Escort;
            int zPos = 0;
            for (int i = 0; i < escortsPerWave; i++)
            {
                zPos = zPos + 3;
                BF109Escort = Instantiate(BF109, transform.position + new Vector3(0, 0, zPos), transform.root.rotation) as GameObject;
                BF109AI = BF109Escort.GetComponent<MesserschmidtAI>();
                BF109AI.SetAssignedEscort(dornierInstanceOne);
                BF109AI.setOriginalAirbase(transform.root.gameObject);
                BF109AI.setDefaultTarget(dornierInstanceOne);
                BF109AI.setCurrentTarget(dornierInstanceOne);
                bf109sStored = bf109sStored - 1;

                foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
                {
                    BF109AI.AddToTargetsList(spitfire);
                }
                foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
                {
                    BF109AI.AddToTargetsList(hurricane);
                }
            }
        }

        if (escortType == 2)
        {
            GameObject BF110Escort;
            int zPos = 0;
            for (int i = 0; i < escortsPerWave; i++)
            {
                zPos = zPos + 3;
                BF110Escort = Instantiate(BF110, transform.position + new Vector3(0, 0, zPos), transform.root.rotation) as GameObject;
                BF110AI = BF110Escort.GetComponent<BF110AI>();
                BF110AI.SetAssignedEscort(dornierInstanceOne);
                BF110AI.setOriginalAirbase(transform.root.gameObject);
                BF110AI.setDefaultTarget(dornierInstanceOne);
                BF110AI.setCurrentTarget(dornierInstanceOne);
                bf110sStored = bf110sStored - 1;

                foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
                {
                    BF110AI.AddToTargetsList(spitfire);
                }
                foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
                {
                    BF110AI.AddToTargetsList(hurricane);
                }
            }
        }
    }

    public void SpawnJunkers()
    {
        GameObject junkersInstanceOne; // first bomber
        junkersInstanceOne = Instantiate(junkers, transform.position, transform.root.rotation) as GameObject;

        junkersAI = junkersInstanceOne.GetComponent<Junkers>();
        junkersAI.SetTarget(targetAirfield);
        junkersAI.SetEnemyAirbase(targetAirfield);
        junkersAI.SetOriginalAirBase(transform.root.gameObject);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            junkersAI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            junkersAI.AddToTargetsList(hurricane);
        }


        GameObject junkersInstanceTwo; // second bomber
        junkersInstanceTwo = Instantiate(junkers, transform.position + new Vector3(10, 0, -10), transform.root.rotation) as GameObject;

        junkersAI = junkersInstanceTwo.GetComponent<Junkers>();
        junkersAI.SetTarget(targetAirfield);
        junkersAI.SetEnemyAirbase(targetAirfield);
        junkersAI.SetOriginalAirBase(transform.root.gameObject);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            junkersAI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            junkersAI.AddToTargetsList(hurricane);
        }


        GameObject junkersInstanceThree; // thrid bomber
        junkersInstanceThree = Instantiate(junkers, transform.position + new Vector3(-10, 0, -10), transform.root.rotation) as GameObject;

        junkersAI = junkersInstanceThree.GetComponent<Junkers>();
        junkersAI.SetTarget(targetAirfield);
        junkersAI.SetEnemyAirbase(targetAirfield);
        junkersAI.SetOriginalAirBase(transform.root.gameObject);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            junkersAI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            junkersAI.AddToTargetsList(hurricane);
        }

        junkersStored = junkersStored - 3;

        if (escortType == 1)
        {
            GameObject BF109Escort;
            int zPos = 0;
            for (int i = 0; i < escortsPerWave; i++)
            {
                zPos = zPos + 3;
                BF109Escort = Instantiate(BF109, transform.position + new Vector3(0, 0, zPos), transform.root.rotation) as GameObject;
                BF109AI = BF109Escort.GetComponent<MesserschmidtAI>();
                BF109AI.SetAssignedEscort(junkersInstanceOne);
                BF109AI.setOriginalAirbase(transform.root.gameObject);
                BF109AI.setDefaultTarget(junkersInstanceOne);
                BF109AI.setCurrentTarget(junkersInstanceOne);
                bf109sStored = bf109sStored - 1;

                foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
                {
                    BF109AI.AddToTargetsList(spitfire);
                }
                foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
                {
                    BF109AI.AddToTargetsList(hurricane);
                }
            }
        }

        if (escortType == 2)
        {
            GameObject BF110Escort;
            int zPos = 0;
            for (int i = 0; i < escortsPerWave; i++)
            {
                zPos = zPos + 3;
                BF110Escort = Instantiate(BF110, transform.position + new Vector3(0, 0, zPos), transform.root.rotation) as GameObject;
                BF110AI = BF110Escort.GetComponent<BF110AI>();
                BF110AI.SetAssignedEscort(junkersInstanceOne);
                BF110AI.setOriginalAirbase(transform.root.gameObject);
                BF110AI.setDefaultTarget(junkersInstanceOne);
                BF110AI.setCurrentTarget(junkersInstanceOne);
                bf110sStored = bf110sStored - 1;

                foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
                {
                    BF110AI.AddToTargetsList(spitfire);
                }
                foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
                {
                    BF110AI.AddToTargetsList(hurricane);
                }
            }
        }
    }

    public void SpawnBF109s()
    {
        GameObject BF109InstanceOne;
        BF109InstanceOne = Instantiate(BF109, transform.position, transform.root.rotation) as GameObject;

        BF109AI = BF109InstanceOne.GetComponent<MesserschmidtAI>();
        BF109AI.setDefaultTarget(targetAirfield);
        BF109AI.setOriginalAirbase(transform.root.gameObject);
        BF109AI.setCurrentTarget(targetAirfield);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            BF109AI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            BF109AI.AddToTargetsList(hurricane);
        }


        GameObject BF109InstanceTwo;
        BF109InstanceTwo = Instantiate(BF109, transform.position + new Vector3(10, 0, -10), transform.root.rotation) as GameObject;

        BF109AI = BF109InstanceTwo.GetComponent<MesserschmidtAI>();
        BF109AI.setDefaultTarget(targetAirfield);
        BF109AI.setOriginalAirbase(transform.root.gameObject);
        BF109AI.setCurrentTarget(targetAirfield);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            BF109AI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            BF109AI.AddToTargetsList(hurricane);
        }


        GameObject BF109InstanceThree;
        BF109InstanceThree = Instantiate(BF109, transform.position + new Vector3(-10, 0, -10), transform.root.rotation) as GameObject;

        BF109AI = BF109InstanceThree.GetComponent<MesserschmidtAI>();
        BF109AI.setDefaultTarget(targetAirfield);
        BF109AI.setOriginalAirbase(transform.root.gameObject);
        BF109AI.setCurrentTarget(targetAirfield);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            BF109AI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            BF109AI.AddToTargetsList(hurricane);
        }

        bf109sStored = bf109sStored - 3;
    }

    public void SpawnBF110s()
    {
        GameObject BF110InstanceOne;
        BF110InstanceOne = Instantiate(BF110, transform.position, transform.root.rotation) as GameObject;

        BF110AI = BF110InstanceOne.GetComponent<BF110AI>();
        BF110AI.setDefaultTarget(targetAirfield);
        BF110AI.setOriginalAirbase(transform.root.gameObject);
        BF110AI.setCurrentTarget(targetAirfield);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            BF110AI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            BF110AI.AddToTargetsList(hurricane);
        }


        GameObject BF110InstanceTwo;
        BF110InstanceTwo = Instantiate(BF110, transform.position + new Vector3(10, 0, -10), transform.root.rotation) as GameObject;

        BF110AI = BF110InstanceTwo.GetComponent<BF110AI>();
        BF110AI.setDefaultTarget(targetAirfield);
        BF110AI.setOriginalAirbase(transform.root.gameObject);
        BF110AI.setCurrentTarget(targetAirfield);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            BF110AI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            BF110AI.AddToTargetsList(hurricane);
        }


        GameObject BF110InstanceThree;
        BF110InstanceThree = Instantiate(BF110, transform.position + new Vector3(-10, 0, -10), transform.root.rotation) as GameObject;

        BF110AI = BF110InstanceThree.GetComponent<BF110AI>();
        BF110AI.setDefaultTarget(targetAirfield);
        BF110AI.setOriginalAirbase(transform.root.gameObject);
        BF110AI.setCurrentTarget(targetAirfield);

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE"))
        {
            BF110AI.AddToTargetsList(spitfire);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            BF110AI.AddToTargetsList(hurricane);
        }

        bf110sStored = bf110sStored - 3;
    }

    public void SetWavesToSend(int waves)
    {
        bomberWavesToSend = waves;
    }

    public void SetEscortsPerWave(int escorts)
    {
        escortsPerWave = escorts;
    }

    public void SetTargetAirfield(GameObject target)
    {
        targetAirfield = target;
    }

    public int GetHeinkelsStored()
    {
        return heinkelsStored;
    }

    public int GetDorniersStored()
    {
        return dorniersStored;
    }

    public int GetJunkersStored()
    {
        return junkersStored;
    }

    public int GetBF109sStored()
    {
        return bf109sStored;
    }

    public int GetBF110sStored()
    {
        return bf110sStored;
    }

    public void SetPlaneType(int plane)
    {
        planeType = plane;
    }

    public void SetEscortType(int escort)
    {
        escortType = escort;
    }

    public int GetEscortType()
    {
        return escortType;
    }

    public bool GetCurrentlySending()
    {
        return currentlySending;
    }

    public void AddBF109()
    {
        bf109sStored = bf109sStored + 1;
    }
    public void AddBf110()
    {
        bf110sStored = bf110sStored + 1;
    }
    public void AddHeinkel()
    {
        heinkelsStored = heinkelsStored + 1;
    }
    public void AddDornier()
    {
        dorniersStored = dorniersStored + 1;
    }
    public void AddJunkers()
    {
        junkersStored = junkersStored + 1;
    }

    public void ReenforceBF109s()
    {
        if ((BF109sIncoming == true)
            && (stockPileTimer < 0))
        {
            bf109sStored = bf109sStored + reenforcingBF109s;
            BF109sIncoming = false;
        }
    }
    public void ReenforceBF110s()
    {
        if ((BF110sIncoming == true)
            && (stockPileTimer < 0))
        {
            bf110sStored = bf110sStored + reenforcingBF110s;
            BF110sIncoming = false;
        }
    }
    public void ReenforceHeinkels()
    {
        if ((HeinkelsIncoming == true)
            && (stockPileTimer < 0))
        {
            heinkelsStored = heinkelsStored + reenforcingHeinkels;
            HeinkelsIncoming = false;
        }
    }
    public void ReenforceJunkers()
    {
        if ((JunkersIncoming == true)
            && (stockPileTimer < 0))
        {
            junkersStored = junkersStored + reenforcingJunkers;
            JunkersIncoming = false;
        }
    }

    public void SetIncomingBF109s()
    {
        BF109sIncoming = true;
    }
    public void SetIncomingBF110s()
    {
        BF110sIncoming = true;
    }
    public void SetIncomingHeinkels()
    {
        HeinkelsIncoming = true;
    }
    public void SetIncomingJunkers()
    {
        JunkersIncoming = true;
    }

    public bool GetIncomingBF109s()
    {
        return BF109sIncoming;
    }
    public bool GetIncomingBF110s()
    {
        return BF110sIncoming;
    }
    public bool GetIncomingHeinkels()
    {
        return HeinkelsIncoming;
    }
    public bool GetIncomingJunkers()
    {
        return JunkersIncoming;
    }

    public void SetReenforcingBF109s(int planes)
    {
        reenforcingBF109s = planes;
    }
    public void SetReenforcingBF110s(int planes)
    {
        reenforcingBF110s = planes;
    }
    public void SetReenforcingHeinkels(int planes)
    {
        reenforcingHeinkels = planes;
    }
    public void SetReenforcingJunkers(int planes)
    {
        reenforcingJunkers = planes;
    }

    public void SetStockpileTimer(int time)
    {
        stockPileTimer = time;
    }
}
