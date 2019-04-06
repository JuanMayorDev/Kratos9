using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pokoi
{
    public class input_manager : MonoBehaviour
    {
        const float MINIMUM_SWIPE_DISTANCE = 10f;
        struct Area
        {

            float width;
            float height;
            Vector2 min_extents;
            Vector2 max_extents;
            public Vector2 center;

            public Area(Vector2 _c, float _w, float _h)
            {
                width = _w;
                height = _h;
                center = _c;

                min_extents = new Vector2(center.x - (width * 0.5f), center.y - (height * 0.5f));
                max_extents = new Vector2(center.x + (width * 0.5f), center.y + (height * 0.5f));
            }

            public bool ContainsPoint(Vector2 point)
            {
                return (point.x < max_extents.x && point.x > min_extents.x && point.y < max_extents.y && point.y > min_extents.y);
            }
        }

        public enum ScreenHalfs { upper_half, lower_half }
        public ScreenHalfs this_half;

        public enum Side { left, right }

        public movement_manager this_movement_manager;

        public float speed_modification;


        Area player_area;

        Vector2 ship_position_in_screen;

        Vector2 initial_swipe_position;
        Vector2 end_swipe_position;

        bool touched;

        // Start is called before the first frame update
        void Start()
        {
            float screen_width  = Screen.width;
            float screen_height = Screen.height;

            if (this_half == ScreenHalfs.upper_half)
                player_area  = new Area(new Vector2(screen_width * 0.5f, screen_height * 0.75f), screen_width, screen_height);
            else player_area = new Area(new Vector2(screen_width * 0.5f, screen_height * 0.25f), screen_width, screen_height);
        }

        // Update is called once per frame
        void Update()
        {
            ship_position_in_screen = Camera.main.WorldToScreenPoint(transform.position);

            if (Application.isEditor)
            {
                ReceiveInputEditor();
            }
            else ReceiveInputMobile();

        }

        void ReceiveInputMobile()
        {
            var input_count = Input.touchCount;
            if (input_count > 0)
            {
               for (var iterator = 0; iterator < input_count; iterator++)
                {
                    Touch touch = Input.GetTouch(iterator);

                    if (player_area.ContainsPoint(touch.position))
                    {

                        switch (touch.phase)
                        {
                            case TouchPhase.Began: OnInputPressed(touch.position); break;

                            case TouchPhase.Ended: OnInputRelease(touch.position); break;
                        }
                    }
                }

            }
        }

        void ReceiveInputEditor()
        {
            if (Input.GetMouseButtonDown(0) && player_area.ContainsPoint(Input.mousePosition)) OnInputPressed(Input.mousePosition);
            
            if (Input.GetMouseButtonUp(0))   OnInputRelease(Input.mousePosition);              
            
        }

        movement_manager.SideToRotate GetTouchSide(Vector2 point)
        {
            return point.x < player_area.center.x ? movement_manager.SideToRotate.right : movement_manager.SideToRotate.left;
        }

        bool CheckSwipe()
        {
            return Vector2.Distance(initial_swipe_position, end_swipe_position) > MINIMUM_SWIPE_DISTANCE;
        }

        void OnInputPressed(Vector2 _p)
        {
            touched = true;
            initial_swipe_position = _p;
        }

        void OnInputRelease(Vector2 _p)
        {
            if (touched)
            {
                end_swipe_position = _p;

                if (CheckSwipe())
                {
                    this_movement_manager.RotateShip(GetTouchSide(end_swipe_position), this_movement_manager.angles_to_rotate);
                    if (end_swipe_position.y > initial_swipe_position.y)
                    {
                        if (this_half == ScreenHalfs.lower_half) this_movement_manager.DecreaseSpeed(speed_modification);
                        else this_movement_manager.IncreaseSpeed(speed_modification);
                    }
                    else
                    {
                        if (this_half == ScreenHalfs.lower_half) this_movement_manager.IncreaseSpeed(speed_modification);
                        else this_movement_manager.DecreaseSpeed(speed_modification);
                    }

                    this_movement_manager.UpdateDirectorVector(this.this_movement_manager.ship_transform.forward);
                }
                touched = false;
            }
        }
    }
}