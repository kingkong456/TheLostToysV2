using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_tutorialJoy : MonoBehaviour {

    private void Start()
    {
        this.GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    // Update is called once per frame
    void Update ()
    {
        if(Input.GetButtonDown("all_j_buttun_summit"))
        {
            this.GetComponent<DialogueTrigger>().next_di();
        }
	}
}
