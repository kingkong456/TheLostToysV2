using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe_col : MonoBehaviour {

    public float dmg;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<HpSystem>().tacking_Dmg(dmg);
        }
        else if(other.gameObject.tag == "tree")
        {
            other.gameObject.GetComponent<Tree>().tacking_dmg(dmg);
        }
    }
}
