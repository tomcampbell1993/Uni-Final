using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HurricaneAI : MonoBehaviour
{

    public List<GameObject> enemyList = new List<GameObject>(); // list of potential enemies
    public List<GameObject> chaserList = new List<GameObject>(); // list of enemies currently chasing this plane
    public MesserschmidtAI messerschmidtAI;
    public GameTimer gameTimerScript;
    public Menu menuScript;

    public bool isFiring;
    public float timer;

    public GameObject currentTarget;
    public GameObject originalAirbase;
    public GameObject defaultTarget;
    public GameObject testProjectile;
    public GameObject gameTimerObject;
    public GameObject menuObject;

    public float ForwardSpeed;
    public float TurnSpeed;
    public float distance = 100.0f;
    public float fireDistance = 100.0f;

    public float Angle;
    public float SignedAngle;
    public bool isFacing;
    public bool hasFuel;
    public float fuelCount;
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

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameTimerObject = GameObject.Find("GameTimer");
        gameTimerScript = gameTimerObject.GetComponent<GameTimer>();

        menuObject = GameObject.Find("Menu");
        menuScript = menuObject.GetComponent<Menu>();
        gameObject.name = "Hawker Hurricane";

        isFacing = false;
        isFiring = false;

        foreach (GameObject messerschmidt in GameObject.FindGameObjectsWithTag("BF109")) //Lists itself as an enemy target.
        {
            messerschmidtAI = messerschmidt.GetComponent<MesserschmidtAI>();
            messerschmidtAI.AddToTargetsList(transform.root.gameObject);
        }
        foreach (GameObject bf110 in GameObject.FindGameObjectsWithTag("BF110")) //Lists itself as an enemy target.
        {
            BF110AI bf110AI = bf110.GetComponent<BF110AI>();
            bf110AI.AddToTargetsList(transform.root.gameObject);
        }
        foreach (GameObject heinkel in GameObject.FindGameObjectsWithTag("HEINKEL")) //Lists itself as an enemy target.
        {
            Heinkel heinkelAI = heinkel.GetComponent<Heinkel>();
            heinkelAI.AddToTargetsList(transform.root.gameObject);
        }
        foreach (GameObject junkers in GameObject.FindGameObjectsWithTag("JUNKERS")) //Lists itself as an enemy target.
        {
            Junkers junkersAI = junkers.GetComponent<Junkers>();
            junkersAI.AddToTargetsList(transform.root.gameObject);
        }
        foreach (GameObject dornier in GameObject.FindGameObjectsWithTag("DORNIER")) //Lists itself as an enemy target.
        {
            Dornier dornierAI = dornier.GetComponent<Dornier>();
            dornierAI.AddToTargetsList(transform.root.gameObject);
        }

        currentTarget = defaultTarget;

        ForwardSpeed = 9;
        TurnSpeed = 45;
        integrity = 100;
        accuracy = 2;
        maneuverability = 4;

        pilot = 0;
        engine = 0;
        firepower = 5;

        healthyPilot = true;
        healthyEngine = true;
        safeLand = false;

        hasFuel = true;
        fuelCount = 80;
        ammunition = 17;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        rb.velocity = transform.forward * ForwardSpeed;

        FindTarget();
        FireAtBF109();
        FireAtHeinkel();
        FireAtJunkers();
        FireAtDornier();
        SetMovement();
        AssessDamage();

        fuelCount -= Time.deltaTime;
        if (fuelCount < 0)
        {
            hasFuel = false;
        }
        if(ammunition < 0)
        {
            hasFuel = false;
        }

        if (currentTarget == null)
        {
            currentTarget = defaultTarget;
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

    void FindTarget()
    {
        if (hasFuel == true)
        {
            if (enemyList.Count > 0)
            {
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if (enemyList[i])
                    {
                        GameObject enemyPlane = enemyList[i];
                        if (enemyPlane.tag == "BF109")
                        {
                            MesserschmidtAI messerschmidtPick = enemyPlane.GetComponent<MesserschmidtAI>();

                            float distance = Vector3.Distance(transform.position, enemyPlane.transform.position);

                            if ((distance < 150.0f)
                                && (currentTarget.tag != ("BF109"))
                                && (currentTarget.tag != ("BF110"))
                                && (currentTarget.tag != ("HEINKEL"))
                                && (currentTarget.tag != ("JUNKERS"))
                                && (currentTarget.tag != ("DORNIER"))
                                )
                            {

                                int spreadTarget = Random.Range(1, 6); // this spreads out targets so that target is not always first plane in list.
                                if (spreadTarget == 1)
                                {
                                    currentTarget = enemyPlane;
                                    messerschmidtPick.AddToChaserList(transform.root.gameObject);
                                }
                            }
                        }

                        if (enemyPlane.tag == "BF110")
                        {
                            BF110AI bf110Pick = enemyPlane.GetComponent<BF110AI>();

                            float distance = Vector3.Distance(transform.position, enemyPlane.transform.position);

                            if ((distance < 150.0f)
                                && (currentTarget.tag != ("BF109"))
                                && (currentTarget.tag != ("BF110"))
                                && (currentTarget.tag != ("HEINKEL"))
                                && (currentTarget.tag != ("JUNKERS"))
                                && (currentTarget.tag != ("DORNIER"))
                                )
                            {

                                int spreadTarget = Random.Range(1, 6); // this spreads out targets so that target is not always first plane in list.
                                if (spreadTarget == 1)
                                {
                                    currentTarget = enemyPlane;
                                    bf110Pick.AddToChaserList(transform.root.gameObject);
                                }
                            }
                        }

                        if (enemyPlane.tag == "HEINKEL")
                        {
                            Heinkel heinkelPick = enemyPlane.GetComponent<Heinkel>();

                            float distance = Vector3.Distance(transform.position, enemyPlane.transform.position);

                            if ((distance < 150.0f)
                                && (currentTarget.tag != ("BF109"))
                                && (currentTarget.tag != ("BF110"))
                                && (currentTarget.tag != ("HEINKEL"))
                                && (currentTarget.tag != ("JUNKERS"))
                                && (currentTarget.tag != ("DORNIER"))
                                )
                            {

                                int spreadTarget = Random.Range(1, 6); // this spreads out targets so that target is not always first plane in list.
                                if (spreadTarget == 1)
                                {
                                    currentTarget = enemyPlane;
                                    heinkelPick.AddToChaserList(transform.root.gameObject);
                                }
                            }
                        }

                        if (enemyPlane.tag == "JUNKERS")
                        {
                            Junkers junkersPick = enemyPlane.GetComponent<Junkers>();

                            float distance = Vector3.Distance(transform.position, enemyPlane.transform.position);

                            if ((distance < 150.0f)
                                && (currentTarget.tag != ("BF109"))
                                && (currentTarget.tag != ("BF110"))
                                && (currentTarget.tag != ("HEINKEL"))
                                && (currentTarget.tag != ("JUNKERS"))
                                && (currentTarget.tag != ("DORNIER"))
                                )
                            {

                                int spreadTarget = Random.Range(1, 6); // this spreads out targets so that target is not always first plane in list.
                                if (spreadTarget == 1)
                                {
                                    currentTarget = enemyPlane;
                                    junkersPick.AddToChaserList(transform.root.gameObject);
                                }
                            }
                        }

                        if (enemyPlane.tag == "DORNIER")
                        {
                            Dornier dornierPick = enemyPlane.GetComponent<Dornier>();

                            float distance = Vector3.Distance(transform.position, enemyPlane.transform.position);

                            if ((distance < 150.0f)
                                && (currentTarget.tag != ("BF109"))
                                && (currentTarget.tag != ("BF110"))
                                && (currentTarget.tag != ("HEINKEL"))
                                && (currentTarget.tag != ("JUNKERS"))
                                && (currentTarget.tag != ("DORNIER"))
                                )
                            {

                                int spreadTarget = Random.Range(1, 6); // this spreads out targets so that target is not always first plane in list.
                                if (spreadTarget == 1)
                                {
                                    currentTarget = enemyPlane;
                                    dornierPick.AddToChaserList(transform.root.gameObject);
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
            if (currentTarget.tag == "BF109")  // If it has a target while running out of fuel, it removes from chaser!
            {
                MesserschmidtAI enemyPlane = currentTarget.GetComponent<MesserschmidtAI>();
                enemyPlane.RemoveFromChaserList(transform.root.gameObject);
            }
            if (currentTarget.tag == "BF110")  // If it has a target while running out of fuel, it removes from chaser!
            {
                BF110AI enemyPlane = currentTarget.GetComponent<BF110AI>();
                enemyPlane.RemoveFromChaserList(transform.root.gameObject);
            }
            if (currentTarget.tag == "HEINKEL")  // If it has a target while running out of fuel, it removes from chaser!
            {
                Heinkel enemyPlane = currentTarget.GetComponent<Heinkel>();
                enemyPlane.RemoveFromChaserList(transform.root.gameObject);
            }
            if (currentTarget.tag == "DORNIER")  // If it has a target while running out of fuel, it removes from chaser!
            {
                Dornier enemyPlane = currentTarget.GetComponent<Dornier>();
                enemyPlane.RemoveFromChaserList(transform.root.gameObject);
            }
            if (currentTarget.tag == "JUNKERS")  // If it has a target while running out of fuel, it removes from chaser!
            {
                Junkers enemyPlane = currentTarget.GetComponent<Junkers>();
                enemyPlane.RemoveFromChaserList(transform.root.gameObject);
            }

            currentTarget = originalAirbase;

            float homeDistance = Vector3.Distance(transform.position, originalAirbase.transform.position);
            if (homeDistance < 10)
            {
                Land();
            }
        }
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
        if ((SignedAngle > 0.0f) && (Angle > 2.0f))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * TurnSpeed, Space.World);
        }

        // Turn Clockwise
        if ((SignedAngle < 0.0f) && (Angle > 2.0f))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * TurnSpeed, Space.World);
        }

        if ((transform.position.y > 80) || (transform.position.y < 80))
        {
            transform.position = new Vector3(transform.position.x, 80, transform.position.z);
        }
    }

    void FireAtBF109()
    {
        if (currentTarget == null)
        {
            return;
        }

        fireDistance = 100.0f;

        if (currentTarget.tag == "BF109")
        {

            fireDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
        }

        if ((currentTarget.tag == "BF109")
           && (fireDistance < 20.0f)
           && (isFacing == true)
           )
        {
            isFiring = true;
            if (fireDistance > 20.0f) // pulling range
            {
                isFiring = false;
                MesserschmidtAI fireTarget = currentTarget.GetComponent<MesserschmidtAI>();
                fireTarget.RemoveFromChaserList(transform.root.gameObject);
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

                MesserschmidtAI bf109Target = currentTarget.GetComponent<MesserschmidtAI>();
                int enemyManeuverability = bf109Target.GetManeuverability();

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
                    bf109Target.GetHitByHurricane();
                    bf109Target.RemoveFromChaserList(transform.root.gameObject);
                    currentTarget = defaultTarget;
                }
            }
        }
    }

    void FireAtBF110()
    {
        if (currentTarget == null)
        {
            return;
        }

        fireDistance = 100.0f;

        if (currentTarget.tag == "BF110")
        {

            fireDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
        }

        if ((currentTarget.tag == "BF110")
           && (fireDistance < 20.0f)
           && (isFacing == true)
           )
        {
            isFiring = true;
            if (fireDistance > 20) // pulling range
            {
                isFiring = false;
                BF110AI fireTarget = currentTarget.GetComponent<BF110AI>();
                fireTarget.RemoveFromChaserList(transform.root.gameObject);
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

                BF110AI bf110Target = currentTarget.GetComponent<BF110AI>();
                int enemyManeuverability = bf110Target.GetManeuverability();

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
                    bf110Target.GetHitByHurricane();
                    bf110Target.RemoveFromChaserList(transform.root.gameObject);
                    currentTarget = defaultTarget;
                }
            }
        }
    }

    void FireAtHeinkel()
    {
        if (currentTarget == null)
        {
            return;
        }

        fireDistance = 100.0f;

        if (currentTarget.tag == "HEINKEL")
        {

            fireDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
        }

        if ((currentTarget.tag == "HEINKEL")
           && (fireDistance < 20.0f)
           && (isFacing == true)
           )
        {
            isFiring = true;
            if (fireDistance > 20) // pulling range
            {
                isFiring = false;
                Heinkel fireTarget = currentTarget.GetComponent<Heinkel>();
                fireTarget.RemoveFromChaserList(transform.root.gameObject);
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

                Heinkel heinkelTarget = currentTarget.GetComponent<Heinkel>();
                int enemyManeuverability = heinkelTarget.GetManeuverability();

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
                    heinkelTarget.GetHitByHurricane();
                    heinkelTarget.RemoveFromChaserList(transform.root.gameObject);
                    currentTarget = defaultTarget;
                }
            }
        }
    }

    void FireAtJunkers()
    {
        if (currentTarget == null)
        {
            return;
        }

        fireDistance = 100.0f;

        if (currentTarget.tag == "JUNKERS")
        {

            fireDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
        }

        if ((currentTarget.tag == "JUNKERS")
           && (fireDistance < 20.0f)
           && (isFacing == true)
           )
        {
            isFiring = true;
            if (fireDistance > 20) // pulling range
            {
                isFiring = false;
                Junkers fireTarget = currentTarget.GetComponent<Junkers>();
                fireTarget.RemoveFromChaserList(transform.root.gameObject);
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

                Junkers junkersTarget = currentTarget.GetComponent<Junkers>();
                int enemyManeuverability = junkersTarget.GetManeuverability();

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
                    junkersTarget.GetHitByHurricane();
                    junkersTarget.RemoveFromChaserList(transform.root.gameObject);
                    currentTarget = defaultTarget;
                }
            }
        }
    }

    void FireAtDornier()
    {
        if (currentTarget == null)
        {
            return;
        }

        fireDistance = 100.0f;

        if (currentTarget.tag == "DORNIER")
        {

            fireDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
        }

        if ((currentTarget.tag == "DORNIER")
           && (fireDistance < 20.0f)
           && (isFacing == true)
           )
        {
            isFiring = true;
            if (fireDistance > 20) // pulling range
            {
                isFiring = false;
                Dornier fireTarget = currentTarget.GetComponent<Dornier>();
                fireTarget.RemoveFromChaserList(transform.root.gameObject);
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

                Dornier dornierTarget = currentTarget.GetComponent<Dornier>();
                int enemyManeuverability = dornierTarget.GetManeuverability();

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
                    dornierTarget.GetHitByHurricane();
                    dornierTarget.RemoveFromChaserList(transform.root.gameObject);
                    currentTarget = defaultTarget;
                }
            }
        }
    }

    public void GetHitByBf109()
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

    public void GetHitByBf110()
    {
        integrity = integrity - 8;

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

        int engineHitChance = Random.Range(1, 5); // chance to hit engine
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

    public void GetHitByHeinkel()
    {
        integrity = integrity - 1;

        int pilotHitChance = Random.Range(1, 18); // chance to hit pilot
        if (pilotHitChance == 1)
        {
            pilot = 1;
            accuracy = accuracy - 1;
            maneuverability = maneuverability - 1;
            int pilotKillChance = Random.Range(1, 5);
            if (pilotKillChance == 1)
            {
                pilot = 2;
                GetDestroyed();
            }
        }

        int engineHitChance = Random.Range(1, 16); // chance to hit engine
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

    public void GetHitByJunkers()
    {
        integrity = integrity - 1;

        int pilotHitChance = Random.Range(1, 20); // chance to hit pilot
        if (pilotHitChance == 1)
        {
            pilot = 1;
            accuracy = accuracy - 1;
            maneuverability = maneuverability - 1;
            int pilotKillChance = Random.Range(1, 5);
            if (pilotKillChance == 1)
            {
                pilot = 2;
                GetDestroyed();
            }
        }

        int engineHitChance = Random.Range(1, 18); // chance to hit engine
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

    public void GetHitByDornier()
    {
        integrity = integrity - 1;

        int pilotHitChance = Random.Range(1, 20); // chance to hit pilot
        if (pilotHitChance == 1)
        {
            pilot = 1;
            accuracy = accuracy - 1;
            maneuverability = maneuverability - 1;
            int pilotKillChance = Random.Range(1, 5);
            if (pilotKillChance == 1)
            {
                pilot = 2;
                GetDestroyed();
            }
        }

        int engineHitChance = Random.Range(1, 18); // chance to hit engine
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

        if (safeLand == false)
        {
            Debug.Log("Hurricane shot down");
        }
        if (safeLand == true)
        {
            Debug.Log("Hurricane landed");
        }
        transform.position = new Vector3(-9999.0f, -9999.0f, -9999.0f); // Teleports away to execute code first

        if (currentTarget.tag == "BF109") // Removes itself from the enemy list of planes targetting this planes current target
        {
            MesserschmidtAI targetPlane = currentTarget.GetComponent<MesserschmidtAI>();
            targetPlane.RemoveFromChaserList(transform.root.gameObject);
        }
        if (currentTarget.tag == "BF110")
        {
            BF110AI targetPlane = currentTarget.GetComponent<BF110AI>();
            targetPlane.RemoveFromChaserList(transform.root.gameObject);
        }
        if (currentTarget.tag == "HEINKEL")
        {
            Heinkel targetPlane = currentTarget.GetComponent<Heinkel>();
            targetPlane.RemoveFromChaserList(transform.root.gameObject);
        }
        if (currentTarget.tag == "JUNKERS")
        {
            Junkers targetPlane = currentTarget.GetComponent<Junkers>();
            targetPlane.RemoveFromChaserList(transform.root.gameObject);
        }
        if (currentTarget.tag == "DORNIER")
        {
            Dornier targetPlane = currentTarget.GetComponent<Dornier>();
            targetPlane.RemoveFromChaserList(transform.root.gameObject);
        }

        foreach (GameObject chaserPlane in chaserList)  // any planes targetting this plane will have their targets reset
        {
            if (chaserPlane.tag == "BF109")
            {
                MesserschmidtAI bf109AI = chaserPlane.GetComponent<MesserschmidtAI>();
                bf109AI.ResetTarget(chaserPlane);
            }
            if (chaserPlane.tag == "BF110")
            {
                BF110AI bf110AI = chaserPlane.GetComponent<BF110AI>();
                bf110AI.ResetTarget(chaserPlane);
            }
            if (chaserPlane.tag == "HEINKEL")
            {
                Heinkel heinkelAI = chaserPlane.GetComponent<Heinkel>();
                heinkelAI.ResetTarget(chaserPlane);
            }
            if (chaserPlane.tag == "JUNKERS")
            {
                Junkers junkersAI = chaserPlane.GetComponent<Junkers>();
                junkersAI.ResetTarget(chaserPlane);
            }
            if (chaserPlane.tag == "DORNIER")
            {
                Dornier dornierAI = chaserPlane.GetComponent<Dornier>();
                dornierAI.ResetTarget(chaserPlane);
            }
        }

        foreach (GameObject enemyPlane in enemyList) // removes itself from the enemy list of potential targets
        {
            if (enemyPlane.tag == "BF109")
            {
                MesserschmidtAI bf109AI = enemyPlane.GetComponent<MesserschmidtAI>();
                bf109AI.RemoveTarget(transform.root.gameObject);
            }
            if (enemyPlane.tag == "BF110")
            {
                BF110AI bf110AI = enemyPlane.GetComponent<BF110AI>();
                bf110AI.RemoveTarget(transform.root.gameObject);
            }
            if (enemyPlane.tag == "HEINKEL")
            {
                Heinkel heinkelAI = enemyPlane.GetComponent<Heinkel>();
                heinkelAI.RemoveTarget(transform.root.gameObject);
            }
            if (enemyPlane.tag == "JUNKERS")
            {
                Junkers junkersAI = enemyPlane.GetComponent<Junkers>();
                junkersAI.RemoveTarget(transform.root.gameObject);
            }
            if (enemyPlane.tag == "DORNIER")
            {
                Dornier dornierAI = enemyPlane.GetComponent<Dornier>();
                dornierAI.RemoveTarget(transform.root.gameObject);
            }
        }

        ScoreScreen score = GameObject.Find("ScoreScreen").GetComponent<ScoreScreen>();
        score.addBritishFighter(1);

        Destroy(transform.root.gameObject);

        if (menuScript.GetSelectedObject() == gameObject)
        {
            menuScript.SetSelectedObject(GameObject.Find("Land"));
        }

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
        BritishAirfield airbaseScript = originalAirbase.GetComponent<BritishAirfield>();
        airbaseScript.AddHurricane(); // readd Hurricane
        safeLand = true;
        ScoreScreen score = GameObject.Find("ScoreScreen").GetComponent<ScoreScreen>();
        score.addBritishFighter(-1);

        GetDestroyed();
    }

    public void RemoveTarget(GameObject target)
    {
        enemyList.Remove(target);
    }

    public void ResetTarget(GameObject self)
    {
        currentTarget = defaultTarget;
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
        enemyList.Add(enemy);
    }

    public void SetDefaultTarget(GameObject target)
    {
        defaultTarget = target;
    }

    public void SetOriginalAirbase(GameObject airbase)
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
