using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleForwardMovement : MonoBehaviour
{

    private Rigidbody my_rb;
    private Transform my_tr;
    public float movement_speed;

    // Start is called before the first frame update
    void Start()
    {
        my_rb = GetComponent<Rigidbody>();

        my_tr = GetComponent<Transform>();
        my_rb.velocity = my_tr.forward * movement_speed;

    }

    // Update is called once per frame
    void Update()
    {
    }
}
