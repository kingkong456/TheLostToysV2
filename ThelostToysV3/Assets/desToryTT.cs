using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desToryTT : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

}
