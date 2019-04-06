using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jota
{
    public class ImpactManager
    {

        /// <summary>
        /// Cambia las direcciones y las velocidades entre los botes que colisionan.
        /// </summary>
        /// <param name="collided"></param>
        /// <param name="contact"></param>
        public void CalculateImpactForces(Transform collided, ContactPoint contact)
        {
            collided.GetComponent<pokoi.movement_manager>().RecieveImpact(contact.normal * (collided.GetComponent<pokoi.movement_manager>().GetSpeed() + contact.otherCollider.transform.parent.GetComponent<pokoi.movement_manager>().GetSpeed()));

            contact.otherCollider.transform.parent.GetComponent<pokoi.movement_manager>().RecieveImpact(-contact.normal * (collided.GetComponent<pokoi.movement_manager>().GetSpeed() + collided.GetComponent<pokoi.movement_manager>().GetSpeed()));
        }

    }
}

