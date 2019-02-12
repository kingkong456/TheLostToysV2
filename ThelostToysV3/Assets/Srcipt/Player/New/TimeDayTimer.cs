using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDayTimer : MonoBehaviour {
    public float timer;
    public Image timer_bar;
    public float timer_de;
    public Text time_txt;
    public GameObject timeUpPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timeUpPanel.SetActive(true);
        }
        int timerTxt = (int)timer;
        time_txt.text = timerTxt.ToString();
        timer_bar.fillAmount = timer / timer_de;
	}
}
