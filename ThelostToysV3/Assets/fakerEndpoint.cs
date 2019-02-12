using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakerEndpoint : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
//        if(other.gameObject.tag == "Player")
//        {
//            Destroy(this.gameObject);
//            GameObject.FindObjectOfType<ToyGoalmanager>().getFogy();
//        }
        if(other.gameObject.tag == "Player")
        {
            GameObject.FindObjectOfType<ToyGoalmanager>().getFogy();
            Destroy(this.gameObject);
        }
    }

}
