using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialTriiger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("in");
            GameObject.FindObjectOfType<Tutorial>().nextpanel();
            this.gameObject.SetActive(false);
        }
    }

}
