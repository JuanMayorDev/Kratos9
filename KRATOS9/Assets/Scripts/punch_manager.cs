using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kratos9
{
    public class punch_manager : MonoBehaviour
    {

        movement_manager this_movement_manager;

        public float punch_charge_needed;
        public float punch_charge;
        public float punch_dash_duration;
        public bool punch_ready;
        public PunchButtonController my_punch_button_controller;



        // Start is called before the first frame update
        void Start()
        {
            this_movement_manager = GetComponent<movement_manager>();
        }

        // Update is called once per frame
        void Update()
        {
            punch_charge += Time.deltaTime * 0.5f;
            my_punch_button_controller.UpdateFillAmount(punch_charge/punch_charge_needed);
           if(punch_charge >= punch_charge_needed)
            {
                punch_ready = true;
            }

        }

        public void Punch()

        { 
            if(punch_ready)
            {
                this_movement_manager.my_anim.SetTrigger("Attack");
                this_movement_manager.hitting = true;
                this_movement_manager.director_speed = Vector3.zero;
                this_movement_manager.RecieveImpact(this_movement_manager.ship_transform.forward * this_movement_manager.dash_speed);
                punch_charge = 0;
                punch_ready = false;
                Invoke("StopPunchEffect", punch_dash_duration);
                my_punch_button_controller.ResetFillAmount();
            }


        }

        public void StopPunchEffect()
        {
            CancelInvoke("StopPunchEffect");
            this_movement_manager.director_speed = Vector3.forward;
            this_movement_manager.current_speed = this_movement_manager.base_speed;
            this_movement_manager.hitting = false;
        }

        public void IncreasePunchCharge(byte _b)
        {
            punch_charge += _b;
            float amount = punch_charge / punch_charge_needed;
            my_punch_button_controller.UpdateFillAmount(amount);
        }

    }
}
