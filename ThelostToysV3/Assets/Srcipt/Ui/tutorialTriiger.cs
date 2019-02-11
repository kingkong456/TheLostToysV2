using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialTriiger : MonoBehaviour {

    void OnTrigerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject.FindObjectOfType<Tutorial>().nextpanel();
        }
    }

}
