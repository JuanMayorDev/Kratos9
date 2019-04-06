using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kratos9
{
    public class SimpleForwardMovement : MonoBehaviour
    {

        private Rigidbody my_rb;
        private Transform my_tr;
        public float movement_speed;
        public Vector3 vector_velocity_direction;
        public bool winded;



        // Start is called before the first frame update
        void Start()
        {
            my_rb = GetComponent<Rigidbody>();

            my_tr = GetComponent<Transform>();
            vector_velocity_direction = my_tr.forward;
        }

        // Update is called once per frame
        void Update()
        {
           // if(moving)
                my_tr.position += vector_velocity_direction * movement_speed * Time.deltaTime;

            if (movement_speed > 0)
            {
                movement_speed -= Time.deltaTime;
            }
            if(movement_speed < 0)
            {
                movement_speed = 0;
            }

            if(winded)
            {
                movement_speed -= Time.deltaTime * 10;

            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Wind")
            {
                winded = true;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.name == "Boat1")
            {
                ImpactManager impact = new ImpactManager();

                impact.CalculateImpactForces(my_tr, collision.GetContact(0));
                //collision.GetContact(0).
                
            }
        }

    }

}
