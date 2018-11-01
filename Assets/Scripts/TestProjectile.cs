using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour {

    public float timer = 10.0f;
    public float ForwardSpeed;

    private Rigidbody rb;

    void Start () {

        rb = GetComponent<Rigidbody>();
    }
	
	void Update () {

        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            Destroy(gameObject);
        }

        rb.velocity = transform.forward * ForwardSpeed;

    }
}
