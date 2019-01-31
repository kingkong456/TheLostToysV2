using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Inhand : MonoBehaviour {

    public GameObject[] toys_po;

    public void active_toy(string name)
    {
        for (int i = 0; i < toys_po.Length; i++)
        {
            if(toys_po[i].name == name)
            {
                toys_po[i].SetActive(true);
            }
        }
    }

    public void hide_toy(string name)
    {
        for (int i = 0; i < toys_po.Length; i++)
        {
            if (toys_po[i].name == name)
            {
                toys_po[i].SetActive(false);
            }
        }
    }

}
