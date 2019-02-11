using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class newSlotNode : MonoBehaviour {
    public Image m_img_icon;
    public int leftRight;
    public GameObject select_current;
    public Image m_face;
    public Sprite select_sp;
    public Sprite unSlect_sp;
    public Transform spawnRemovePoint;

    void Start()
    {
        select_current.SetActive(false);
    }

    public void move_left(float degree)
    {
        //transform.Rotate(new Vector3(0, 0,  leftRight * degree));
        transform.Translate(leftRight * degree, 0, 0);
    }

    public void set_NewICon(Sprite icon)
    {
        m_img_icon.sprite = icon;
    }

    public void select()
    {
        select_current.SetActive(true);
        m_face.sprite = select_sp;
    }

    public void unselect()
    {
        select_current.SetActive(false);
        m_face.sprite = unSlect_sp;
    }
}
