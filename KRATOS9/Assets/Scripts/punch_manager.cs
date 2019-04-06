using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kratos9
{
    public class punch_manager : MonoBehaviour
    {

        movement_manager this_movement_manager;
        public float punch_dash_duration;

        // Start is called before the first frame update
        void Start()
        {
            this_movement_manager = GetComponent<movement_manager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) Punch();
        }

        public void Punch()
        {
            this_movement_manager.hitting = true;
            this_movement_manager.current_speed = this_movement_manager.dash_speed;
            Invoke("StopPunchEffect", punch_dash_duration);
        }

        public void StopPunchEffect()
        {
            CancelInvoke("StopPunchEffect");
            this_movement_manager.hitting = false;
            this_movement_manager.current_speed = this_movement_manager.base_speed;
        }

      

    }
}