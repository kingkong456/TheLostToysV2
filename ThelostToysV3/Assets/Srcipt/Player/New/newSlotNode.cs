using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newSlotNode : MonoBehaviour {
    public Image m_img_icon;
    public int leftRight;
    public GameObject select_current;

    void Start()
    {
        select_current.SetActive(false);
    }

    public void move_left(float degree)
    {
        transform.Rotate(0, 0,  leftRight * degree);
    }

    public void set_NewICon(Sprite icon)
    {
        m_img_icon.sprite = icon;
    }

    public void select()
    {
        select_current.SetActive(true);
    }

    public void unselect()
    {
        select_current.SetActive(false);
    }
}
