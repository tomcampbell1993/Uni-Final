using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MesserschmidtAI : MonoBehaviour
{
    public List<GameObject> britishFighterList = new List<GameObject>(); // list of potential enemies
    public List<GameObject> chaserList = new List<GameObject>(); // list of enemies currently chasing this plane
    public SpitfireAI spitFireAIStart;
    public GameTimer gameTimerScript;
    public Menu menuScript;

    public bool isFiring;
    public float timer;

    public GameObject assignedEscort;
    public GameObject CurrentTarget;
    public GameObject defaultTarget;
    public GameObject originalAirbase;
    public GameObject testProjectile;
    public GameObject gameTimerObject;
    public GameObject menuObject;
    public float ForwardSpeed;
    public float TurnSpeed;
    public float distance = 100.0f;
    public float fireDistance = 100.0f;
    public float escortRange;
    public int missionDesignation; // Mission designation, chosen by number, 1 = escort. 2 = air superiority.

    private float Angle;
    private float SignedAngle;
    private bool isFacing;
    bool safeLand;

    private Rigidbody rb;

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
    public int ammunition;

    public bool healthyPilot;
    public bool healthyEngine;
    public bool hasFuel;
    public float fuelCount;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameTimerObject = GameObject.Find("GameTimer");
        gameTimerScript = gameTimerObject.GetComponent<GameTimer>();
        menuObject = GameObject.Find("Menu");
        menuScript = menuObject.GetComponent<Menu>();
        gameObject.name = "BF109";

        isFacing = false;
        isFiring = false;

        foreach (GameObject spitfire in GameObject.FindGameObjectsWithTag("SPITFIRE")) // Lists itself as a potential enemy target.
        {
            spitFireAIStart = spitfire.GetComponent<SpitfireAI>();
            spitFireAIStart.AddToTargetsList(transform.root.gameObject);
        }
        foreach (GameObject hurricane in GameObject.FindGameObjectsWithTag("HURRICANE"))
        {
            HurricaneAI hurricaneStart = hurricane.GetComponent<HurricaneAI>();
            hurricaneStart.AddToTargetsList(transform.root.gameObject);
        }

        ForwardSpeed = 10;
        TurnSpeed = 50;
        integrity = 100;
        accuracy = 2;
        maneuverability = 4;
        fuelCount = 80;
        ammunition = 16;

        pilot = 0;
        engine = 0;
        firepower = 2;

        healthyPilot = true;
        healthyEngine = true;
        hasFuel = true;
        safeLand = false;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        fuelCount -= Time.deltaTime;

        rb.velocity = transform.forward * ForwardSpeed;

        FindTarget();
        FireAtSpitfire();
        FireAtHurricane();
        SetMovement();
        AssessDamage();

        if (fuelCount < 0)
        {
            hasFuel = false;
        }
        if(ammunition < 0)
        {
            hasFuel = false;
        }

        if (!defaultTarget)
        {
            defaultTarget = originalAirbase;
        }
        if (!CurrentTarget)
        {
            CurrentTarget = defaultTarget;
        }
        
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

    public void FindTarget() // Main update to determine the current target of this plane
    {
        if (hasFuel == true) // check if the plane has fuel
        {
            if (britishFighterList.Count > 0) // check if enemy planes exist in enemy plane list
            {
                for (int i = 0; i < britishFighterList.Count; i++) // clycle through each plane in enemy plane list
                {
                    if (britishFighterList[i]) // check that this instance of enemy plane actually exists
                    {
                        GameObject enemyPlane = britishFighterList[i];
                        if (enemyPlane.tag == ("SPITFIRE")) // check what type of plane the enemy plane is
                        {
                            SpitfireAI spitfirePick = enemyPlane.GetComponent<SpitfireAI>();

                            float distance = Vector3.Distance(transform.position, enemyPlane.transform.position);

                            if ((distance < 40.0f) && (CurrentTarget.tag != ("SPITFIRE")) && (CurrentTarget.tag != ("HURRICANE"))) // distance 40 represents the range at which it will follow the enemy
                            {
                                int spreadTarget = Random.Range(1, 6); // this spreads out targets so that target is not always first plane in list.
                                if (spreadTarget == 1)
                                {
                                    CurrentTarget = enemyPlane;
                                    spitfirePick.AddToChaserList(transform.root.gameObject);
                                }
                            }
                        }

                        if (enemyPlane.tag == ("HURRICANE"))
                        {
                            HurricaneAI hurricanePick = enemyPlane.GetComponent<HurricaneAI>();

                            float distance = Vector3.Distance(transform.position, enemyPlane.transform.position);

                            if ((distance < 40.0f) && (CurrentTarget.tag != ("SPITFIRE")) && (CurrentTarget.tag != ("HURRICANE")))
                            {
                                int spreadTarget = Random.Range(1, 6); // this spreads out targets so that target is not always first plane in list.
                                if (spreadTarget == 1)
                                {
                                    CurrentTarget = enemyPlane;
                                    hurricanePick.AddToChaserList(transform.root.gameObject);
                                }
                            }
                        }
                    }
                    
                }
            }
            distance = 100.0f;
        }
        else
        {
            if (CurrentTarget.tag == "SPITFIRE")  // If it has a target while running out of fuel, it removes from chaser!
            {
                SpitfireAI enemyPlane = CurrentTarget.GetComponent<SpitfireAI>();
                enemyPlane.RemoveFromChaserList(transform.root.gameObject);
            }
            if (CurrentTarget.tag == "HURRICANE")  // If it has a target while running out of fuel, it removes from chaser!
            {
                HurricaneAI enemyPlane = CurrentTarget.GetComponent<HurricaneAI>();
                enemyPlane.RemoveFromChaserList(transform.root.gameObject);
            }

            CurrentTarget = originalAirbase;

            float homeDistance = Vector3.Distance(transform.position, originalAirbase.transform.position);
            if(homeDistance < 10)
            {
                Land();
            }
        }
    }

    void SetMovement()
    {

        if (CurrentTarget == null)
        {
            return;
        }

        Vector3 TargetDir = CurrentTarget.transform.position - transform.position;
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
        if ((SignedAngle > 0.0f) && (Angle > 2.0f))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * TurnSpeed, Space.World);
        }

        // Turn Clockwise
        if ((SignedAngle < 0.0f) && (Angle > 2.0f))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * TurnSpeed, Space.World);
        }

        if ((transform.position.y > 80) || (transform.position.y < 80))                             //Maintains aircraft height at 80
        {
            transform.position = new Vector3(transform.position.x, 80, transform.position.z);
        }
    }

    void FireAtSpitfire()
    {
        if (CurrentTarget == null)
        {
            return;
        }

        fireDistance = 100.0f;

        if (CurrentTarget.tag == "SPITFIRE")
        {

            fireDistance = Vector3.Distance(transform.position, CurrentTarget.transform.position);
        }

        if ((CurrentTarget.tag == "SPITFIRE")
           && (fireDistance < 20.0f)
           && (isFacing == true)
           )
        {
            isFiring = true;
            if (fireDistance > 20.0f) // If the target pulls range, remove from chaser list.
            {
                isFiring = false;
                SpitfireAI spitfireTarget = CurrentTarget.GetComponent<SpitfireAI>();
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
                timer = 2.0f;
                ammunition = ammunition - 1;

                SpitfireAI spitfireTarget = CurrentTarget.GetComponent<SpitfireAI>();
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
                    spitfireTarget.GetHitByBf109();
                    spitfireTarget.RemoveFromChaserList(transform.root.gameObject);
                    CurrentTarget = defaultTarget;
                }
            }
        }
    }

    void FireAtHurricane()
    {
        if (CurrentTarget == null)
        {
            return;
        }

        fireDistance = 100.0f;       

        if (CurrentTarget.tag == "HURRICANE")
        {

            fireDistance = Vector3.Distance(transform.position, CurrentTarget.transform.position);
        }

        if ((CurrentTarget.tag == "HURRICANE")
           && (fireDistance < 20.0f)
           && (isFacing == true)
           )
        {
            isFiring = true;
            if (fireDistance > 20.0f) // pulling range
            {
                isFiring = false;
                HurricaneAI hurricaneTarget = CurrentTarget.GetComponent<HurricaneAI>();
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
                timer = 2.0f;
                ammunition = ammunition - 1;

                HurricaneAI hurricaneTarget = CurrentTarget.GetComponent<HurricaneAI>();
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
                    hurricaneTarget.GetHitByBf109();
                    hurricaneTarget.RemoveFromChaserList(transform.root.gameObject);
                    CurrentTarget = defaultTarget;
                }
            }
        }
    }

    public void GetHitBySpitfire()
    {
        integrity = integrity - 2;

        int pilotHitChance = Random.Range(1, 8); // chance to hit pilot
        if (pilotHitChance == 1)
        {
            pilot = 1;
            accuracy = accuracy - 1;
            maneuverability = maneuverability - 1;
            int pilotKillChance = Random.Range(1, 3);
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

        int pilotHitChance = Random.Range(1, 8); // chance to hit pilot
        if (pilotHitChance == 1)
        {
            pilot = 1;
            accuracy = accuracy - 1;
            maneuverability = maneuverability - 1;
            int pilotKillChance = Random.Range(1, 3);
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

    public void GetDestroyed()
    {
        if(safeLand == false)
        {
            Debug.Log("BF109 shot down");
        }
        if(safeLand == true)
        {
            Debug.Log("BF109 landed");
        }
        
        transform.position = new Vector3(9999.0f, 9999.0f, 9999.0f); // Teleports away to execute code first

        if (CurrentTarget.tag == "SPITFIRE") // Removes itself from the enemy list of planes targetting this planes current target
        {
            SpitfireAI targetPlane = CurrentTarget.GetComponent<SpitfireAI>();
            targetPlane.RemoveFromChaserList(transform.root.gameObject);
        }

        if (CurrentTarget.tag == "HURRICANE") // Removes itself from the enemy list of planes targetting this planes current target
        {
            HurricaneAI targetPlane = CurrentTarget.GetComponent<HurricaneAI>();
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
        score.addGermanFighter(1);

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

                TurnSpeed = TurnSpeed - 10;
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
                TurnSpeed = TurnSpeed - 20;
                ForwardSpeed = ForwardSpeed - 2;
                healthyEngine = false;
            }
        }
    }

    void Land()
    {
        GermanAirfield airbaseScript = originalAirbase.GetComponent<GermanAirfield>();
        airbaseScript.AddBF109(); // readd BF109
        safeLand = true;
        ScoreScreen score = GameObject.Find("ScoreScreen").GetComponent<ScoreScreen>();
        score.addGermanFighter(-1);
        GetDestroyed();    
    }

    public void RemoveTarget(GameObject target)
    {
        britishFighterList.Remove(target);
    }

    public void ResetTarget(GameObject self)
    {
        CurrentTarget = defaultTarget;
    }

    public void AddToChaserList(GameObject enemy)
    {
        chaserList.Add(enemy);
    }

    public void RemoveFromChaserList(GameObject enemy)
    {
        chaserList.Remove(enemy);
    }

    public void AddToTargetsList(GameObject enemy)
    {
        britishFighterList.Add(enemy);
    }

    public void AssignMission(int mission)
    {
        missionDesignation = mission;
    }

    public void setCurrentTarget(GameObject target)
    {
        CurrentTarget = target;
    }

    public void setDefaultTarget(GameObject target)
    {
        defaultTarget = target;
    }

    public void SetAssignedEscort(GameObject target)
    {
        assignedEscort = target;
    }

    public void setOriginalAirbase(GameObject airbase)
    {
        originalAirbase = airbase;
    }

    public int GetManeuverability()
    {
        return maneuverability;
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
    public float getFuel()
    {
        return fuelCount;
    }
    public int GetAmmunition()
    {
        return ammunition;
    }

}


