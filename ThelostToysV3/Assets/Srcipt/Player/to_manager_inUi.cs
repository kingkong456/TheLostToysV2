using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class to_manager_inUi : MonoBehaviour {

    public GameObject[] selected_ui;
    public Image[] toy_icon; 

    public void select_toy(int index)
    {
        selected_ui[index].SetActive(true);
    }

    public void deSelect_toy(int index)
    {
        selected_ui[index].SetActive(false);
    }

    public void set_toy_icon(int index, Sprite sp)
    {
        toy_icon[index].sprite = sp;
    }
}
