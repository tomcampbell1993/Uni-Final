using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dornier : GermanBomber
{
    public List<GameObject> britishFighterList = new List<GameObject>(); // list of potential enemies
    public List<GameObject> chaserList = new List<GameObject>(); // list of enemies currently chasing this plane
    public Menu menuScript;

    public GameObject currentTarget;
    public GameObject fireTarget;
    public GameObject originalAirbase;
    public GameObject gameTimerObject;
    public GameObject convoyTarget;
    public GameObject enemyAirbase;
    public GameObject britishConvoy;
    public GameObject menuObject;

    float fireRange;
    float distance;
    float timer;

    public bool isFacing;
    public bool isFiring;
    public float Angle;
    public float SignedAngle;
    public bool missionComplete;

    private Rigidbody rb;
    public GameTimer gameTimerScript;

    public int accuracy;
    public int maneuverability;
    public int integrity;
    public int rain;
    public int fog;
    public bool nightTime;
    public int pilot;
    // 0 pilot healthy
    // 1 pilot wounded
    // 2 pilot dead
    public int engine;
    // 0 engine fine
    // 1 engine damaged
    // 2 engine destroyed
    public int firepower;

    public bool healthyPilot;
    public bool healthyEngine;

    // Use this for initialization
    void Start()
    {

        fireTarget = transform.root.gameObject;

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE")) // Lists itself as a potential enemy target.
        {
            SpitfireAI spitFireAIStart = spitfire.GetComponent<SpitfireAI>();
            spitFireAIStart.AddToTargetsList(transform.root.gameObject);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            HurricaneAI hurricaneStart = hurricane.GetComponent<HurricaneAI>();
            hurricaneStart.AddToTargetsList(transform.root.gameObject);
        }
        convoyTarget = GameObject.FindGameObjectWithTag("BRITISHCONVOY");

        isFacing = false;
        missionComplete = false;
        rb = GetComponent<Rigidbody>();
        bomberName = "Dornier Do17";

        gameTimerObject = GameObject.Find("GameTimer");
        gameTimerScript = gameTimerObject.GetComponent<GameTimer>();

        menuObject = GameObject.Find("Menu");
        menuScript = menuObject.GetComponent<Menu>();
        gameObject.name = "Dornier DO17";

        forwardSpeed = 6;
        turnSpeed = 80;
        integrity = 120;
        accuracy = 2;
        maneuverability = 3;

        pilot = 0;
        engine = 0;
        firepower = 1;

        healthyPilot = true;
        healthyEngine = true;

        convoyTarget = GameObject.FindGameObjectWithTag("BRITISHCONVOY");

    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;
        rb.velocity = transform.forward * forwardSpeed;

        FindFireTarget();
        FireAtSpitfire();
        FireAtHurricane();

        SetMovement();
        CheckTarget();
        AssessDamage();

        convoyTarget = GameObject.FindGameObjectWithTag("BRITISHCONVOY");
    }

    private void OnMouseDown()
    {
        if (!IsPointerOverUIObject())
        {
            menuScript.SetSelectedObject(transform.root.gameObject);
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

    void SetMovement()
    {

        if (currentTarget == null)
        {
            return;
        }

        Vector3 TargetDir = currentTarget.transform.position - transform.position;
        Angle = Vector3.Angle(TargetDir, transform.forward);

        SignedAngle = Vector3.SignedAngle(TargetDir, transform.forward, Vector3.down);

        if (Angle < 6.0f)
        {
            isFacing = true;
        }

        if (Angle > 6.0f)
        {
            isFacing = false;
        }

        // Turn Anticlockwise
        if ((SignedAngle > 0.0f) && (Angle > 10.0f))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * turnSpeed, Space.World);
        }

        // Turn Clockwise
        if ((SignedAngle < 0.0f) && (Angle > 10.0f))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * turnSpeed, Space.World);
        }

        if ((transform.position.y > 80) || (transform.position.y < 80))                             //Maintains aircraft height at 80
        {
            transform.position = new Vector3(transform.position.x, 80, transform.position.z);
        }
    }

    public void FindFireTarget()
    {

        if (britishFighterList.Count > 0)
        {
            for (int i = 0; i < britishFighterList.Count; i++)
            {
                if (britishFighterList[i])
                {
                    GameObject enemyPlane = britishFighterList[i];
                    if (enemyPlane.tag == ("SPITFIRE"))
                    {
                        SpitfireAI spitfirePick = enemyPlane.GetComponent<SpitfireAI>();

                        float distance = Vector3.Distance(transform.position, enemyPlane.transform.position);

                        if ((distance < 40.0f) && (fireTarget.tag != ("SPITFIRE")) && (fireTarget.tag != ("HURRICANE")))
                        {
                            int spreadTarget = Random.Range(1, 3); // this spreads out targets so that target is not always first plane in list.
                            if (spreadTarget == 1)
                            {
                                fireTarget = enemyPlane;
                                spitfirePick.AddToChaserList(transform.root.gameObject);
                            }
                        }
                    }

                    if (enemyPlane.tag == ("HURRICANE"))
                    {
                        HurricaneAI hurricanePick = enemyPlane.GetComponent<HurricaneAI>();

                        float distance = Vector3.Distance(transform.position, enemyPlane.transform.position);

                        if ((distance < 40.0f) && (fireTarget.tag != ("SPITFIRE")) && (fireTarget.tag != ("HURRICANE")))
                        {
                            int spreadTarget = Random.Range(1, 3); // this spreads out targets so that target is not always first plane in list.
                            if (spreadTarget == 1)
                            {
                                fireTarget = enemyPlane;
                                hurricanePick.AddToChaserList(transform.root.gameObject);
                            }
                        }
                    }
                }
                
            }
        }

        distance = 100.0f;
    }

    void FireAtSpitfire()
    {
        if (fireTarget == null)
        {
            return;
        }

        fireRange = 100.0f;

        if (fireTarget.tag == "SPITFIRE")
        {

            fireRange = Vector3.Distance(transform.position, fireTarget.transform.position);
        }

        if ((fireTarget.tag == "SPITFIRE")
           && (fireRange < 20.0f)
           )
        {
            isFiring = true;
            if (fireRange > 20.0f)
            {
                isFiring = false;
                SpitfireAI spitfireTarget = fireTarget.GetComponent<SpitfireAI>();
                spitfireTarget.RemoveFromChaserList(transform.root.gameObject);
            }
        }
        else
        {
            isFiring = false;
        }

        if (isFiring == true)
        {
            if (timer <= 1.0f)
            {
                timer = 3.0f;

                SpitfireAI spitfireTarget = fireTarget.GetComponent<SpitfireAI>();
                int enemyManeuverability = spitfireTarget.GetManeuverability();

                nightTime = gameTimerScript.GetNightTime();
                rain = gameTimerScript.GetRain();
                fog = gameTimerScript.GetFog();
                int hitCalc = 0;

                if (nightTime == false)
                {
                    hitCalc = (rain + fog + enemyManeuverability + 5) - accuracy;
                }

                if (nightTime == true)
                {
                    hitCalc = ((rain + fog) * 2) - accuracy;
                }


                int hitChance = Random.Range(1, hitCalc);

                if (hitChance == 1)
                {
                    spitfireTarget.GetHitByDornier();
                    spitfireTarget.RemoveFromChaserList(transform.root.gameObject);
                    fireTarget = transform.root.gameObject;
                }
            }
        }
    }

    void FireAtHurricane()
    {
        if (fireTarget == null)
        {
            return;
        }

        fireRange = 100.0f;

        if (fireTarget.tag == "HURRICANE")
        {

            fireRange = Vector3.Distance(transform.position, fireTarget.transform.position);
        }

        if ((fireTarget.tag == "HURRICANE")
           && (fireRange < 20.0f)
           )
        {
            isFiring = true;
            if (fireRange > 20.0f)
            {
                isFiring = false;
                HurricaneAI hurricaneTarget = fireTarget.GetComponent<HurricaneAI>();
                hurricaneTarget.RemoveFromChaserList(transform.root.gameObject);
            }
        }
        else
        {
            isFiring = false;
        }

        if (isFiring == true)
        {
            if (timer <= 1.0f)
            {
                timer = 3.0f;

                HurricaneAI hurricaneTarget = fireTarget.GetComponent<HurricaneAI>();
                int enemyManeuverability = hurricaneTarget.GetManeuverability();

                nightTime = gameTimerScript.GetNightTime();
                rain = gameTimerScript.GetRain();
                fog = gameTimerScript.GetFog();
                int hitCalc = 0;

                if (nightTime == false)
                {
                    hitCalc = (rain + fog + enemyManeuverability + 5) - accuracy;
                }

                if (nightTime == true)
                {
                    hitCalc = ((rain + fog) * 2) - accuracy;
                }


                int hitChance = Random.Range(1, hitCalc);

                if (hitChance == 1)
                {
                    hurricaneTarget.GetHitByDornier();
                    hurricaneTarget.RemoveFromChaserList(transform.root.gameObject);
                    fireTarget = transform.root.gameObject;
                }
            }
        }
    }

    public void GetHitBySpitfire()
    {
        integrity = integrity - 2;

        int pilotHitChance = Random.Range(1, 16); // chance to hit pilot
        if (pilotHitChance == 1)
        {
            pilot = 1;
            accuracy = accuracy - 1;
            maneuverability = maneuverability - 1;
            int pilotKillChance = Random.Range(1, 6);
            if (pilotKillChance == 1)
            {
                pilot = 2;
                GetDestroyed();
            }
        }

        int engineHitChance = Random.Range(1, 4); // chance to hit engine
        if (engineHitChance == 1)
        {
            engine = 1;
            maneuverability = maneuverability - 1;
            int engineKillChance = Random.Range(1, 4);
            if (engineKillChance == 1)
            {
                engine = 2;
                GetDestroyed();
            }
        }

        if (integrity < 0)
        {
            GetDestroyed();
        }
    }

    public void GetHitByHurricane()
    {
        integrity = integrity - 5;

        int pilotHitChance = Random.Range(1, 16); // chance to hit pilot
        if (pilotHitChance == 1)
        {
            pilot = 1;
            accuracy = accuracy - 1;
            maneuverability = maneuverability - 1;
            int pilotKillChance = Random.Range(1, 6);
            if (pilotKillChance == 1)
            {
                pilot = 2;
                GetDestroyed();
            }
        }

        int engineHitChance = Random.Range(1, 4); // chance to hit engine
        if (engineHitChance == 1)
        {
            engine = 1;
            maneuverability = maneuverability - 1;
            int engineKillChance = Random.Range(1, 3);
            if (engineKillChance == 1)
            {
                engine = 2;
                GetDestroyed();
            }
        }

        if (integrity < 0)
        {
            GetDestroyed();
        }
    }

    public void GetDestroyed()
    {
        if (missionComplete == false)
        {
            Debug.Log("Dornier shot down");
        }
        if (missionComplete == true)
        {
            Debug.Log("Dornier Landed");
        }

        transform.position = new Vector3(9999.0f, 9999.0f, 9999.0f); // Teleports away to execute code first

        if (fireTarget.tag == "SPITFIRE") // Removes itself from the enemy list of planes targetting this planes current target
        {
            SpitfireAI targetPlane = fireTarget.GetComponent<SpitfireAI>();
            targetPlane.RemoveFromChaserList(transform.root.gameObject);
        }

        if (fireTarget.tag == "HURRICANE") // Removes itself from the enemy list of planes targetting this planes current target
        {
            HurricaneAI targetPlane = fireTarget.GetComponent<HurricaneAI>();
            targetPlane.RemoveFromChaserList(transform.root.gameObject);
        }

        foreach (GameObject chaserPlane in chaserList) // any planes targetting this plane will have their targets reset
        {
            if (chaserPlane.tag == "SPITFIRE")
            {
                SpitfireAI spitfireAI = chaserPlane.GetComponent<SpitfireAI>();
                spitfireAI.ResetTarget(chaserPlane);
            }
            if (chaserPlane.tag == "HURRICANE")
            {
                HurricaneAI hurricaneAI = chaserPlane.GetComponent<HurricaneAI>();
                hurricaneAI.ResetTarget(chaserPlane);
            }
        }

        foreach (GameObject enemyPlane in britishFighterList) // removes itself from the enemy list of potential targets
        {

            if (enemyPlane.tag == "SPITFIRE")
            {
                SpitfireAI spitfireAI = enemyPlane.GetComponent<SpitfireAI>();
                spitfireAI.RemoveTarget(transform.root.gameObject);
            }
            if (enemyPlane.tag == "HURRICANE")
            {
                HurricaneAI hurricaneAI = enemyPlane.GetComponent<HurricaneAI>();
                hurricaneAI.RemoveTarget(transform.root.gameObject);
            }
        }

        if (menuScript.GetSelectedObject() == gameObject)
        {
            menuScript.SetSelectedObject(GameObject.Find("Land"));
        }
        ScoreScreen score = GameObject.Find("ScoreScreen").GetComponent<ScoreScreen>();
        score.addBomber(1);

        Destroy(transform.root.gameObject);
    }

    public void AssessDamage()
    {
        if (healthyPilot == true) // If the pilot gets hit!
        {
            if (pilot == 1)
            {
                if (accuracy > 1)
                {
                    accuracy = accuracy - 1;
                }

                if (maneuverability > 1)
                {
                    maneuverability = maneuverability - 1;
                }

                turnSpeed = turnSpeed - 10;
                healthyPilot = false;
            }
        }

        if (healthyEngine == true) // if the engine gets hit!
        {
            if (engine == 1)
            {
                if (maneuverability > 1)
                {
                    maneuverability = maneuverability - 1;
                }
                turnSpeed = turnSpeed - 20;
                forwardSpeed = forwardSpeed - 2;
                healthyEngine = false;
            }
        }
    }

    public void SetTarget(GameObject target)
    {
        currentTarget = target;
    }

    public void SetOriginalAirBase(GameObject airbase) // Selected Object found in MenuCanvas Script.
    {

        originalAirbase = airbase;
    }

    void CheckTarget()
    {
        distance = Vector3.Distance(transform.position, currentTarget.transform.position);
        float homeDistance = Vector3.Distance(transform.position, originalAirbase.transform.position);

        if (missionComplete == true)
        {
            currentTarget = originalAirbase;
        }

        if (distance < 30.0f)
        {
            if (currentTarget == convoyTarget)
            {
                Debug.Log("distance");
                BritishConvoy convoy = currentTarget.GetComponent<BritishConvoy>();

                int bombCalc = 0;
                nightTime = gameTimerScript.GetNightTime();
                rain = gameTimerScript.GetRain();
                fog = gameTimerScript.GetFog();

                if (nightTime == false)
                {
                    bombCalc = 1 + rain + fog;
                }
                if (nightTime == true)
                {
                    bombCalc = (1 + rain + fog) * 2;
                }

                int bombChance = Random.Range(1, bombCalc);
                if (bombChance == 1)
                {
                    convoy.GetBombed();
                    GameObject convoyInstance;
                    convoyInstance = Instantiate(britishConvoy, new Vector3(3.0f, 60.0f, 132.0f), transform.rotation) as GameObject;
                }
                
            }
            if (currentTarget.tag == "BRITISHAIRFIELD")
            {
                BritishAirfield airfield = currentTarget.GetComponent<BritishAirfield>();

                int bombCalc = 0;
                nightTime = gameTimerScript.GetNightTime();
                rain = gameTimerScript.GetRain();
                fog = gameTimerScript.GetFog();

                if (nightTime == false)
                {
                    bombCalc = 1 + rain + fog;
                }
                if (nightTime == true)
                {
                    bombCalc = (1 + rain + fog) * 2;
                }

                int bombChance = Random.Range(1, bombCalc);
                if (bombChance == 1)
                {
                    airfield.GetBombed();
                }
            }

            currentTarget = originalAirbase;
            missionComplete = true;
        }

        if (convoyTarget)
        {
            if (convoyTarget.tag == "BRITISHCONVOY")
            {
                float convoyDistance = Vector3.Distance(transform.position, convoyTarget.transform.position);
                if ((convoyDistance < 100) && (missionComplete == false))
                {
                    currentTarget = convoyTarget;
                }
            }
        }


        if ((homeDistance < 10.0f) // sends plane home when it reaches enemy airbase
            && (missionComplete == true))
        {
            Land();
        }



    }

    void Land()
    {
        GermanAirfield airbaseScript = originalAirbase.GetComponent<GermanAirfield>();
        airbaseScript.AddDornier(); // readd Dornier
        ScoreScreen score = GameObject.Find("ScoreScreen").GetComponent<ScoreScreen>();
        score.addBomber(-1);
        GetDestroyed();


    }

    public void AddToTargetsList(GameObject enemy)
    {
        britishFighterList.Add(enemy);
    }

    public void AddToChaserList(GameObject enemy)
    {
        chaserList.Add(enemy);
    }

    public void RemoveFromChaserList(GameObject enemy)
    {
        chaserList.Remove(enemy);
    }

    public void ResetTarget(GameObject self)
    {
        fireTarget = transform.root.gameObject;
    }

    public void RemoveTarget(GameObject target)
    {
        britishFighterList.Remove(target);
    }

    public int GetManeuverability()
    {
        return maneuverability;
    }

    public void SetConvoyTarget(GameObject target)
    {
        convoyTarget = target;
    }

    public void RemoveConvoyTarget()
    {
        convoyTarget = null;
    }

    public void SetEnemyAirbase(GameObject airbase)
    {
        enemyAirbase = airbase;
    }

    public void ResetToEnemyAirbase()
    {
        currentTarget = enemyAirbase;
    }

    // Stat Screen Info Getters

    public int GetPilot()
    {
        return pilot;
    }
    public int GetEngine()
    {
        return engine;
    }
    public int GetIntegrity()
    {
        return integrity;
    }
    public bool GetMissionComplete()
    {
        return missionComplete;
    }
}
