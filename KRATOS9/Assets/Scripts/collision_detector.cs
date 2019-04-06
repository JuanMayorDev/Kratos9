using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kratos9
{
    
    public class collision_detector : MonoBehaviour
    {

        Kratos9.ImpactManager impactManager = new Kratos9.ImpactManager();

        private void Start()
        {
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Boat1")
            {
                impactManager.CalculateImpactForces(transform, collision.GetContact(0));

            }
        }
    }
}
