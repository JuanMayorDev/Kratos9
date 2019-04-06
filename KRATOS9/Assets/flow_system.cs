using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pokoi
{
    public class flow_system : MonoBehaviour
    {
        public List<movement_manager> ships_references;
        enum Flow { up, down, none }
        Flow current_flow = Flow.none;

        public float min_seconds_to_change_flow, max_seconds_to_change_flow, flow_speed;

        // Start is called before the first frame update
        void Start()
        {
            ChangeFlow();
        }

        private void ChangeFlow()
        {
            current_flow = current_flow == Flow.up || current_flow == Flow.none ? Flow.down : Flow.up;

            foreach (movement_manager ship in ships_references)
            {
                ship.flow_direction = current_flow == Flow.up ? Vector3.forward * flow_speed  : -Vector3.forward * flow_speed;
            }

            Invoke("ChangeFlow", Random.Range(min_seconds_to_change_flow, max_seconds_to_change_flow));

        }
    }
}
