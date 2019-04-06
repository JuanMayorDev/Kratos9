using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pokoi {
    public class movement_manager : MonoBehaviour
    {
        
        public Transform ship_transform;

        public float angles_to_rotate;
        public Vector3 flow_direction;

        float speed = 1f;        

        public Vector3 director_speed;

        public enum SideToRotate {left, right }

        // Start is called before the first frame update
        void Start()
        {
            director_speed = ship_transform.forward * speed;
        }

        // Update is called once per frame
        void Update()
        {
            
            ship_transform.position += ( director_speed + flow_direction )* Time.deltaTime;
           
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
            director_speed += _v  * speed;
        }

        public void IncreaseSpeed(float _s)
        {
             UpdateDirectorVector(ship_transform.forward * _s);            
        }

        public Vector3 GetDirectorVector()
        {
            return director_speed;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public void DecreaseSpeed(float _s)
        {
            UpdateDirectorVector(-ship_transform.forward * _s * 2f);          
        }

        public void RecieveImpact(Vector3 _v)
        {
            director_speed += _v;
        }

       
    }
}
