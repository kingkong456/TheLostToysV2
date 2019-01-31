using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_Col : MonoBehaviour {
    public float dmg;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            //hit enemy
            other.GetComponent<HpSystem>().tacking_Dmg(dmg);
        }

        Destroy(this.gameObject);
    }

}
