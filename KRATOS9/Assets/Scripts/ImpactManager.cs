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
            Vector3 temp_normal = contact.normal;
            float temp_movement_speed = collided.GetComponent<SimpleForwardMovement>().movement_speed;

            //DIRECCIONES DEL MOVIMIENTO
            contact.otherCollider.transform.GetComponent<SimpleForwardMovement>().vector_velocity_direction = -contact.normal;
            collided.GetComponent<SimpleForwardMovement>().vector_velocity_direction = temp_normal;

            //VELOCIDADES, DAMOS NUESTRA VELOCIDAD Y RECIBIMOS LA MITAD DE NUESTRA VELOCIDAD MÁS LA VELOCIDAD DEL OTRO
            collided.GetComponent<SimpleForwardMovement>().movement_speed = temp_movement_speed / 2 + contact.otherCollider.GetComponent<SimpleForwardMovement>().movement_speed;
            contact.otherCollider.transform.GetComponent<SimpleForwardMovement>().movement_speed = temp_movement_speed + contact.otherCollider.transform.GetComponent<SimpleForwardMovement>().movement_speed/2;


            //Debug.Log(contact.otherCollider.transform.name.ToString() + " " + contact.otherCollider.transform.forward * contact.otherCollider.transform.GetComponent<SimpleForwardMovement>().movement_speed + " " +
            //        collided.name + " " + collided.GetComponent<SimpleForwardMovement>().vector_velocity_direction * collided.GetComponent<SimpleForwardMovement>().movement_speed);        


        }

    }
}

