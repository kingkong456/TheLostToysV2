using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public GameObject[] tutorial_panel;
    private int currentPanel = -1;
    public GameObject tutorialPanel;

    private void Start()
    {
        nextpanel();
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("all_j_buttun_summit"))
        {
            tutorial_panel[currentPanel].SetActive(false);
            tutorialPanel.SetActive(false);
        }
    }

    public void nextpanel()
    {
        currentPanel++;
        tutorialPanel.SetActive(true);
        tutorial_panel[currentPanel].SetActive(true);
    }
}
