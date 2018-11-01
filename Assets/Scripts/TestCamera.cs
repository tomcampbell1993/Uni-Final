using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestCamera : MonoBehaviour
{

    public float cameraSpeed;


    private Vector3 offset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("up"))
        {
            pos.z += cameraSpeed * Time.deltaTime;
        }

        if (Input.GetKey("down"))
        {
            pos.z -= cameraSpeed * Time.deltaTime;
        }

        if (Input.GetKey("left"))
        {
            pos.x -= cameraSpeed * Time.deltaTime;
        }

        if (Input.GetKey("right"))
        {
            pos.x += cameraSpeed * Time.deltaTime;
        }

        pos.y -= (Input.GetAxis("Mouse ScrollWheel") * 30);

        transform.position = pos;
    }
}
