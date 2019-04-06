using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kratos9 {
    public class movement_manager : MonoBehaviour
    {
        
        public Transform ship_transform;

        public float   angles_to_rotate;
        public Vector3 flow_direction;
        public float   max_director_speed_magnitude;
        public Vector3 director_speed;
        public enum SideToRotate {left, right }
        public bool hitting;
        public float dash_speed;

        public float base_speed    = 1f;
        public float current_speed = 1f;

        public float lerp_time;

        punch_manager this_punch_manager;

       

        // Start is called before the first frame update
        void Start()
        {
            director_speed = ship_transform.forward * base_speed;
            this_punch_manager = GetComponent<punch_manager>();
        }

        // Update is called once per frame
        void Update()
        {
            Debug.DrawLine(ship_transform.position, ship_transform.position + director_speed * 10, Color.blue);
            ship_transform.position += (director_speed * current_speed) * Time.deltaTime;

            if (!hitting)
            {
                director_speed = director_speed.magnitude > 0 ? director_speed - director_speed * Time.deltaTime * 0.001f : Vector3.zero;

                if(director_speed != flow_direction)
                {
                    director_speed += flow_direction;
                }
                director_speed = Vector3.ClampMagnitude(director_speed, max_director_speed_magnitude);
            }
        }

        public void RotateShip(SideToRotate _s, float angles)
        {
           switch (_s)
            {
                case SideToRotate.left:

                    ship_transform.eulerAngles = new Vector3(ship_transform.eulerAngles.x, ship_transform.eulerAngles.y - angles, ship_transform.eulerAngles.z);
                    break;

                case SideToRotate.right:

                    ship_transform.eulerAngles = new Vector3(ship_transform.eulerAngles.x, ship_transform.eulerAngles.y + angles, ship_transform.eulerAngles.z);
                    break;
            }
        }

        public void UpdateDirectorVector(Vector3 _v)
        {
            director_speed += _v  ;

        }

        public void IncreaseSpeed(float _s)
        {
             if(!hitting) UpdateDirectorVector(ship_transform.forward * _s);            
        }

        public Vector3 GetDirectorVector()
        {
            return director_speed;
        }

        public float GetSpeed()
        {
            return current_speed;
        }

        public void DecreaseSpeed(float _s)
        {
            if (!hitting) UpdateDirectorVector(-ship_transform.forward * _s * 5f);          
        }

        public void RecieveImpact(Vector3 _v)
        {
            director_speed += _v;
        }

       
    }
}
