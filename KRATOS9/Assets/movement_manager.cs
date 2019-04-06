using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pokoi {
    public class movement_manager : MonoBehaviour
    {
        
        public Transform ship_transform;

        public float angles_to_rotate;

        float speed = 1f;

        Vector3 director_speed;

        public enum SideToRotate {left, right }

        // Start is called before the first frame update
        void Start()
        {
            director_speed = ship_transform.forward * speed;
        }

        // Update is called once per frame
        void Update()
        {            
            ship_transform.position += director_speed * Time.deltaTime;
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
            director_speed = _v * speed;
        }

        public void IncreaseSpeed(float _s)
        {
            speed += _s;
        }

        public void DecreaseSpeed(float _s)
        {
            speed -= _s;
        }


    }
}
