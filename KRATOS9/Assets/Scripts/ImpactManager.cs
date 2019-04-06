using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kratos9
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
            Kratos9.movement_manager collided_movement_manager       = collided.GetComponent<Kratos9.movement_manager>();
            Kratos9.movement_manager other_collider_movement_manager = contact.otherCollider.transform.parent.GetComponent<Kratos9.movement_manager>();

<<<<<<< HEAD

=======
>>>>>>> parent of 22552a2... ManyDicks
            if (!collided_movement_manager.hitting)
            {
         
                if (!other_collider_movement_manager.hitting)
                {

                    collided_movement_manager.RecieveImpact(contact.normal * (collided_movement_manager.GetSpeed() + other_collider_movement_manager.GetSpeed()));

                    other_collider_movement_manager.RecieveImpact(-contact.normal * (collided_movement_manager.GetSpeed() + other_collider_movement_manager.GetSpeed()));
                }
                else
                {
                    collided_movement_manager.RecieveImpact(contact.normal * (collided_movement_manager.dash_speed + other_collider_movement_manager.GetSpeed()));
                    other_collider_movement_manager.GetComponent<Kratos9.punch_manager>().StopPunchEffect();
                }
            }
            else{
                collided_movement_manager.GetComponent<Kratos9.punch_manager>().StopPunchEffect();

                if (!other_collider_movement_manager.hitting)
                {
                    other_collider_movement_manager.RecieveImpact(-contact.normal * (collided_movement_manager.dash_speed + other_collider_movement_manager.GetSpeed()));
                }
                else
                {
                    other_collider_movement_manager.GetComponent<Kratos9.punch_manager>().StopPunchEffect();
                }
            }

        }

    }
}

