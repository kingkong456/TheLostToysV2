using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyGoalmanager : MonoBehaviour {

    public GameObject Fogy;
    public GameObject Sword;
    public GameObject Kike;

    [Header("Object")]
    public GameObject mainCam;
    public GameObject toyCam;
    public GameObject Canves;

    public void showToy()
    {
        Canves.SetActive(false);
        mainCam.SetActive(false);
        toyCam.SetActive(true);
    }

    void endend()
    {
        Canves.SetActive(true);
        mainCam.SetActive(true);
        toyCam.SetActive(false);
    }

    public void getFogy()
    {
        showToy();
        Fogy.SetActive(true);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(mainCam.active == false && Input.GetButtonDown("all_j_buttun_summit"))
        {
            endend();
        }
	}
}
