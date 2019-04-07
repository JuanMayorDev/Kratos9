using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kratos9
{
    
    public class collision_detector : MonoBehaviour
    {
        Kratos9.movement_manager this_movement_manager;
        Kratos9.ImpactManager impactManager = new Kratos9.ImpactManager();
        public float camshake_duration, magnitude;
        public Transform cam1, cam2;
        private void Start()
        {
            this_movement_manager = transform.GetComponent<movement_manager>();
        }


        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Boat")
            {
                ParticlesSpawner.Instance.SpawnHit(collision.GetContact(0).point);
                impactManager.CalculateImpactForces(transform, collision.GetContact(0));
                StartCoroutine(CamShake());
            }
            if(collision.transform.tag == "Enviroment")
            {
                ParticlesSpawner.Instance.SpawnHit(collision.GetContact(0).point);
                this_movement_manager.RecieveImpact(collision.GetContact(0).normal * this_movement_manager.GetDirectorVector().magnitude);
            }
        }

        IEnumerator CamShake()
        {
            Vector3 cam1_s_pos = cam1.position;
            Vector3 cam2_s_pos = cam2.position;

            float elapsed_time = 0f;
            while(elapsed_time < camshake_duration)
            {
                Debug.Log("Hey");
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                cam1_s_pos = new Vector3(cam1.position.x + x, cam1.position.y + y, cam1.position.z);
                cam2_s_pos = new Vector3(cam2.position.x + x, cam2.position.y + y, cam2.position.z);
                elapsed_time += Time.deltaTime;
                yield return null;
            }
            cam1.position = cam1_s_pos;
            cam2.position = cam2_s_pos;
        }
    }

    
}
