using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kratos9
{
    
    public class collision_detector : MonoBehaviour
    {

        Kratos9.ImpactManager impactManager = new Kratos9.ImpactManager();
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

            }
        }
    }
}
