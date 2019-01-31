using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class danger_col : MonoBehaviour
{

    public float power;
    public bool isBullet;

    [Header("Controller")]
    public enemyController m_controller;

    void call_col()
    {
        m_controller.attack();
    }

    void end_attack()
    {
        m_controller.end_attack();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //hited
            other.GetComponent<playerData>().tacking_Dmg(power);
        }

        if (other.gameObject.tag != "Enemy" && isBullet)
        {
            Destroy(this.gameObject);
        }
    }

}
