using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_detector : MonoBehaviour
{

    Jota.ImpactManager impactManager = new Jota.ImpactManager();
    Transform parent_tr;

    private void Start()
    {
        parent_tr = transform.parent;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Boat1")
        {

            impactManager.CalculateImpactForces(parent_tr, collision.GetContact(0));
            Debug.Log("Hey");
        }
    }
}
