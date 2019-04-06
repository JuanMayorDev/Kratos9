using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kratos9
{
    public class punch_manager : MonoBehaviour
    {

        movement_manager this_movement_manager;

        public byte punch_charge_needed;
        public byte punch_charge;
        public float punch_dash_duration;
        public bool punch_ready;

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
            if(punch_ready)
            {
                this_movement_manager.hitting = true;
                this_movement_manager.director_speed = Vector3.zero;
                this_movement_manager.RecieveImpact(this_movement_manager.ship_transform.forward * this_movement_manager.dash_speed);
                punch_charge = 0;
                punch_ready = false;
                Invoke("StopPunchEffect", punch_dash_duration);
            }

        }

        public void StopPunchEffect()
        {
            CancelInvoke("StopPunchEffect");
            this_movement_manager.director_speed = Vector3.forward;
            this_movement_manager.current_speed = this_movement_manager.base_speed;
            this_movement_manager.hitting = false;
        }

    }
}