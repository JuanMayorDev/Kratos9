using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kratos9
{

    public class CameraBehaviour : MonoBehaviour
    {

        public List<movement_manager> ships;
        public float min_padding, max_padding;
        Transform camera_transform;
        Camera camera_component;
        
        // Start is called before the first frame update
        void Start()
        {
            camera_transform = transform;
            camera_component = camera_transform.GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 position = GetCenter();
            camera_transform.position = new Vector3(position.x, camera_transform.position.y, position.y);

            foreach (movement_manager ship in ships)
            {
                while (CheckIfNeedToGrow(ship)) camera_component.fieldOfView += Time.deltaTime;
            }
        }

        bool CheckIfNeedToGrow(movement_manager ship)
        {
            BoxCollider collider = ship.GetComponentInChildren<BoxCollider>();
            Vector2 min_extents  = new Vector2(collider.bounds.min.x, collider.bounds.min.y);
            Vector2 max_extents  = new Vector2(collider.bounds.max.x, collider.bounds.max.y);

            float min_left_position   = Camera.main.WorldToScreenPoint(new Vector3(min_extents.x, 0 ,collider.bounds.center.y)).x;
            float max_right_position  = Camera.main.WorldToScreenPoint(new Vector3(max_extents.x, 0, collider.bounds.center.y)).x;
            float min_bottom_position = Camera.main.WorldToScreenPoint(new Vector3(collider.bounds.center.y, 0, min_extents.y)).z;
            float max_top_position    = Camera.main.WorldToScreenPoint(new Vector3(collider.bounds.center.y, 0 , max_extents.y)).z;

            return (min_left_position - min_padding < 0 || min_bottom_position - min_padding < 0 || max_top_position + min_padding > Screen.height || max_right_position + min_padding > Screen.width);
        }

       

        Vector2 GetCenter()
        {
            float Ax = ships[0].ship_transform.position.x;
            float Az = ships[0].ship_transform.position.z;
            float Bx = ships[1].ship_transform.position.x;
            float Bz = ships[1].ship_transform.position.z;

            return new Vector2(Ax + (Bx - Ax) * 0.5f, Az + (Bz - Az) * 0.5f);
        }
        

    }
}
