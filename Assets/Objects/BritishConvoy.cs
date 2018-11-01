using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BritishConvoy : MonoBehaviour
{

    public GameObject currentTarget;
    public GameObject fighterInputObject;
    public GameObject britishConvoy;
    public GameObject bomberObject;

    float Angle;
    float SignedAngle;

    public int turnSpeed;
    public int forwardSpeed;

    private Rigidbody rb;
    BritishStockpileUI britishStockpileUI;


    void Start()
    {

        rb = GetComponent<Rigidbody>();

        turnSpeed = 30;
        forwardSpeed = 5;

        currentTarget = GameObject.Find("convoy1");
        fighterInputObject = GameObject.Find("FighterInput");

        britishStockpileUI = fighterInputObject.GetComponent<BritishStockpileUI>();
    }


    void Update()
    {

        rb.velocity = transform.forward * forwardSpeed;

        SetMovement();
        findWaypoint();

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

        // Turn Anticlockwise
        if ((SignedAngle > 0.0f) && (Angle > 5.0f))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * turnSpeed, Space.World);
        }

        // Turn Clockwise
        if ((SignedAngle < 0.0f) && (Angle > 5.0f))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * turnSpeed, Space.World);
        }

        if ((transform.position.y > 60) || (transform.position.y < 60))                             //Maintains boat height at 60
        {
            transform.position = new Vector3(transform.position.x, 60, transform.position.z);
        }
    }

    void findWaypoint()
    {
        GameObject convoy1 = GameObject.Find("convoy1");
        GameObject convoy2 = GameObject.Find("convoy2");
        GameObject convoyComplete = GameObject.Find("convoyComplete");

        float distance1 = Vector3.Distance(transform.position, convoy1.transform.position);
        float distance2 = Vector3.Distance(transform.position, convoy2.transform.position);
        float distanceComplete = Vector3.Distance(transform.position, convoyComplete.transform.position);

        if (distance1 < 10)
        {
            currentTarget = convoy2;
        }
        if (distance2 < 10)
        {
            currentTarget = convoyComplete;
        }
        if (distanceComplete < 10)
        {
            Dock();
        }
    }

    void Dock()
    {
        britishStockpileUI.AddPlanesBuilt(8);

        GameObject convoyInstance = Instantiate(britishConvoy, new Vector3(3.0f, 60.0f, 132.0f), transform.rotation) as GameObject;

        GetDestroyed();
    }

    public void GetDestroyed()
    {
        Destroy(transform.root.gameObject);
    }

    public void GetBombed()
    {
        transform.position = new Vector3(9999.0f, 9999.0f, 9999.0f);
        Debug.Log("Convoy bombed");

        foreach (GameObject bomber in GameObject.FindGameObjectsWithTag("HEINKEL"))
        {
            Heinkel heinkel = bomber.GetComponent<Heinkel>();
            heinkel.ResetToEnemyAirbase();
            heinkel.RemoveConvoyTarget();
        }
        foreach (GameObject bomber in GameObject.FindGameObjectsWithTag("DORNIER"))
        {
            Dornier dornier = bomber.GetComponent<Dornier>();
            dornier.ResetToEnemyAirbase();
            dornier.RemoveConvoyTarget();
        }
        foreach (GameObject bomber in GameObject.FindGameObjectsWithTag("JUNKERS"))
        {
            Junkers junkers = bomber.GetComponent<Junkers>();
            junkers.ResetToEnemyAirbase();
            junkers.RemoveConvoyTarget();
        }

        GetDestroyed();
    }
}
