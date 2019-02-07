using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSystem : MonoBehaviour {
    //controller
    public enemyController m_controller;
    public GameObject myHeader;

    //hp point
    public float start_hp;
    private float hp;

    public Image hp_bar;

    public GameObject text_hp;
    public Vector3 textOffset;

    public GameObject[] toy_droppingData;

    private void Start()
    {
        //set hp
        if (m_controller == null)
        {
            m_controller = GetComponent<enemyController>();
        }
        if(myHeader == null)
        {
            myHeader = GetComponent<GameObject>();
        }

        //set hp
        hp = start_hp;
    }

    //play get danmge animation
    //look controller
    //decrease hp
    public void tacking_Dmg(float dmg)
    {
        Debug.Log("Hited");
        hp -= dmg;
        this.GetComponent<Animator>().SetTrigger("getHurt");
        m_controller.is_canMove = false;
        spawntext_hp();

        hp_bar.fillAmount = hp / start_hp;
        if(hp <= 0)
        {
            die();
        }
    }

    void spawntext_hp()
    {
        var go = Instantiate(text_hp, transform.position + textOffset, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = hp.ToString();
    }

    //die
    //play aniamtion
    //destrosy this
    void die()
    {
        this.GetComponent<Animator>().SetTrigger("die");
        int drop_number = Random.Range(0, toy_droppingData.Length - 1);
        if((Random.Range(0,3)) != 1)
        {
            drop_item(drop_number);
        }
        Destroy(myHeader, 2f);
        //this.GetComponent<AudioSource>().PlayOneShot(myHeader.GetComponent<sound_data>().enemy_die);
    }

    void drop_item(int index)
    {
        Instantiate(toy_droppingData[index], new Vector3(transform.position.x, toy_droppingData[index].transform.position.y, transform.position.z), transform.rotation);
    }

}
