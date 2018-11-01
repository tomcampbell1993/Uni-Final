using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEscortNumberButtons : MonoBehaviour
{

    public GameObject GEscortNumberObject;

    // Use this for initialization
    void Start()
    {
        GEscortNumberObject = GameObject.Find("GEscortNumber");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseSize()
    {
        GEscortNumber GEscortNumberScript = GEscortNumberObject.GetComponent<GEscortNumber>();
        GEscortNumberScript.IncreasePlanes();
    }

    public void DecreaseSize()
    {
        GEscortNumber GEscortNumberScript = GEscortNumberObject.GetComponent<GEscortNumber>();
        GEscortNumberScript.DecreasePlanes();
    }
}
