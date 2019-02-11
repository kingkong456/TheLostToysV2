using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDayTimer : MonoBehaviour {
    public float timer;
    public Image timer_bar;
    public float timer_de;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        timer_bar.fillAmount = timer / timer_de;
	}
}
