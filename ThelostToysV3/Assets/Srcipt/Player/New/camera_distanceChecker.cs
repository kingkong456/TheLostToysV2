using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class camera_distanceChecker : MonoBehaviour
    {
        public Camera player1_cam;
        public Camera player2_cam;
        public Camera main_cam;
        public float max_distance;
        public Transform player1;
        public Transform player2;

        // Update is called once per frame
        void Update()
        {
            if (Vector3.Distance(player1.position, player2.position) >= max_distance)
            {
                main_cam.GetComponent<Camera>().enabled = false;
                main_cam.GetComponent<cameraMultiTraget>().islock = true;

                player1_cam.GetComponent<Camera>().enabled = true;
                player2_cam.GetComponent<Camera>().enabled = true;
            }
            else
            {
                main_cam.GetComponent<Camera>().enabled = true;
                main_cam.GetComponent<cameraMultiTraget>().islock = false;

                player1_cam.GetComponent<Camera>().enabled = false;
                player2_cam.GetComponent<Camera>().enabled = false;
            }
        }
    }
}