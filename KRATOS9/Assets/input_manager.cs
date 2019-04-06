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
            Vector2 center;

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

        // Start is called before the first frame update
        void Start()
        {
            float screen_width = Screen.width;
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

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            initial_swipe_position = touch.position;
                            break;

                        case TouchPhase.Ended:
                            end_swipe_position     = touch.position;

                            if (CheckSwipe())
                            {
                                this_movement_manager.RotateShip(GetTouchSide(end_swipe_position), this_movement_manager.angles_to_rotate);
                                if (end_swipe_position.y > initial_swipe_position.y) this_movement_manager.DecreaseSpeed(speed_modification);
                                else this_movement_manager.IncreaseSpeed(speed_modification);

                                this_movement_manager.UpdateDirectorVector(this.this_movement_manager.ship_transform.forward);
                            }

                            break;
                    }
                }

            }
        }

        void ReceiveInputEditor()
        {
            if (Input.GetMouseButtonDown(0))
            {
                initial_swipe_position = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                end_swipe_position = Input.mousePosition;

                if (CheckSwipe())
                {
                    this_movement_manager.RotateShip(GetTouchSide(end_swipe_position), this_movement_manager.angles_to_rotate);
                    if (end_swipe_position.y > initial_swipe_position.y) this_movement_manager.DecreaseSpeed(speed_modification);
                    else this_movement_manager.IncreaseSpeed(speed_modification);

                    this_movement_manager.UpdateDirectorVector(this.this_movement_manager.ship_transform.forward);
                }
            }               
            
        }

        movement_manager.SideToRotate GetTouchSide(Vector2 point)
        {
            return point.x < ship_position_in_screen.x ? movement_manager.SideToRotate.left : movement_manager.SideToRotate.right;
        }

        bool CheckSwipe()
        {
            return Vector2.Distance(initial_swipe_position, end_swipe_position) > MINIMUM_SWIPE_DISTANCE;
        }
    }
}